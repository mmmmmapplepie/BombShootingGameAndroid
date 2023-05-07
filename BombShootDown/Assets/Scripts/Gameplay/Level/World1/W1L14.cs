using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L14 : MonoBehaviour, IGetLevelDataInterface {
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
    spawner.spawnEnemyInMap("Outlier", 0f, 10f, false, LevelSpawner.addToList.All);
    int i = 5;
    yield return new WaitForSeconds(20f);
    spawner.waveCleared();
    while (i > 0) {
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("Outlier", x, 10f, LevelSpawner.addToList.None);
      i--;
      yield return new WaitForSeconds(25f);
    }
    wave1Done = true;
  }
  IEnumerator wave2() {
    float x = spawner.randomWithRange(-5f, 5f);
    spawner.spawnEnemy("Reflector", x, 10f, LevelSpawner.addToList.All);
    yield return null;
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave3() {
    int i = 2;
    while (i > 0) {
      wave3Pattern(8, "Enigma");
      i--;
      yield return new WaitForSeconds(15f);
    }
    while (!wave1Done) yield return null;
    spawner.LastWaveEnemiesCleared();
  }
  void wave3Pattern(int enemies, string enename) {
    for (int i = 0; i < enemies; i++) {
      float x = Random.Range(-5f, 5f);
      spawner.spawnEnemyInMap(enename, x, 9f, false, LevelSpawner.addToList.All);
    }
  }
}
