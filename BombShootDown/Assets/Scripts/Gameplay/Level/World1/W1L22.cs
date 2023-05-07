using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L22 : MonoBehaviour, IGetLevelDataInterface {
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
  List<string> mobs = new List<string>() { "KiloShield", "KiloArmored" };
  IEnumerator wave1() {
    int i = 10;
    while (i > 0) {
      i--;
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("KiloArmored", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(2f);
    }
    yield return null;
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    int i = 20;
    while (i > 0) {
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy(mobs[Random.Range(0, 2)], x, 10f, LevelSpawner.addToList.All);
      i--;
      yield return new WaitForSeconds(0.5f);
    }
    yield return null;
    spawner.AllTriggerEnemiesCleared();
  }
  bool protectorSpawned = false;
  IEnumerator wave3() {
    float time = Time.time;
    while (true) {
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy(mobs[Random.Range(0, 2)], x, 10f, LevelSpawner.addToList.All);
      if (Time.time - time > 0f && protectorSpawned == false) {
        protectorSpawned = true;
        spawner.spawnEnemyInMap("Protector", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
      }
      if (protectorSpawned && spawner.setEnemies.Count > 0) {
        if (spawner.setEnemies[0] == null) break;
      }
      yield return new WaitForSeconds(2f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
