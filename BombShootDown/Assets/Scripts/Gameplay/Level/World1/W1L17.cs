using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L17 : MonoBehaviour, IGetLevelDataInterface {
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
    int i = 3;
    while (i > 0) {
      i--;
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("Vessel", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(20f);
    }
    yield return null;
    spawner.AllEnemiesCleared();
  }
  IEnumerator wave2() {
    float x = spawner.randomWithRange(-5f, 5f);
    spawner.spawnEnemy("Vessel", x, 10f, LevelSpawner.addToList.All);
    wave2_Pattern();
    yield return new WaitForSeconds(5f);
    wave2_Pattern();
    yield return new WaitForSeconds(5f);
    wave2_Pattern();
    yield return new WaitForSeconds(5f);
    wave2_Pattern();
    yield return new WaitForSeconds(5f);
    wave2_Pattern();
    yield return new WaitForSeconds(10f);
    x = spawner.randomWithRange(-5f, 5f);
    spawner.spawnEnemy("Vessel", x, 10f, LevelSpawner.addToList.All);
    wave2_Pattern();
    yield return new WaitForSeconds(5f);
    wave2_Pattern();
    yield return new WaitForSeconds(5f);
    wave2_Pattern();
    yield return new WaitForSeconds(5f);
    wave2_Pattern();
    yield return new WaitForSeconds(5f);
    wave2_Pattern();
    spawner.LastWaveEnemiesCleared();
  }
  void wave2_Pattern() {
    float x;
    for (int k = 0; k < 10; k++) {
      x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("NanoShield", x, 10f, LevelSpawner.addToList.All);
    }
  }
}
