using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L1test : LevelSpawnerBase {
  // [SerializeField]
  // spawning animation prefab spawnEffect;
  void Update() {
    cleanWaveLists();
  }
  void Start() {
    StartCoroutine("wave1");
  }
  IEnumerator wave1() {
    int totalEnemies = 5;
    while (totalEnemies > 0) {
      totalEnemies--;
      float x = randomWithRange(-5f, 5f);
      spawnEnemy("NanoBasic", x, 10f, addToList.All);
      yield return new WaitForSeconds(3f);
    }
    StartCoroutine("WaveTriggerEnemiesCleared");
  }
}
