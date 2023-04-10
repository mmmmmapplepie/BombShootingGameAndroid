using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandbox : MonoBehaviour, IGetLevelDataInterface {
  [SerializeField]
  Level level;
  // [SerializeField]
  // GameObject winPanel;
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
  // spawner.spawnEnemyInMap("Core", 0f, 5f, LevelSpawner.addToList.All, true);
  // spawner.spawnEnemyInMap("Carrier", -5f, 8f, LevelSpawner.addToList.All, true);
  // spawner.spawnEnemyInMap("Colossus", 5f, 8f, LevelSpawner.addToList.All, true);
  // spawner.spawnEnemyInMap("Leviathan", 1.5f, 8f, LevelSpawner.addToList.All, true);
  // spawner.spawnEnemyInMap("HyperCore", -1.5f, 8f, LevelSpawner.addToList.All, true);
  int totalEnemies = 1;
  while (totalEnemies > 0) {
    totalEnemies--;
    float x = spawner.randomWithRange(-5f, 5f);
    spawner.spawnEnemy("Vessel", x, 10f, LevelSpawner.addToList.All);
    // x = spawner.randomWithRange(-5f, 5f);
    // spawner.spawnEnemy("MesoVessel", x, 10f, LevelSpawner.addToList.All);
    // x = spawner.randomWithRange(-5f, 5f);
    // spawner.spawnEnemy("MacroVessel", x, 10f, LevelSpawner.addToList.All);
    // x = spawner.randomWithRange(-5f, 5f);
    // spawner.spawnEnemy("HyperVessel", x, 10f, LevelSpawner.addToList.All);
    yield return new WaitForSeconds(0.1f);
  }
  // yield return new WaitForSeconds(5f);
  spawner.AllTriggerEnemiesCleared();
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
