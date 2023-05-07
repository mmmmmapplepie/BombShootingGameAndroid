using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L11 : MonoBehaviour, IGetLevelDataInterface {
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
    int enemies = 3;
    for (int i = 0; i < 4; i++) {
      wave1Pattern(enemies, new List<string>() { "MicroShield", "NanoBasic" });
      enemies += 2;
      yield return new WaitForSeconds(5f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  void wave1Pattern(int enemies, List<string> enemiesNames) {
    for (int i = 0; i < enemies; i++) {
      float x = spawner.randomWithRange(-5f, 5f);
      int ran = Random.Range(0, enemiesNames.Count);
      spawner.spawnEnemy(enemiesNames[ran % enemiesNames.Count], x, 10f, LevelSpawner.addToList.All);
    }
  }
  IEnumerator wave2() {
    int enemies = 3;
    for (int i = 0; i < 6; i++) {
      if (i % 2 == 0) {
        wave1Pattern(enemies, new List<string>() { "MicroShield", "KiloShield" });
      } else {
        wave1Pattern(enemies, new List<string>() { "KiloBasic", "MesoShifter" });
        enemies += 1;
      }
      yield return new WaitForSeconds(12f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
