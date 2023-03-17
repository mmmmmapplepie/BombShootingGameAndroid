using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawnerBase : MonoBehaviour, IGetLevelDataInterface {
  [SerializeField]
  public Level level;
  [SerializeField]
  GameObject BigSpawnPrefab;
  [SerializeField]
  GameObject SmallSpawnPrefab;
  [HideInInspector]
  public WaveController waveControllerScript;
  // [SerializeField]
  // spawning animation prefab spawnEffect;
  [HideInInspector]
  public bool waveRunning = false;
  [HideInInspector]
  public List<GameObject> AllWaveTriggerEnemies = new List<GameObject>();
  [HideInInspector]
  public List<GameObject> SpecificWaveTriggerEnemies = new List<GameObject>();
  [HideInInspector]
  public enum addToList { All, Specific, None };



  #region dataManagement
  void Awake() {
    waveControllerScript = FindObjectOfType<WaveController>();
  }
  public Level GetLevelData() {
    return level;
  }
  #endregion



  #region generalfunctions
  //With should be probably from -5, to 5 and height the maximum depth at y = -5ish
  public float randomWithRange(float min, float max) {
    float ranNum = Random.Range(min, max);
    return ranNum;
  }
  public void findCorrectWaveToStart() {
    int currWave = 1;
    currWave = WaveController.WavesCleared + 1;
    if (currWave <= level.upgradesPerWave.Count) {
      //must name the individual wave coroutines as "wave##" format.
      string waveRoutineName = "wave" + currWave.ToString();
      waveRunning = true;
      resetWaveSettings();
      StartCoroutine(waveRoutineName);
    }
  }
  #endregion




  #region waveClearFunctions
  public void cleanWaveLists() {
    AllWaveTriggerEnemies.RemoveAll(x => x == null);
    SpecificWaveTriggerEnemies.RemoveAll(x => x == null);
  }
  public void resetWaveSettings() {
    AllWaveTriggerEnemies.Clear();
    SpecificWaveTriggerEnemies.Clear();
  }
  public IEnumerator WaveTriggerEnemiesCleared() {
    while (AllWaveTriggerEnemies.Count > 0) {
      yield return null;
    }
    waveCleared();
  }
  public void waveCleared() {
    WaveController.WavesCleared++;
    if (WaveController.WavesCleared == level.upgradesPerWave.Count) {
      WaveController.LevelCleared = true;
    }
    waveRunning = false;
  }

  #endregion



  #region spawnEnemyFunctions
  public void spawnEnemy(string name, float xpos, float ypos, addToList listname) {
    GameObject enemyPrefab = level.Enemies.Find(x => x.enemyPrefab.name == name).enemyPrefab;
    GameObject spawnedEnemy = Instantiate(enemyPrefab, new Vector3(xpos, ypos, 0f), Quaternion.identity);
    AddEnemyToList(spawnedEnemy, listname);
  }
  public void spawnEnemyInMap(string name, float xpos, float ypos, bool big, addToList listname) {
    if (big) {
      Instantiate(BigSpawnPrefab, new Vector3(xpos, ypos, 0f), Quaternion.identity);
    } else {
      Instantiate(SmallSpawnPrefab, new Vector3(xpos, ypos, 0f), Quaternion.identity);
    }
    StartCoroutine(mapSpawnRoutine(listname, name, xpos, ypos));
  }
  IEnumerator mapSpawnRoutine(addToList listname, string name, float xpos, float ypos) {
    yield return new WaitForSeconds(0.5f);
    spawnEnemy(name, xpos, ypos, listname);
  }
  void AddEnemyToList(GameObject enemy, addToList listname) {
    if (listname == addToList.None) {
      return;
    }
    if (listname == addToList.All) {
      SpecificWaveTriggerEnemies.Add(enemy);
      return;
    }
    if (listname == addToList.Specific) {
      AllWaveTriggerEnemies.Add(enemy);
      return;
    }
  }
  #endregion
}
