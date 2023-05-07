using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L21 : MonoBehaviour, IGetLevelDataInterface {
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
    float x = spawner.randomWithRange(-5f, 5f);
    spawner.spawnEnemy("MegaShield", x, 10f);
    yield return null;
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    for (int i = 0; i < 4; i++) {
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("MegaShield", x, 10f);
      yield return new WaitForSeconds(5f);
    }
    yield return new WaitForSeconds(10f);
    for (int i = 0; i < 4; i++) {
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("MegaShield", x, 10f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave3() {
    int i = 0;
    while (i < 2) {
      StartCoroutine(wave3_Pattern(i + 1));
      i++;
      yield return new WaitForSeconds(30f);
    }
    spawner.LastWaveEnemiesCleared();
  }
  IEnumerator wave3_Pattern(int i) {
    for (int k = i; k > 0; k--) {
      float x = spawner.randomWithRange(-1f, 1f);
      spawner.spawnEnemyInMap("MesoTicker", x, 10f, true);
    }
    yield return new WaitForSeconds(10f);
    spawner.spawnEnemy("MegaShield", -2.5f, 10f);
    spawner.spawnEnemy("MegaShield", -0.5f, 10f);
    spawner.spawnEnemy("MegaShield", 0.5f, 10f);
    spawner.spawnEnemy("MegaShield", 2.5f, 10f);
  }
}
