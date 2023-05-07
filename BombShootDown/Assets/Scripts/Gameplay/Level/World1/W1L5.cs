using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class W1L5 : MonoBehaviour, IGetLevelDataInterface {
  [SerializeField]
  Level level;
  LevelSpawner spawner;
  new AudioManagerBGM audio;
  bool wave1Done = false;
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
    StartCoroutine(Wave1_1());
    while (i > 0) {
      i--;
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("MicroBasic", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(10f);
    }
    while (wave1Done == false) {
      yield return null;
    }
    spawner.LastWaveEnemiesCleared();
  }
  IEnumerator Wave1_1() {
    yield return new WaitForSeconds(10f);
    int counter = 0;
    while (counter < 9) {
      counter++;
      if (counter % 3 == 0) {
        spawner.spawnEnemy("Zipper", 0, 10f, LevelSpawner.addToList.All);
        yield return new WaitForSeconds(10f);
      } else {
        spawner.spawnEnemy("Shifter", -5f, 10f, LevelSpawner.addToList.All);
        spawner.spawnEnemy("Shifter", 5f, 10f, LevelSpawner.addToList.All);
        yield return new WaitForSeconds(15f);
      }
    }
    wave1Done = true;
  }
  void wave1Pattern1(int enemies, string enemyname) {
    float x;
    for (int i = 0; i < enemies; i++) {
      x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy(enemyname, x, 10f, LevelSpawner.addToList.All);
    }
  }
}
