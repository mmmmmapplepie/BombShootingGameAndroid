using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L8 : MonoBehaviour, IGetLevelDataInterface {
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
    spawner.spawnEnemy("Teleporter", 0, 10f, LevelSpawner.addToList.All);
    yield return new WaitForSeconds(10f);
    spawner.spawnEnemy("Teleporter", 0, 10f, LevelSpawner.addToList.All);
    yield return new WaitForSeconds(15f);
    spawner.spawnEnemy("Teleporter", 0, 10f, LevelSpawner.addToList.All);
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    for (int i = 0; i < 2; i++) {
      if (i == 0) {
        spawner.spawnEnemy("MicroBasic", 5f, 10f, LevelSpawner.addToList.All);
        spawner.spawnEnemy("MicroBasic", -5f, 10f, LevelSpawner.addToList.All);
        spawner.spawnEnemy("Teleporter", 0, 10f, LevelSpawner.addToList.All);
        yield return new WaitForSeconds(5f);
        spawner.spawnEnemy("Shifter", 0, 10f, LevelSpawner.addToList.All);
        yield return new WaitForSeconds(5f);
        spawner.spawnEnemy("Zipper", 0, 10f, LevelSpawner.addToList.All);
      } else {
        spawner.spawnEnemy("MicroBasic", 5f, 10f, LevelSpawner.addToList.All);
        spawner.spawnEnemy("MicroBasic", -5f, 10f, LevelSpawner.addToList.All);
        spawner.spawnEnemy("Teleporter", 0, 10f, LevelSpawner.addToList.All);
        yield return new WaitForSeconds(5f);
        spawner.spawnEnemy("Teleporter", 0, 10f, LevelSpawner.addToList.All);
        yield return new WaitForSeconds(5f);
        spawner.spawnEnemy("Teleporter", 0, 10f, LevelSpawner.addToList.All);
      }
    }
    spawner.LastWaveEnemiesCleared();
  }
}
