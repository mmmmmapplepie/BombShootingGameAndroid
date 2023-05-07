using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L4 : MonoBehaviour, IGetLevelDataInterface {
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
    int enemies = 5;
    StartCoroutine(wave1_1());
    wave1Pattern1(enemies, "NanoBasic");
    yield return new WaitForSeconds(25f);
    wave1Pattern1(enemies, "NanoBasic");
    yield return new WaitForSeconds(25f);
    wave1Pattern1(enemies, "NanoBasic");
    yield return null;
    spawner.AllTriggerEnemiesCleared();
  }
  void wave1Pattern1(int enemies, string enemyname) {
    float x;
    float side = spawner.randomWithRange(-1f, 1f);
    for (int i = 0; i < enemies; i++) {
      if (side > 0f) {
        x = spawner.randomWithRange(4.5f, 5f);
      } else {
        x = spawner.randomWithRange(-4.5f, -5f);
      }
      spawner.spawnEnemy(enemyname, x, 10f, LevelSpawner.addToList.All);
    }
  }
  IEnumerator wave1_1() {
    yield return new WaitForSeconds(5f);
    float time = Time.time;
    float period = 45f;
    float x;
    int counter = 0;
    while (Time.time < time + period) {
      counter++;
      x = spawner.randomWithRange(-5f, 0f);
      spawner.spawnEnemy("MicroBasic", x, 10f, LevelSpawner.addToList.All);
      if ((counter) % 5 == 0) {
        x = spawner.randomWithRange(-5f, 0f);
        spawner.spawnEnemy("Shifter", x, 10f, LevelSpawner.addToList.All);
      }
      yield return new WaitForSeconds(9f);
    }
  }

  IEnumerator wave2() {
    wave1Pattern1(2, "MicroBasic");
    yield return new WaitForSeconds(15f);
    wave1Pattern1(2, "MicroBasic");
    yield return new WaitForSeconds(15f);
    wave1Pattern1(1, "Shifter");
    spawner.LastWaveEnemiesCleared();
  }
}
