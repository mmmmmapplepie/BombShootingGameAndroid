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
    StartCoroutine("wave1");
  }
  IEnumerator wave1() {
    int totalEnemies = 3;
    while (totalEnemies > 0) {
      totalEnemies--;
      float x = spawner.randomWithRange(-5f, 5f);
      // spawner.spawnEnemy("Vessel", x, 10f, LevelSpawner.addToList.All);
      // x = spawner.randomWithRange(-5f, 5f);
      // spawner.spawnEnemy("MesoVessel", x, 10f, LevelSpawner.addToList.All);
      // x = spawner.randomWithRange(-5f, 5f);
      // spawner.spawnEnemy("MacroVessel", x, 10f, LevelSpawner.addToList.All);
      // x = spawner.randomWithRange(-5f, 5f);
      // spawner.spawnEnemy("HyperVessel", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(30f);
    }
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
