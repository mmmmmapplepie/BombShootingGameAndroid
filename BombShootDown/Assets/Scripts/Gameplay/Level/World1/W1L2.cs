using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L2 : MonoBehaviour, IGetLevelDataInterface {
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
    spawner.setLevelData(level);
    GameObject.Find("AudioManagerBGM").GetComponent<AudioManagerBGM>().ChangeBGM("World1");
  }
  void Update() {
    if (spawner.waveRunning == false && WaveController.startWave == true && WaveController.LevelCleared == false) {
      string name = spawner.findCorrectWaveToStart();
      if (name != null) {
        StartCoroutine(name);
      }
    }
  }
  #region LevelDesign
  IEnumerator wave1() {
    int totalEnemies = 5;
    while (totalEnemies > 0) {
      totalEnemies--;
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("NanoBasic", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(2f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    int totalEnemies = 10;
    while (totalEnemies > 0) {
      totalEnemies--;
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("NanoBasic", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(1f);
    }
    spawner.LastWaveEnemiesCleared();
  }
  #endregion
}
