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
    spawner.spawnEnemy("Shifter", -5f, 10f, LevelSpawner.addToList.All);
    spawner.spawnEnemy("KiloShield", -2f, 10f, LevelSpawner.addToList.All);
    spawner.spawnEnemy("UltimateShield", 2f, 10f, LevelSpawner.addToList.All);
    spawner.spawnEnemy("UltimateArmored", 5f, 10f, LevelSpawner.addToList.All);
    spawner.spawnEnemy("UltimateBasic", 0f, 10f, LevelSpawner.addToList.All);
    // StartCoroutine("EndLevel");
    yield return null;
  }
  // IEnumerator EndLevel() {
  //   while (spawner.AllWaveTriggerEnemies.Count > 0) {
  //     yield return null;
  //   }
  //   yield return new WaitForSeconds(1f);
  //   winPanel.SetActive(true);
  // }
}
