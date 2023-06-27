using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

public class EndlessOriginalLevelMobControl : MonoBehaviour {
  // public partial class EndlessOriginalLevel {
  //mobs by tiers
  Enemy[][] mobsByTier = new Enemy[5][];
  [SerializeField] Enemy[] tier0Mobs, tier1Mobs, tier2Mobs, tier3Mobs, tier4Mobs;

  // 1st array is the actual values comapared for the rates
  // 2nd array is the current percentage (gap) of the tier
  // 3rd array is the final percentages to reach, the beginning percentages are put direclty in the beginning so don't matter

  int[,] tempRates = new int[3, 5] { { 60, 80, 100, 100, 100 }, { 60, 20, 20, 0, 0 }, { 0, 10, 20, 30, 40 } };
  int[,] difficultyRates = new int[3, 5] { { 60, 80, 100, 100, 100 }, { 60, 20, 20, 0, 0 }, { 0, 10, 20, 30, 40 } };

  int attempts = 0;
  int total = 0;
  int waves = 0;

  //async cancellation token stuff
  CancellationTokenSource cancelToken;


  bool wavecycle = false;
  void Start() {
    EndlessSpawner();
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
        tasks[i] = StartRandomWave();
      }
      await Task.WhenAll(tasks);
      wavecycle = false;
    }
  }
  async Task StartRandomWave() {
    float period = Random.Range(5f, 23f);//together with delay gives about 22seconds per cycle which allows for roughly 15mins before the maximum difficulty is reached in terms of avg cycles required for max difficulty (41 cycles)
    float delay = Random.Range(0f, 0.5f * period);

    //spawnperiod:burst/delayed; position: scattered/bunched
    bool burst = Random.Range(0, 2) == 1 ? true : false;
    bool scattered = Random.Range(0, 2) == 1 ? true : false;
    await AsyncAdditional.Delay(delay, true);
    //difficulty goes from 0 up to 4. (0 is easiest)
    int difficulty = getDifficulty();
    if (cancelToken.IsCancellationRequested) return;
    // Instantiate(prefabobject, Vector3.zero, Quaternion.identity);
    //make coroutine for actually instantiating stuff so that the game doesnt break due to missing cancellationtokens miss
    if (cancelToken.IsCancellationRequested) return;
    await AsyncAdditional.Delay(period, true);
    //picktype: burst/spread; position: scattered/bunched
    //pickEnemies
  }

  int getDifficulty() {
    int difficultyRandom = Random.Range(1, 101);
    for (int i = 0; i < 5; i++) {
      if (i == 0) {
        if (difficultyRandom <= difficultyRates[0, i]) {
          return i;
        }
      } else {
        if (difficultyRandom > difficultyRates[0, i - 1] && difficultyRandom <= difficultyRates[0, i]) {
          return i;
        }
      }
    }
    return 0;//technically not reachable.
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
    bool done = true;
    for (int i = 0; i < 5; i++) {
      if (difficultyRates[1, i] != difficultyRates[2, i]) {
        done = false;
        break;
      }
    }
    if (done) {
      print(waves);
      for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 5; j++) {
          difficultyRates[i, j] = tempRates[i, j];
        }
      }
      total += waves;
      waves = 0;
      attempts++;
      if (attempts > 10000) {
        print("done");
        print(total / 10000);
        cancelToken.Cancel();
      }
    }

  }
  void OnDestroy() {
    cancelToken.Cancel();
    StopAllCoroutines();
    cancelToken.Dispose();
  }
}
