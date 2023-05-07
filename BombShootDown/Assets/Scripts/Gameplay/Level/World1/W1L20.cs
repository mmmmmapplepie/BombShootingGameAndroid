using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L20 : MonoBehaviour, IGetLevelDataInterface {
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
  List<string> mobs = new List<string>() { "Teleporter", "KiloShield" };
  IEnumerator wave1() {
    int i = 20;
    while (i > 0) {
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy(mobs[Random.Range(0, 2)], x, 10f);
      i--;
      yield return new WaitForSeconds(1f);
    }
    spawner.waveCleared();
  }
  IEnumerator wave2() {
    spawner.spawnEnemyInMap("Carrier", 0, 11f, true, LevelSpawner.addToList.Specific, true);
    yield return null;
    spawner.LastWaveEnemiesCleared();
  }
}
