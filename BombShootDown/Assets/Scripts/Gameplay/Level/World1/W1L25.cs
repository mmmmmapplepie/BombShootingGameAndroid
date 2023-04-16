using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L25 : MonoBehaviour, IGetLevelDataInterface {
  [SerializeField]
  Level level;
  LevelSpawner spawner;
  new AudioManagerBGM audio;
  public Level GetLevelData() {
    return level;
  }
  void Awake() {
    spawner = gameObject.GetComponent<LevelSpawner>();
    spawner.setLevelData(level);
    audio = GameObject.Find("AudioManagerBGM").GetComponent<AudioManagerBGM>();
  }
  void Start() {
    audio.ChangeBGM("MenuTheme");
  }
  void Update() {
    if (spawner.waveRunning == false && WaveController.startWave == true && WaveController.LevelCleared == false) {
      string name = spawner.findCorrectWaveToStart();
      if (name != null) {
        StartCoroutine(name);
      }
    }
  }
  List<string> basicMobs = new List<string>() { "NanoBasic", "MicroBasic", "KiloBasic", "NanoShield", "MicroShield", "NanoArmored", "MicroArmored", "KiloArmored" };
  List<string> bigMobs = new List<string>() { "Ticker", "Outlier", "Vessel" };
  void BundleSpawn(int number, string name, bool allRandom = true) {
    float x = Random.Range(-5f, 5f);
    for (int i = 0; i < number; i++) {
      if (allRandom) x = Random.Range(-5f, 5f);
      spawner.spawnEnemy(name, x, 10f);
    }
  }
  IEnumerator wave1() {
    BundleSpawn(5, basicMobs[Random.Range(0, basicMobs.Count)], false);
    yield return null;
    spawner.AllTriggerEnemiesCleared();
  }

  IEnumerator wave2() {
    BundleSpawn(2, bigMobs[Random.Range(0, bigMobs.Count)]);
    yield return null;
    spawner.AllEnemiesCleared();
  }
  IEnumerator wave3() {
    spawner.spawnEnemyInMap("Core", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(60f);
    spawner.waveCleared();
  }
  IEnumerator wave4() {
    int i = 4;
    while (i > 0) {
      i--;
      BundleSpawn(10, basicMobs[Random.Range(0, basicMobs.Count)]);
      yield return new WaitForSeconds(15f);
    }
    yield return null;
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave5() {
    while (true) {
      int ranMob = Random.Range(0, 2);
      int ranDistribution = Random.Range(2, 30);
      if (spawner.setEnemies[0] == null) break;
      if (ranMob == 0) {
        BundleSpawn(ranDistribution, basicMobs[Random.Range(0, basicMobs.Count)]);
        yield return new WaitForSeconds(30f);
      }
      if (ranMob == 1) {
        BundleSpawn(1, bigMobs[Random.Range(0, bigMobs.Count)]);
        yield return new WaitForSeconds(40f);
      } else {
        yield return null;
      }
    }
    spawner.LastWaveEnemiesCleared();
  }
}
