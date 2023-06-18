using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour {
  [HideInInspector]
  public Level level;
  [SerializeField]
  GameObject BigSpawnPrefab;
  [SerializeField]
  GameObject SmallSpawnPrefab;
  [HideInInspector]
  WaveController waveControllerScript;
  public bool waveRunning = false;
  [HideInInspector]
  public List<GameObject> AllWaveTriggerEnemies = new List<GameObject>();
  [HideInInspector]
  public List<GameObject> SpecificWaveTriggerEnemies = new List<GameObject>();
  [HideInInspector]
  public List<GameObject> NonTriggerEnemies = new List<GameObject>();
  [HideInInspector]
  public List<GameObject> AllEnemiesListDynamic = new List<GameObject>();
  [HideInInspector]
  public List<GameObject> setEnemies = new List<GameObject>();
  [HideInInspector]
  public enum addToList { All, Specific, None };



  #region dataManagement
  void Awake() {
    waveControllerScript = FindObjectOfType<WaveController>();
    level = gameObject.GetComponent<IGetLevelDataInterface>().GetLevelData();
  }
  void Update() {
    cleanWaveLists();
  }
  public void setLevelData(Level data) {
    level = data;
  }
  #endregion



  #region generalfunctions
  //With should be probably from -5, to 5 and height the maximum depth at y = -5ish
  public float randomWithRange(float min, float max) {
    float ranNum = Random.Range(min, max);
    return ranNum;
  }
  public string findCorrectWaveToStart() {
    int currWave = 1;
    currWave = WaveController.WavesCleared + 1;
    if (currWave <= level.upgradesPerWave.Count) {
      //must name the individual wave coroutines as "wave##" format.
      string waveRoutineName = "wave" + currWave.ToString();
      waveRunning = true;
      return waveRoutineName;
    }
    return null;
  }
  #endregion



  #region waveClearFunctions
  void cleanWaveLists() {
    AllWaveTriggerEnemies.RemoveAll(x => x == null);
    SpecificWaveTriggerEnemies.RemoveAll(x => x == null);
    NonTriggerEnemies.RemoveAll(x => x == null);
    AllEnemiesListDynamic.RemoveAll(x => x == null);
  }
  void updateAllEnemiesList() {
    GameObject[] ene = GameObject.FindGameObjectsWithTag("Enemy");
    GameObject[] tene = GameObject.FindGameObjectsWithTag("TauntEnemy");
    foreach (GameObject go in ene) {
      if (AllEnemiesListDynamic.Contains(go)) {
        continue;
      } else {
        AllEnemiesListDynamic.Add(go);
      }
    }
    foreach (GameObject go in tene) {
      if (AllEnemiesListDynamic.Contains(go)) {
        continue;
      } else {
        AllEnemiesListDynamic.Add(go);
      }
    }
  }
  public void AllTriggerEnemiesCleared() {
    StartCoroutine(AllTriggerEnemiesClearedRoutine());
  }
  IEnumerator AllTriggerEnemiesClearedRoutine() {
    while (AllWaveTriggerEnemies.Count > 0) {
      yield return null;
    }
    waveCleared();
  }
  public void SpecificWaveTriggerEnemiesCleared() {
    StartCoroutine(SpecificTriggerEnemiesClearedRoutine());
  }
  IEnumerator SpecificTriggerEnemiesClearedRoutine() {
    while (SpecificWaveTriggerEnemies.Count > 0) {
      yield return null;
    }
    waveCleared();
  }
  public void AllEnemiesCleared() {
    StartCoroutine(AllEnemiesClearedRoutine());
  }
  IEnumerator AllEnemiesClearedRoutine() {
    updateAllEnemiesList();
    while (AllEnemiesListDynamic.Count > 0) {
      yield return new WaitForSeconds(1f);
      updateAllEnemiesList();
    }
    waveCleared();
  }
  public void LastWaveEnemiesCleared() {
    StartCoroutine(LastWaveEnemiesClearedRoutine());
  }
  IEnumerator LastWaveEnemiesClearedRoutine() {
    while (SpecificWaveTriggerEnemies.Count > 0) {
      yield return null;
    }
    while (AllWaveTriggerEnemies.Count > 0) {
      yield return null;
    }
    while (NonTriggerEnemies.Count > 0) {
      yield return null;
    }
    AllEnemiesCleared();
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
  public GameObject spawnEnemy(string name, float xpos, float ypos, addToList listname = addToList.All, bool setEnemy = false) {
    GameObject enemyPrefab = level.Enemies.Find(x => x.enemyPrefab.name == name).enemyPrefab;
    GameObject spawnedEnemy = Instantiate(enemyPrefab, new Vector3(xpos, ypos, 0f), Quaternion.identity);
    AddEnemyToList(spawnedEnemy, listname);
    if (setEnemy) {
      setEnemies.Add(spawnedEnemy);
    }
    return spawnedEnemy;
  }
  public void spawnEnemyInMap(string name, float xpos, float ypos, bool big, addToList listname = addToList.All, bool setEnemy = false) {
    if (big) {
      Instantiate(BigSpawnPrefab, new Vector3(xpos, ypos, 0f), Quaternion.identity);
    } else {
      Instantiate(SmallSpawnPrefab, new Vector3(xpos, ypos, 0f), Quaternion.identity);
    }
    if (setEnemy) {
      StartCoroutine(mapSpawnRoutine(listname, name, xpos, ypos, true));
    } else {
      StartCoroutine(mapSpawnRoutine(listname, name, xpos, ypos));
    }
  }
  IEnumerator mapSpawnRoutine(addToList listname, string name, float xpos, float ypos, bool setEnemy = false) {
    yield return new WaitForSeconds(0.5f);
    if (setEnemy) {
      setEnemies.Add(spawnEnemy(name, xpos, ypos, listname));
    } else {
      spawnEnemy(name, xpos, ypos, listname);
    }
  }
  void AddEnemyToList(GameObject enemy, addToList listname) {
    if (listname == addToList.None) {
      return;
    }
    if (listname == addToList.All) {
      AllWaveTriggerEnemies.Add(enemy);
      return;
    }
    if (listname == addToList.Specific) {
      SpecificWaveTriggerEnemies.Add(enemy);
      return;
    }
  }
  #endregion
}
