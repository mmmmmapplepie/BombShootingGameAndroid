using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L15 : MonoBehaviour, IGetLevelDataInterface {
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
    int i = 10;
    while (i > 0) {
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("KiloShield", x, 10f, LevelSpawner.addToList.All);
      i--;
      yield return new WaitForSeconds(3f);
    }
    i = 20;
    while (i > 0) {
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("KiloBasic", x, 10f, LevelSpawner.addToList.All);
      i--;
      yield return new WaitForSeconds(1.5f);
    }
    spawner.waveCleared();
  }
  IEnumerator wave2() {
    int i = 20;
    while (i > 0) {
      float x;
      x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("KiloArmored", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(0.5f);
      i--;
    }
    spawner.LastWaveEnemiesCleared();
  }
}
