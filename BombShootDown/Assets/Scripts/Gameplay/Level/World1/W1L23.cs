using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L23 : MonoBehaviour, IGetLevelDataInterface {
  [SerializeField]
  Level level;
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
    audio.ChangeBGM("World1");
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
    spawner.spawnEnemy("MegaBasic", -5f, 10f);
    yield return new WaitForSeconds(5f);
    spawner.spawnEnemy("GigaBasic", 0f, 10f);
    yield return new WaitForSeconds(15f);
    spawner.spawnEnemy("UltimateBasic", 5f, 10f);
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    spawner.spawnEnemyInMap("Disruptor", 0f, 10f, true, LevelSpawner.addToList.Specific, true);
    yield return new WaitForSeconds(5f);
    while (true) {
      if (spawner.setEnemies[0] == null) break;
      int ran = Random.Range(0, 3);
      if (ran == 0) {
        float x = Random.Range(-5f, 5f);
        spawner.spawnEnemy("MegaBasic", x, 10f);
        yield return new WaitForSeconds(15f);
      }
      if (ran == 1) {
        float x = Random.Range(-5f, 5f);
        spawner.spawnEnemy("GigaBasic", x, 10f);
        yield return new WaitForSeconds(25f);
      }
      if (ran == 2) {
        float x = Random.Range(-5f, 5f);
        spawner.spawnEnemy("UltimateBasic", x, 10f);
        yield return new WaitForSeconds(35f);
      }
    }
    spawner.LastWaveEnemiesCleared();
  }
}
