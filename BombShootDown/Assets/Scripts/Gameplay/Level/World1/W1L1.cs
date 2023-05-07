using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L1 : MonoBehaviour, IGetLevelDataInterface {
  [SerializeField]
  Level level;
  [SerializeField]
  GameObject winPanel;
  // [SerializeField]
  // spawning animation prefab spawnEffect;
  LevelSpawner spawner;
  new AudioManagerBGM audio;
  public Level GetLevelData() {
    return level;
  }
  void Awake() {
    spawner = gameObject.GetComponent<LevelSpawner>();
    spawner.setLevelData(level);
    audio = GameObject.Find("AudioManagerBGM").GetComponent<AudioManagerBGM>();
    audio.ChangeBGM("World1");
  }

  void Start() {
    StartCoroutine("wave1");
  }
  IEnumerator wave1() {
    int totalEnemies = 5;
    while (totalEnemies > 0) {
      totalEnemies--;
      spawner.spawnEnemy("NanoBasic", 0f, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(3f);
    }
    StartCoroutine("EndLevel");
  }
  IEnumerator EndLevel() {
    while (spawner.AllWaveTriggerEnemies.Count > 0) {
      yield return null;
    }
    yield return new WaitForSeconds(1f);
    winPanel.SetActive(true);
  }
}
