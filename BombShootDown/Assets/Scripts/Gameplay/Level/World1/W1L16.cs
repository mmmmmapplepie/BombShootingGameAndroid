using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L16 : MonoBehaviour, IGetLevelDataInterface {
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
  bool wave1Done = false;
  IEnumerator wave1() {
    spawner.spawnEnemyInMap("Ticker", 0f, 10f, true, LevelSpawner.addToList.All);
    int i = 4;
    yield return new WaitForSeconds(15f);
    spawner.waveCleared();
    while (i > 0) {
      float x = spawner.randomWithRange(0f, 5f);
      spawner.spawnEnemyInMap("Ticker", x, 8f, true, LevelSpawner.addToList.All);
      x = spawner.randomWithRange(0f, -5f);
      spawner.spawnEnemyInMap("Outlier", x, 8f, false, LevelSpawner.addToList.All);
      i--;
      yield return new WaitForSeconds(35f);
    }
    wave1Done = true;
  }
  IEnumerator wave2() {
    StartCoroutine(wave2_Clear());
    while (!wave1Done) {
      int ran = Random.Range(0, 3);
      float x = spawner.randomWithRange(0f, 5f);
      if (ran == 0) {
        spawner.spawnEnemy("Zipper", x, 8f, LevelSpawner.addToList.All);
      }
      if (ran == 1) {
        spawner.spawnEnemy("MicroBasic", x, 8f, LevelSpawner.addToList.All);
      }
      if (ran == 2) {
        spawner.spawnEnemy("MicroArmored", x, 8f, LevelSpawner.addToList.All);
      }
      yield return new WaitForSeconds(10f);
    }
  }
  IEnumerator wave2_Clear() {
    yield return new WaitForSeconds(30f);
    spawner.waveCleared();
  }
  IEnumerator wave3() {
    int i = 4;
    while (i > 0) {
      wave3Pattern(10, "MicroBasic");
      i--;
      yield return new WaitForSeconds(20f);
    }
    while (!wave1Done) yield return null;
    spawner.LastWaveEnemiesCleared();
  }
  void wave3Pattern(int enemies, string enename) {
    for (int i = 0; i < enemies; i++) {
      float x = Random.Range(-5f, 5f);
      spawner.spawnEnemyInMap(enename, x, 9f, false, LevelSpawner.addToList.All);
    }
  }
}
