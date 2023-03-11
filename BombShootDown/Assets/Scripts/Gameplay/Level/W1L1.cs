using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W1L1 : MonoBehaviour
{
  WaveController waveControllerScript;
  // [SerializeField]
  // spawning animation prefab spawnEffect;
  bool waveRunning = false;
  List<GameObject> WaveTriggerEnemiesAll = new List<GameObject>();

  void Awake()
  {
    waveControllerScript = FindObjectOfType<WaveController>();
  }
  void Update()
  {
    cleanWaveLists();
    if (WaveController.CurrentWave > WaveController.WavesCleared && WaveController.startWave == true && waveRunning == false)
    {
      findCorrectWaveToStart();
    }
  }
  #region generalfunctions
  //With should be probably from -5, to 5 and height the maximum depth at y = -5ish
  float randomWithRange(float min, float max)
  {
    float ranNum = Random.Range(min, max);
    return ranNum;
  }
  void cleanWaveLists()
  {
    WaveTriggerEnemiesAll.RemoveAll(x => x == null);
  }
  void resetWaveSettings()
  {
    WaveTriggerEnemiesAll.Clear();
  }
  IEnumerator WaveTriggerEnemiesCleared()
  {
    while (WaveTriggerEnemiesAll.Count > 0)
    {
      yield return null;
    }
    waveCleared();
  }
  GameObject spawnEnemy(string name, float xpos, float ypos)
  {
    GameObject enemyPrefab = waveControllerScript.thisLevelData.Enemies.Find(x => x.enemyPrefab.name == name).enemyPrefab;
    GameObject spawnedEnemy = Instantiate(enemyPrefab, new Vector3(xpos, ypos, 0f), Quaternion.identity);
    return spawnedEnemy;
  }
  void findCorrectWaveToStart()
  {
    int currWave = 1;
    currWave = WaveController.WavesCleared + 1;
    if (currWave <= waveControllerScript.thisLevelData.upgradesPerWave.Count)
    {
      //must name the individual wave coroutines as "wave##" format.
      string waveRoutineName = "wave" + currWave.ToString();
      waveRunning = true;
      resetWaveSettings();
      StartCoroutine(waveRoutineName);
    }
  }
  void waveCleared()
  {
    WaveController.WavesCleared++;
    if (WaveController.WavesCleared == waveControllerScript.thisLevelData.upgradesPerWave.Count)
    {
      WaveController.LevelCleared = true;
    }
    waveRunning = false;
  }
  #endregion

  #region LevelDesign
  IEnumerator wave1()
  {
    int totalEnemies = 50;
    while (totalEnemies > 0)
    {
      totalEnemies--;
      float x = randomWithRange(-5f, 5f);
      WaveTriggerEnemiesAll.Add(spawnEnemy("NanoBasic", x, 10f));
      yield return new WaitForSeconds(0.1f);
    }
    StartCoroutine("WaveTriggerEnemiesCleared");
  }
  IEnumerator wave2()
  {
    int totalEnemies = 200;
    while (totalEnemies > 0)
    {
      totalEnemies--;
      float x = randomWithRange(-5f, 5f);
      WaveTriggerEnemiesAll.Add(spawnEnemy("NanoBasic", x, 10f));
      yield return new WaitForSeconds(0.1f);
    }
    StartCoroutine("WaveTriggerEnemiesCleared");
  }
  #endregion
}
