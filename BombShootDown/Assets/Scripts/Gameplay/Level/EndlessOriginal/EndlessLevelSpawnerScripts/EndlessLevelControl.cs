using System.Collections;
using UnityEngine;

public partial class EndlessLevelControl : MonoBehaviour, IGetLevelDataInterface {
  //this is also the BossSpawnController.

  [SerializeField]
  Level level;
  LevelSpawner spawner;
  new AudioManagerBGM audio;

  Enemy[][] bossByTier = new Enemy[5][];

  [SerializeField] Enemy[] tier0Boss, tier1Boss, tier2Boss, tier3Boss, tier4Boss;

  public Level GetLevelData() {
    return level;
  }
  void Awake() {
    spawner = gameObject.GetComponent<LevelSpawner>();
    spawner.setLevelData(level);
    audio = GameObject.Find("AudioManagerBGM").GetComponent<AudioManagerBGM>();
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
    int i = 15;
    while (i > 0) {
      i--;
      float x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("NanoBasic", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(1.5f);
    }
    yield return null;
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave2() {
    int i = 10;
    float x;
    while (i > 0) {
      i--;
      x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("MicroBasic", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(0.5f);
    }
    yield return new WaitForSeconds(10f);
    i = 15;
    while (i > 0) {
      i--;
      x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("MicroBasic", x, 10f, LevelSpawner.addToList.None);
      yield return new WaitForSeconds(0.2f);
    }
    spawner.AllTriggerEnemiesCleared();
  }
  IEnumerator wave3() {
    int i = 5;
    float x;
    for (int k = 0; k < i; k++) {
      x = spawner.randomWithRange(-5f, 5f);
      spawner.spawnEnemy("KiloBasic", x, 10f, LevelSpawner.addToList.All);
      yield return new WaitForSeconds(0.2f);
    }
    spawner.LastWaveEnemiesCleared();
  }
  IEnumerator wave4() {
    yield return null;
  }
  IEnumerator wave5() {
    yield return null;
  }

  //all upgrades enabled here
  IEnumerator wave6() {
    yield return null;
  }
  IEnumerator wave7() {
    yield return null;
  }
  IEnumerator wave8() {
    yield return null;
  }
  IEnumerator wave9() {
    yield return null;
  }
  IEnumerator wave10() {
    yield return null;
  }

  //this one is the final wave and goes on indefinitely.
  IEnumerator wave11() {
    yield return null;
  }
}

