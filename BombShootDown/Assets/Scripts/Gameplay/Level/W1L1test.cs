using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L1test : LevelSpawnerBase {
  [SerializeField]
  GameObject winPanel;
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
      spawnEnemyInMap("NanoBasic", x, 0f, addToList.All, false);
      print(AllWaveTriggerEnemies.Count);

      yield return new WaitForSeconds(3f);
    }
    StartCoroutine("EndLevel");
    print("WaveEndedTechnically");
  }
  IEnumerator EndLevel() {
    while (AllWaveTriggerEnemies.Count > 0) {
      yield return null;
    }
    winPanel.SetActive(true);
  }
}
