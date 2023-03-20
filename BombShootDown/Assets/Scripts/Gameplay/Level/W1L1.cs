using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L1 : MonoBehaviour, IGetLevelDataInterface {
  [SerializeField]
  Level level;
  // [SerializeField]
  // spawning animation prefab spawnEffect;
  LevelSpawner spawner;

  public Level GetLevelData() {
    return level;
  }
  void Awake() {
    spawner = gameObject.GetComponent<LevelSpawner>();
  }
  void Update() {
    if (spawner.waveRunning == false && WaveController.startWave == true) {
      string name = spawner.findCorrectWaveToStart();
      if (name != null) {
        StartCoroutine(name);
      }
    }
  }
  #region LevelDesign
  IEnumerator wave1() {
    int totalEnemies = 20;
    while (totalEnemies > 0) {
      totalEnemies--;
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("NanoBasic", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(0.1f);
    }
    StartCoroutine(spawner.AllTriggerEnemiesCleared());
  }
  IEnumerator wave2() {
    int totalEnemies = 1000;
    while (totalEnemies > 0) {
      totalEnemies--;
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("NanoBasic", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(0.1f);
    }
    StartCoroutine(spawner.LastWaveEnemiesCleared());
  }
  #endregion
}
