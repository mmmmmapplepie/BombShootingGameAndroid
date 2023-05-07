using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L7 : MonoBehaviour, IGetLevelDataInterface {
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
    int i = 0;
    while (i < 8) {
      if (i % 4 == 0) {
        float x = spawner.randomWithRange(-5f, 5f);
        spawner.spawnEnemy("NanoBasic", x, 10f, LevelSpawner.addToList.All);
        yield return new WaitForSeconds(4f);
      }
      if (i % 4 == 1) {
        float x = spawner.randomWithRange(-5f, 5f);
        spawner.spawnEnemy("NanoShield", x, 10f, LevelSpawner.addToList.All);
        yield return new WaitForSeconds(6f);
      }
      if (i % 4 == 2) {
        float x = spawner.randomWithRange(-5f, 5f);
        spawner.spawnEnemy("KiloBasic", x, 10f, LevelSpawner.addToList.All);
        yield return new WaitForSeconds(10f);
      }
      if (i % 4 == 3) {
        float x = spawner.randomWithRange(-5f, 5f);
        spawner.spawnEnemy("Shifter", x, 10f, LevelSpawner.addToList.All);
        yield return new WaitForSeconds(2f);
      }
      i++;
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    spawner.spawnEnemy("MegaShield", 0f, 10f, LevelSpawner.addToList.All);
    yield return null;
    spawner.LastWaveEnemiesCleared();
  }
}
