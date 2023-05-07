using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L9 : MonoBehaviour, IGetLevelDataInterface {
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
    int i = 2;
    while (i > 0) {
      i--;
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("MicroBasic", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(5f);
      x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("MesoShifter", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(10f);
    }
    yield return null;
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    float x = spawner.randomWithRange(-3f, 3f);
    spawner.spawnEnemy("Ticker", x, 10f, LevelSpawner.addToList.All);
    yield return new WaitForSeconds(35f);
    spawner.spawnEnemy("Ticker", 5, 10f, LevelSpawner.addToList.All);
    yield return null;
    for (int i = 0; i < 10; i++) {
      x = spawner.randomWithRange(-5f, 0f);
      spawner.spawnEnemy("MicroBasic", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(3.5f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
