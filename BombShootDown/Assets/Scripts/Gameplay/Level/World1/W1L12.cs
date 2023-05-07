using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L12 : MonoBehaviour, IGetLevelDataInterface {
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
    for (int i = 0; i < 4; i++) {
      float y = Random.Range(5f, 10f);
      spawner.spawnEnemyInMap("Shifter", 0f, y, false, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(5f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    for (int i = 0; i < 4; i++) {
      float y = Random.Range(5f, 10f);
      spawner.spawnEnemyInMap("Teleporter", 0f, y, true, LevelSpawner.addToList.All);
      y = Random.Range(5f, 10f);
      spawner.spawnEnemyInMap("Teleporter", 0f, y, true, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(5f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave3() {
    for (int i = 0; i < 3; i++) {
      float y = Random.Range(8f, 10f);
      spawner.spawnEnemyInMap("Zipper", 0f, y, false, LevelSpawner.addToList.All);
      y = Random.Range(8f, 10f);
      spawner.spawnEnemyInMap("Zipper", 0f, y, false, LevelSpawner.addToList.All);
      y = Random.Range(8f, 10f);
      spawner.spawnEnemyInMap("Zipper", 0f, y, false, LevelSpawner.addToList.All);
      y = Random.Range(8f, 10f);
      spawner.spawnEnemyInMap("Zipper", 0f, y, false, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(5f);
    }
    spawner.LastWaveEnemiesCleared();
  }
}
