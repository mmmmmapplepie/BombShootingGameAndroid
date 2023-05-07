using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L13 : MonoBehaviour, IGetLevelDataInterface {
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
    int i = 20;
    while (i > 0) {
      if (i == 15) spawner.waveCleared();
      float x = spawner.randomWithRange(2.5f, 5f);
      spawner.spawnEnemy("Enigma", x, 10f, LevelSpawner.addToList.All);
      i--;
      yield return new WaitForSeconds(5f);
    }
    wave1Done = true;
  }
  IEnumerator wave2() {
    int i = 15;
    while (i > 0) {
      float x = spawner.randomWithRange(-2.5f, -5f);
      spawner.spawnEnemy("KiloBasic", x, 10f, LevelSpawner.addToList.All);
      spawner.spawnEnemy("MicroBasic", x, 10f, LevelSpawner.addToList.All);
      i--;
      if (i == 10) spawner.waveCleared();
      yield return new WaitForSeconds(5f);
    }
  }

  IEnumerator wave3() {
    int i = 5;
    while (i > 0) {
      spawner.spawnEnemy("Teleporter", -5f, 10f, LevelSpawner.addToList.All);
      i--;
      yield return new WaitForSeconds(10f);
    }
    while (wave1Done == false) yield return null;
    spawner.LastWaveEnemiesCleared();
  }
}
