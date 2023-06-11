using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaximaPhase2 : MonoBehaviour {
  [SerializeField] GameObject summoningTimerSliderGObject;
  [SerializeField] Slider summoningTimerSlider;
  [SerializeField] List<Enemy> AvailableSummons; //Hyper carrier,core, Couplad, Max Couplad
  [SerializeField] GameObject BigSpawnEffect;
  [SerializeField] float SummonTime = 45f;
  float lastSummonTime;

  void OnEnable() {
    lastSummonTime = Time.time;
  }
  void Update() {
    checkToSummon();
  }
  void checkToSummon() {
    if (Time.time - lastSummonTime > SummonTime) {
      Summon();
      lastSummonTime = Time.time;
      summoningTimerSlider.value = 1f;
    } else {
      summoningTimerSlider.value = (SummonTime - Time.time + lastSummonTime) / SummonTime;
    }
  }
  void Summon() {
    Vector3 position = transform.position;
    GameObject spawnEffect = Instantiate(BigSpawnEffect, position, Quaternion.identity);
    GameObject prefab = AvailableSummons[Random.Range(0, AvailableSummons.Count - 1)].enemyPrefab;
    StartCoroutine(Spawn(prefab, position));
  }
  IEnumerator Spawn(GameObject prefab, Vector3 Position) {
    yield return new WaitForSeconds(0.5f);
    Instantiate(prefab, Position, Quaternion.identity);
  }
}
