using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L1 : LevelSpawnerBase {
  // [SerializeField]
  // spawning animation prefab spawnEffect;
  void Update() {
    cleanWaveLists();
    if (waveRunning == false) {
      findCorrectWaveToStart();
    }
  }
  #region LevelDesign
  IEnumerator wave1() {
    int totalEnemies = 20;
    while (totalEnemies > 0) {
      totalEnemies--;
      float x = randomWithRange(-5f, 5f);
      spawnEnemy("NanoBasic", x, 10f, addToList.All);
      yield return new WaitForSeconds(0.1f);
    }
    StartCoroutine("WaveTriggerEnemiesCleared");
  }
  IEnumerator wave2() {
    int totalEnemies = 1000;
    while (totalEnemies > 0) {
      totalEnemies--;
      float x = randomWithRange(-5f, 5f);
      spawnEnemy("NanoBasic", x, 10f, addToList.All);
      yield return new WaitForSeconds(0.15f);
    }
    StartCoroutine("WaveTriggerEnemiesCleared");
  }
  #endregion
}
