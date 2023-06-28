using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

public partial class EndlessLevelControl {
  // public partial class EndlessOriginalLevel {
  //mobs by tiers
  Enemy[][] mobsByTier = new Enemy[5][];
  [SerializeField] Enemy[] tier0Mobs, tier1Mobs, tier2Mobs, tier3Mobs, tier4Mobs;

  // 1st array is the actual values comapared for the rates
  // 2nd array is the current percentage (gap) of the tier
  // 3rd array is the final percentages to reach, the beginning percentages are put direclty in the beginning so don't matter
  int[,] difficultyRates = new int[3, 5] { { 60, 80, 100, 100, 100 }, { 60, 20, 20, 0, 0 }, { 0, 10, 20, 30, 40 } };

  //async cancellation token stuff
  CancellationTokenSource cancelToken;

  bool wavecycle = false;
  void setMobsByTier() {
    mobsByTier[0] = tier0Mobs;
    mobsByTier[1] = tier1Mobs;
    mobsByTier[2] = tier2Mobs;
    mobsByTier[3] = tier3Mobs;
    mobsByTier[4] = tier4Mobs;
  }
  async void EndlessSpawner() {
    cancelToken = new CancellationTokenSource();
    while (true) {
      if (cancelToken.IsCancellationRequested) return;
      if (wavecycle) { await Task.Yield(); continue; }
      wavecycle = true;
      int wavesNumber = Random.Range(1, 4);
      Task[] tasks = new Task[wavesNumber];
      for (int i = 0; i < wavesNumber; i++) {
        //difficulty goes from 0 up to 4. (0 is easiest)
        int difficulty = getDifficulty();
        tasks[i] = StartRandomWave(difficulty);
      }
      if (cancelToken.IsCancellationRequested) return;
      await Task.WhenAll(tasks);
      wavecycle = false;
    }
  }
  float waveFrequencyChange() {
    //raise frequency of waves after 10 mins elapsed
    float frequency = 1f;
    if (Time.time - EndlessStartTime > 600f) {
      float value = (Time.time - EndlessStartTime + 4.18f + 600f);
      frequency = Mathf.Log(value) / Mathf.Log(4.18f);
    }
    return frequency;
  }
  async Task StartRandomWave(int difficulty) {
    float frequencyRaise = waveFrequencyChange();
    float wavePeriod = Random.Range(5f, 23f) * frequencyRaise;
    //together with delay, this gives about 22seconds per cycle which allows for roughly 15mins before the maximum difficulty is reached - in terms of avg cycles required for max difficulty (41 cycles - apparently).

    //spawnperiod:singleburst/periodic; position: scattered/bunched
    bool spawnPeriodBurst = Random.Range(0, 2) == 1 ? true : false;
    bool spawnPositionSpread = Random.Range(0, 2) == 1 ? true : false;

    //pickEnemies variety
    bool repeatEnemy = Random.Range(0, 2) == 1 ? true : false;

    float delay = Random.Range(0f, 0.5f * wavePeriod);
    await AsyncAdditional.Delay(delay);

    //cancel to avoid errors of starting non existent coroutine.
    if (cancelToken.IsCancellationRequested) return;

    //make coroutine for actually instantiating stuff so that the game doesnt break with permanent scene alterations due to missing cancellationtokens miss
    StartCoroutine(subWave(wavePeriod, spawnPeriodBurst, spawnPositionSpread, repeatEnemy, difficulty));

    if (cancelToken.IsCancellationRequested) return;
    await AsyncAdditional.Delay(wavePeriod);
    if (cancelToken.IsCancellationRequested) return;
  }

  #region difficulty getter and modulation
  int getDifficulty() {
    int difficultyRandom = Random.Range(1, 101);
    int difficultyToReturn = 0;
    for (int i = 0; i < 5; i++) {
      if (i == 0) {
        if (difficultyRandom <= difficultyRates[0, i]) {
          difficultyToReturn = i;
        }
      } else {
        if (difficultyRandom > difficultyRates[0, i - 1] && difficultyRandom <= difficultyRates[0, i]) {
          difficultyToReturn = i;
        }
      }
    }
    changeDifficultyRates(difficultyToReturn);
    return difficultyToReturn;
  }
  void changeDifficultyRates(int thisDifficulty) {
    if (difficultyRates[1, thisDifficulty] > difficultyRates[2, thisDifficulty]) {
      difficultyRates[0, thisDifficulty] -= 5;
    }
    updateDifficultyGapArray();
  }
  void updateDifficultyGapArray() {
    difficultyRates[1, 0] = difficultyRates[0, 0];
    for (int i = 1; i < 5; i++) {
      difficultyRates[1, i] = difficultyRates[0, i] - difficultyRates[0, i - 1];
    }
  }
  #endregion

  #region subWave methods
  IEnumerator subWave(float wavePeriod, bool spawnPeriodBurst, bool spawnPositionSpread, bool repeatEnemy, int difficulty) {
    int enemiesNumber = getEnemiesNumber(difficulty);
    if (spawnPeriodBurst) {
      float prewait = Random.Range(0f, wavePeriod);
      yield return new WaitForSeconds(prewait);
      subSpawn(difficulty, enemiesNumber, spawnPositionSpread, repeatEnemy);
      yield return new WaitForSeconds(wavePeriod - prewait);
    } else {
      float subPeriod = wavePeriod / (float)enemiesNumber;
      float startTime = Time.time;
      while (Time.time < startTime + wavePeriod) {
        subSpawn(difficulty, 1, spawnPositionSpread, repeatEnemy);
        yield return new WaitForSeconds(subPeriod);
      }
    }
  }
  void subSpawn(int difficulty, int enemiesNumber, bool spawnPositionSpread, bool repeatEnemy) {
    float xPos = 0f;
    string enemyName = mobsByTier[difficulty][Random.Range(0, mobsByTier[difficulty].Length)].enemyPrefab.name;
    for (int i = 0; i < enemiesNumber; i++) {
      if (spawnPositionSpread) {
        xPos = Random.Range(-5F, 5F);
      }
      if (!repeatEnemy) {
        enemyName = mobsByTier[difficulty][Random.Range(0, mobsByTier[difficulty].Length)].enemyPrefab.name;
      }
      spawner.spawnEnemy(enemyName, xPos, 11f);
    }
  }
  int getEnemiesNumber(int difficulty) {
    int ranNum = Random.Range(1, 6);
    int actualNum = 1;
    if (ranNum - difficulty > 0) actualNum = ranNum - difficulty;
    int multiplier = Random.Range(1, 4);
    return multiplier * actualNum;
  }
  #endregion

  void OnDisable() {
    cancelToken.Cancel();
    StopAllCoroutines();
    cancelToken.Dispose();
  }
}
