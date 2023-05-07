using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L10 : MonoBehaviour, IGetLevelDataInterface {
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
    while (i > 0) {
      i--;
      float x = spawner.randomWithRange(-5f, 5f);
      if (i % 2 == 0) spawner.spawnEnemy("NanoBasic", x, 10f, LevelSpawner.addToList.All);
      if (i % 2 == 1) spawner.spawnEnemy("NanoArmored", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(0.7f);
    }
    yield return null;
    spawner.waveCleared();
  }
  IEnumerator wave2() {
    int i = 20;
    while (i > 0) {
      float x = spawner.randomWithRange(-5f, 5f);
      if (i % 2 == 0) spawner.spawnEnemy("NanoArmored", x, 10f, LevelSpawner.addToList.All);
      if (i % 2 == 1) spawner.spawnEnemy("NanoBasic", x, 10f, LevelSpawner.addToList.All);
      i--;
      yield return new WaitForSeconds(0.5f);
    }
    yield return null;
    spawner.waveCleared();
  }
  IEnumerator wave3() {
    spawner.spawnEnemyInMap("GigaBasic", 0, 10f, true, LevelSpawner.addToList.Specific);
    BowManager.EnemySpeed = 0.5f;
    int i = 9;
    while (i > 0) {
      float x = spawner.randomWithRange(-5f, 5f);
      if (i % 3 == 0) spawner.spawnEnemy("MicroShield", x, 10f, LevelSpawner.addToList.All);
      if (i % 3 == 1) spawner.spawnEnemy("KiloBasic", x, 10f, LevelSpawner.addToList.All);
      if (i % 3 == 2) spawner.spawnEnemy("MicroArmored", x, 10f, LevelSpawner.addToList.All);
      i--;
      yield return new WaitForSeconds(1f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave4() {
    yield return new WaitForSeconds(20f);
    StartCoroutine(wave4_4());
    yield return new WaitForSeconds(10f);
    StartCoroutine(wave4_4());
    yield return new WaitForSeconds(4f);
    BowManager.EnemySpeed = 1f;
    spawner.LastWaveEnemiesCleared();
  }
  IEnumerator wave4_4() {
    wave4_4Pattern(-5f);
    yield return new WaitForSeconds(1f);
    wave4_4Pattern(0f);
    yield return new WaitForSeconds(1f);
    wave4_4Pattern(5f);
  }
  void wave4_4Pattern(float xpos) {
    int ran = Random.Range(0, 3);
    if (ran % 3 == 0) {
      spawner.spawnEnemyInMap("Teleporter", xpos, 8f, false, LevelSpawner.addToList.All);
    }
    if (ran % 3 == 1) {
      spawner.spawnEnemyInMap("MesoZipper", xpos, 8f, false, LevelSpawner.addToList.All);
    }
    if (ran % 3 == 2) {
      spawner.spawnEnemyInMap("Zipper", xpos, 8f, false, LevelSpawner.addToList.All);
    }
  }
}
