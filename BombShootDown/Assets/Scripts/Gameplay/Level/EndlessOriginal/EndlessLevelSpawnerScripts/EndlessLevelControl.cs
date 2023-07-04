using System.Collections;
using UnityEngine;

public partial class EndlessLevelControl : MonoBehaviour, IGetLevelDataInterface {
  //this is also the BossSpawnController.

  [SerializeField]
  Level level;
  LevelSpawner spawner;
  new AudioManagerBGM audio;

  float EndlessStartTime;

  Enemy[][] bossByTier = new Enemy[5][];
  [SerializeField] Enemy[] tier0Boss, tier1Boss, tier2Boss, tier3Boss, tier4Boss;

  public Level GetLevelData() {
    return level;
  }
  void setBossByTier() {
    bossByTier[0] = tier0Boss;
    bossByTier[1] = tier1Boss;
    bossByTier[2] = tier2Boss;
    bossByTier[3] = tier3Boss;
    bossByTier[4] = tier4Boss;
  }
  void Awake() {
    EndlessStartTime = Time.time;
    spawner = gameObject.GetComponent<LevelSpawner>();
    spawner.setLevelData(level);
    audio = GameObject.Find("AudioManagerBGM").GetComponent<AudioManagerBGM>();
    audio.ChangeBGM("World1");
    setMobsByTier();
    setBossByTier();
  }
  void Update() {
    if (spawner.waveRunning == false && WaveController.startWave == true && WaveController.LevelCleared == false) {
      string name = spawner.findCorrectWaveToStart();
      if (name != null) {
        StartCoroutine(name);
      }
    }
  }
  IEnumerator wave1() {
    RandomBoss(0);
    EndlessSpawner();
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }
  IEnumerator wave2() {
    yield return new WaitForSeconds(55f);
    spawner.waveCleared();
  }
  IEnumerator wave3() {
    RandomBoss(1);
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }
  IEnumerator wave4() {
    yield return new WaitForSeconds(55f);
    spawner.waveCleared();
  }
  IEnumerator wave5() {
    RandomBoss(2);
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }
  IEnumerator wave6() {
    yield return new WaitForSeconds(55f);
    spawner.waveCleared();
  }
  IEnumerator wave7() {
    RandomBoss(3);
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
  }
  IEnumerator wave8() {
    yield return new WaitForSeconds(55f);
    spawner.waveCleared();
  }


  //this one is the final wave and goes on indefinitely.
  IEnumerator wave9() {
    //double spawn section
    yield return doubleSpawnRoutine(0, 0);
    yield return doubleSpawnRoutine(0, 1);
    yield return doubleSpawnRoutine(1, 2);
    yield return doubleSpawnRoutine(2, 3);

    yield return new WaitForSeconds(10f);
    //triple spawn infinite part
    while (true) {
      float waitDecrease = waveFrequencyChange();
      int[] tiers = pickTiersTriplet();
      foreach (int tier in tiers) {
        RandomBoss(tier);
      }
      yield return new WaitForSeconds(75f / waitDecrease);
    }
  }





  int[] pickTiersTriplet() {
    int[] tiersList = new int[3];
    //the 0th term will contain the highest tier for this wave.
    tiersList[0] = Random.Range(0, 5);
    for (int i = 1; i < 3; i++) {
      if (tiersList[i - 1] > 2) {
        tiersList[i] = Random.Range(0, tiersList[i - 1]);
      } else {
        tiersList[i] = Random.Range(0, tiersList[i - 1] + 1);
        //disables triple repeat of tiers that are not all 0 tier.
        if (tiersList[i] == tiersList[i - 1] && i > 1 && tiersList[i - 1] != 0) {
          tiersList[i] = Random.Range(0, tiersList[i - 1]);
        }
      }
    }
    return tiersList;
  }
  IEnumerator doubleSpawnRoutine(int tier1, int tier2) {
    RandomBoss(tier1);
    RandomBoss(tier2);
    yield return new WaitForSeconds(75f);
  }
  void RandomBoss(int tier) {
    if (tier == 3) {
      int ranNum = Random.Range(0, 1001);
      if (ranNum == 0) {
        string bossName = bossByTier[4][0].enemyPrefab.name;
        SpawnBoss(bossName);
      } else {
        string bossName = bossByTier[3][0].enemyPrefab.name;
        SpawnBoss(bossName);
      }
    } else {
      // int ranNum = Random.Range(0, bossByTier[tier].Length);
      // SpawnBoss(bossByTier[tier][ranNum].enemyPrefab.name);
      SpawnBoss("Minima");
    }
  }
  void SpawnBoss(string bossName) {
    spawner.spawnEnemyInMap(bossName, 0f, 9f, true, LevelSpawner.addToList.Specific, true);
    if (bossName == "CoupladSeeker") {
      spawner.spawnEnemyInMap("CoupladFollower", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
    }
    if (bossName == "MaxCoupladSeeker") {
      spawner.spawnEnemyInMap("MaxCoupladFollower", 0f, 9f, true, LevelSpawner.addToList.Specific, true);
    }
  }
}

