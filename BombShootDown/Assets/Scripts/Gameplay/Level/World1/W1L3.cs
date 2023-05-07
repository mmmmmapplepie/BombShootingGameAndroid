using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L3 : MonoBehaviour, IGetLevelDataInterface {
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
      spawner.spawnEnemy("NanoBasic", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(0.5f);
    }
    yield return null;
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    int i = 5;
    while (i > 0) {
      i--;
      float x;
      for (int k = 0; k < i; k++) {
        x = spawner.randomWithRange(-5f, 5f);
        spawner.spawnEnemy("NanoBasic", x, 10f, LevelSpawner.addToList.All);
        yield return new WaitForSeconds(0.2f);
      }
      x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("MicroBasic", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(2f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
