using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L24 : MonoBehaviour, IGetLevelDataInterface {
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
    int i = 5;
    while (i > 0) {
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("Zipper", x, 10f, LevelSpawner.addToList.All);
      i--;
      yield return new WaitForSeconds(2f);
    }
    yield return null;
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    StartCoroutine(wave2_Pattern());
    yield return new WaitForSeconds(10f);
    spawner.spawnEnemyInMap("Jammer", 0f, 3f, true, LevelSpawner.addToList.All);
    spawner.waveCleared();
  }
  IEnumerator wave2_Pattern() {
    int i = 10;
    float x = spawner.randomWithRange(-5f, 5f);
    while (i > 0) {
      i--;
      spawner.spawnEnemy("NanoBasic", x, 10f, LevelSpawner.addToList.All);
    }
    yield return new WaitForSeconds(5f);
    i = 20;
    x = spawner.randomWithRange(-5f, 5f);
    while (i > 0) {
      i--;
      spawner.spawnEnemy("NanoBasic", x, 10f, LevelSpawner.addToList.All);
    }
  }
  IEnumerator wave3() {
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemy("Vessel", -3f, 10f, LevelSpawner.addToList.All);
    spawner.spawnEnemy("Vessel", 0f, 10f, LevelSpawner.addToList.All);
    spawner.spawnEnemy("Vessel", 3f, 10f, LevelSpawner.addToList.All);
    yield return new WaitForSeconds(30f);
    spawner.waveCleared();
  }
  IEnumerator wave4() {
    for (int k = 0; k < 5; k++) {
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("MesoTeleporter", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(0.5f);
    }
    while (spawner.AllWaveTriggerEnemies.Count > 0) {
      yield return null;
    }
    yield return new WaitForSeconds(1f);
    spawner.AllTriggerEnemiesCleared();
  }
}
