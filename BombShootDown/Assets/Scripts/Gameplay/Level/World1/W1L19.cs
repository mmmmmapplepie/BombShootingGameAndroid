using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L19 : MonoBehaviour, IGetLevelDataInterface {
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
    audio.ChangeBGM("World1");
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
    int i = 20;
    float x = spawner.randomWithRange(-5f, 5f);
    spawner.spawnEnemy("Ticker", x, 10f, LevelSpawner.addToList.All);
    while (i > 0) {
      x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("NanoArmored", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(0.3f);
      i--;
    }
    yield return null;
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    int i = 10;
    while (i > 0) {
      i--;
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("MesoEnigma", x, 10f, LevelSpawner.addToList.All);
      float rantime = Random.Range(3f, 4f);
      yield return new WaitForSeconds(rantime);
    }
    yield return new WaitForSeconds(10f);
    spawner.spawnEnemy("MesoEnigma", 5f, 10f, LevelSpawner.addToList.All);
    spawner.spawnEnemy("MesoEnigma", 2.5f, 10f, LevelSpawner.addToList.All);
    spawner.spawnEnemy("MesoEnigma", 0f, 10f, LevelSpawner.addToList.All);
    spawner.spawnEnemy("MesoEnigma", -2.5f, 10f, LevelSpawner.addToList.All);
    spawner.spawnEnemy("MesoEnigma", -5f, 10f, LevelSpawner.addToList.All);
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave3() {
    int i = 20;
    while (i > 0) {
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("KiloBasic", x, 10f, LevelSpawner.addToList.All);
      if (i % 5 == 0) {
        x = spawner.randomWithRange(-5f, 5f);
        spawner.spawnEnemy("MesoShifter", x, 10f, LevelSpawner.addToList.All);
      }
      i--;
      yield return new WaitForSeconds(1f);
    }
    yield return null;
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave4() {
    int i = 3;
    while (i > 0) {
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("MesoOutlier", x, 10f, LevelSpawner.addToList.All, true);
      if (i == 3) {
        StartCoroutine(wave4_4());
      }
      i--;
      yield return new WaitForSeconds(30f);

    }
    spawner.LastWaveEnemiesCleared();
  }
  IEnumerator wave4_4() {
    while (spawner.setEnemies.Count < 3 || spawner.setEnemies[spawner.setEnemies.Count - 1] != null) {
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("MesoShifter", x, 10f, LevelSpawner.addToList.All);
      if (spawner.setEnemies.Count == 3 && spawner.setEnemies[spawner.setEnemies.Count - 1] == null) break;
      yield return new WaitForSeconds(4f);
    }
  }
}
