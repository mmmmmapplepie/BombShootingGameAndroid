using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L6 : MonoBehaviour, IGetLevelDataInterface {
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
    int i = 15;
    while (i > 0) {
      i--;
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("NanoBasic", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(1.5f);
    }
    yield return null;
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    int i = 10;
    float x;
    while (i > 0) {
      i--;
      x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("MicroBasic", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(0.5f);
    }
    yield return new WaitForSeconds(10f);
    i = 15;
    while (i > 0) {
      i--;
      x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("MicroBasic", x, 10f, LevelSpawner.addToList.None);
      yield return new WaitForSeconds(0.2f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave3() {
    int i = 5;
    float x;
    for (int k = 0; k < i; k++) {
      x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("KiloBasic", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(0.2f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
