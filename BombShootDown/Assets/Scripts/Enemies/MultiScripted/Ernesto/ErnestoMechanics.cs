using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErnestoMechanics : MonoBehaviour {
  [SerializeField] EnemyLife lifeScript;
  [SerializeField] GameObject enemyChosenMarker;
  [SerializeField] float damageRaisePerTime, pickInterval, pickRadius;
  [SerializeField] Slider pickTimerSlider, damageReflect;
  List<GameObject> AffectedEnemies = new List<GameObject>();

  float lastHitTime;
  float lastEnemyPickTime;
  [HideInInspector] public float totalDamage = 0f;
  bool EnemiesPicked = false;
  float latestHealth;
  void Start() {
    lastHitTime = Time.time;
    lastEnemyPickTime = Time.time;
    latestHealth = lifeScript.currentLife;
    StartCoroutine(Skill_EnemiesChange());
    damageReflect.value = 0f;
  }
  void Update() {
    StoredDamage();
  }
  #region ErnestSkill
  IEnumerator Skill_EnemiesChange() {
    while (true) {
      if (EnemiesPicked) {
        yield return null;
        continue;
      } else {
        if (Time.time > pickInterval + lastEnemyPickTime) {
          pickInterval = Time.time;
          PickEnemies();
          pickTimerSlider.value = 1f;
          pickTimerSlider.gameObject.SetActive(false);
        } else {
          pickTimerSlider.value = (pickInterval - (Time.time - lastEnemyPickTime)) / pickInterval;
        }
        yield return null;
      }
    }
  }
  void PickEnemies() {
    EnemiesPicked = true;
    if (lifeScript.dead) {
      StopAllCoroutines();
      return;
    }
    SetPickedEnemies();
  }
  void SetPickedEnemies() {
    Collider2D[] Enemies = Physics2D.OverlapCircleAll(transform.root.position, pickRadius);
    foreach (Collider2D coll in Enemies) {
      if (coll.tag == "Enemy" || coll.tag == "TauntEnemy") {
        if (transform.Find("Enemy").gameObject == coll.gameObject) continue;
        AffectedEnemies.Add(coll.gameObject);
        //add the particle effect
        // if (coll.transform.Find("ErnestoMark") == null) {
        //   Instantiate(enemyChosenMarker, coll.transform.position, Quaternion.identity, coll.transform);
        // }
        coll.transform.root.gameObject.AddComponent<ErnestoAbility>();
        coll.transform.root.gameObject.GetComponent<ErnestoAbility>().ernestoScript = this;
      }
    }
    StartCoroutine(SkillFinalActivation());
  }
  IEnumerator SkillFinalActivation() {
    while (true) {
      AffectedEnemies.RemoveAll(x => x == null);
      if (AffectedEnemies.Count > 0) {
        yield return null;
        continue;
      }
      yield return new WaitForSeconds(1f);
      LifeManager.CurrentLife -= totalDamage;
      totalDamage = 0f;
      AffectedEnemies.Clear();
      EnemiesPicked = false;
      lastEnemyPickTime = Time.time;

      pickTimerSlider.gameObject.SetActive(true);
      yield break;
    }
  }
  #endregion
  #region ErnestoStoredDamage
  void StoredDamage() {
    float currentStoredDmg = Mathf.Min(damageRaisePerTime * (Time.time - lastHitTime), 220f);
    if (latestHealth != lifeScript.currentLife && latestHealth > lifeScript.currentLife) {
      ReleaseStoredDamage(currentStoredDmg);
    } else {
      damageReflect.value = currentStoredDmg / 220f;
    }
  }
  void ReleaseStoredDamage(float storedDmg) {
    LifeManager.CurrentLife -= storedDmg;
    damageReflect.value = 0f;
    lastHitTime = Time.time;
    latestHealth = lifeScript.currentLife;
    print("got hit");
  }
  #endregion



}
