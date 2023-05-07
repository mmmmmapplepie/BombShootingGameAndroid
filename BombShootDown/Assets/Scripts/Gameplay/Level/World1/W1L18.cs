using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L18 : MonoBehaviour, IGetLevelDataInterface {
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
    if (spawner.SpecificWaveTriggerEnemies.Count > 0) {
      boosters[0] = spawner.SpecificWaveTriggerEnemies[0];
      boosters[1] = spawner.SpecificWaveTriggerEnemies[spawner.SpecificWaveTriggerEnemies.Count - 1];
    }
  }
  GameObject[] boosters = new GameObject[2] { null, null };
  IEnumerator wave1() {
    StartCoroutine(wave1_1());
    yield return new WaitForSeconds(10f);
    spawner.spawnEnemyInMap("Booster", 0f, 10f, true, LevelSpawner.addToList.Specific);
    yield return new WaitForSeconds(40f);
    spawner.spawnEnemyInMap("Booster", 0f, 10f, true, LevelSpawner.addToList.Specific);
    yield return new WaitForSeconds(5f);
    spawner.LastWaveEnemiesCleared();
  }
  IEnumerator wave1_1() {
    float time = Time.time;
    List<string> grade1En = new List<string>() { "NanoBasic", "MicroBasic", "MicroShield", "KiloBasic" };
    List<string> grade2En = new List<string>() { "Shifter", "Zipper", "MesoShifter", "MesoZipper" };
    while (true) {
      if (Time.time > time + 60f && boosters[0] == null && boosters[1] == null) {
        break;
      }
      int ran = Random.Range(0, 4);
      if (Time.time < time + 50f) {
        float x = spawner.randomWithRange(-5f, 5f);
        spawner.spawnEnemy(grade1En[ran], x, 10f, LevelSpawner.addToList.All);
        yield return new WaitForSeconds(0.5f);
      } else {
        float x = spawner.randomWithRange(-5f, 5f);
        spawner.spawnEnemy(grade2En[ran], x, 10f, LevelSpawner.addToList.All);
        yield return new WaitForSeconds(3f);
      }
    }
    spawner.LastWaveEnemiesCleared();
  }
}
