using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErnestoMechanics : MonoBehaviour {
  [SerializeField] EnemyLife lifeScript;
  [SerializeField] GameObject enemyChosenMarker;
  [SerializeField] float damageRaisePerTime, pickInterval, pickRadius;
  [SerializeField] Slider pickTimerSlider;
  List<GameObject> AffectedEnemies;

  float lastHitTime;
  float lastEnemyPickTime;
  public float totalDamage = 0f;
  bool EnemiesPicked = false;
  float currentHealth;
  void Start() {
    lastHitTime = Time.time;
    lastEnemyPickTime = Time.time;
    //start the picking enemy units thingy coroutine
    //start the building damage between hits
  }
  IEnumerator Skill_EnemiesChange() {
    while (true) {
      if (EnemiesPicked) yield return null;
      else {
        if (Time.time > pickInterval + lastEnemyPickTime) {
          pickInterval = Time.time;
          PickEnemies();
          pickTimerSlider.value = 1f;
        } else {
          pickTimerSlider.value = (pickInterval - (Time.time - pickInterval)) / pickInterval;
        }
        yield return null;
      }
    }
  }
  void PickEnemies() {
    EnemiesPicked = true;
    SetPickedEnemies();
    StartCoroutine(waitForPickedEnemyResolution());
  }
  void SetPickedEnemies() {
    Collider2D[] Enemies = Physics2D.OverlapCircleAll(transform.root.position, pickRadius);
    foreach (Collider2D coll in Enemies) {
      if (coll.tag == "Enemy" || coll.tag == "Taunt") {
        AffectedEnemies.Add(coll.gameObject);
        //add the particle effect
        // if (coll.transform.Find("ErnestoMark") == null) {
        //   Instantiate(enemyChosenMarker, coll.transform.position, Quaternion.identity, coll.transform);
        // }
        coll.transform.root.gameObject.AddComponent<ErnestoAbility>();
        coll.transform.root.gameObject.GetComponent<ErnestoAbility>().ernestoScript = this;
      }
    }
  }
  IEnumerator waitForPickedEnemyResolution() {
    while (true) {
      foreach (GameObject enemy in AffectedEnemies) {
        if (enemy != null) {
          AffectedEnemies.Remove(enemy);
          yield return null;
        }
      }
      yield return new WaitForSeconds(1f);
      LifeManager.CurrentLife -= totalDamage;
      totalDamage = 0f;
      AffectedEnemies.Clear();
      yield break;
    }
  }





}
