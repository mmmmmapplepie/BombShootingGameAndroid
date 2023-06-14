using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandbox : MonoBehaviour, IGetLevelDataInterface {
  [SerializeField]
  Level level;
  LevelSpawner spawner;
  new AudioManagerBGM audio;
  public Level GetLevelData() {
    return level;
  }
  void Awake() {
    UpgradesManager.loadAllData();
    spawner = gameObject.GetComponent<LevelSpawner>();
    spawner.setLevelData(level);
    audio = GameObject.Find("AudioManagerBGM").GetComponent<AudioManagerBGM>();
  }
  void Start() {
    audio.ChangeBGM("MenuTheme");
    // StartCoroutine("wave1");
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
    int totalEnemies = 1;
    while (totalEnemies > 0) {
      totalEnemies--;
      // float x = spawner.randomWithRange(-5f, 5f);
      // spawner.spawnEnemy("Core", x, 10f, LevelSpawner.addToList.All);
      // spawner.spawnEnemy("CoupladFollower", -3f, 8f, LevelSpawner.addToList.All);
      // spawner.spawnEnemy("CoupladSeeker", 3f, 8f, LevelSpawner.addToList.All);
      // spawner.spawnEnemy("MaxCoupladFollower", -3f, 8f, LevelSpawner.addToList.All);
      // spawner.spawnEnemy("MaxCoupladSeeker", 3f, 8f, LevelSpawner.addToList.All);
      spawner.spawnEnemy("Ernesto", 1f, 8f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(1f);
      spawner.spawnEnemy("KiloBasic", 1f, 8f, LevelSpawner.addToList.All);
      spawner.spawnEnemy("KiloBasic", 1f, 8f, LevelSpawner.addToList.All);
      spawner.spawnEnemy("KiloBasic", 1f, 8f, LevelSpawner.addToList.All);
    }
    // yield return new WaitForSeconds(5f);
    spawner.AllTriggerEnemiesCleared();
    yield return null;
  }
  IEnumerator wave2() {
    float x = spawner.randomWithRange(-5f, 5f);
    spawner.spawnEnemy("Vessel", x, 10f, LevelSpawner.addToList.All);
    yield return null;
    spawner.LastWaveEnemiesCleared();
  }
  //   // StartCoroutine("EndLevel");
  //   yield return null;
  // }
  // IEnumerator EndLevel() {
  //   while (spawner.AllWaveTriggerEnemies.Count > 0) {
  //     yield return null;
  //   }
  //   yield return new WaitForSeconds(1f);
  //   winPanel.SetActive(true);
  // }
}
