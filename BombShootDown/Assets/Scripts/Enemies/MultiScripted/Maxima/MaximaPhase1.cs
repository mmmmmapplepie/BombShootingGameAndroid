using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaximaPhase1 : MonoBehaviour {
  float lifeChecker;
  [SerializeField] MaximaLife lifescript;
  void Awake() {
    lifeChecker = lifescript.currentLife;
  }
  void OnEnable() {
    BowManager.EnemyDamage *= 2f;
    BowManager.EnemySpeed *= 1.2f;
    BowManager.AmmoRate /= 2f;
    BowManager.CoolDownRate /= 2f;
  }
  void OnDisable() {
    BowManager.EnemyDamage /= 2f;
    BowManager.EnemySpeed /= 1.2f;
    BowManager.AmmoRate *= 2f;
    BowManager.CoolDownRate *= 2f;
    ReflectDamage(0.1f);
  }
  void Update() {
    ReflectDamage(0.1f);
  }
  void ReflectDamage(float percent) {
    if (lifescript.currentLife != lifeChecker && lifescript.currentLife < lifeChecker) {
      LifeManager.CurrentLife -= Mathf.Min((lifeChecker - lifescript.currentLife), lifeChecker) * percent;
      lifeChecker = lifescript.currentLife;
    }
  }
}
