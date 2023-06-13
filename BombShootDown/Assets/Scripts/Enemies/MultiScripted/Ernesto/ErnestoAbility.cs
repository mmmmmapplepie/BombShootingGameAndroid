using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErnestoAbility : MonoBehaviour {
  public ErnestoMechanics ernestoScript;
  float damage = 0f;
  void OnEnable() {
    damage = gameObject.GetComponent<IDamageable>().data.Damage;
    IEnemyDealsDamage damagescript = gameObject.GetComponent<IEnemyDealsDamage>();
    if (damagescript != null) {
      damagescript.Damage = 0f;
    }
  }
  void OnDestroy() {
    if (gameObject.transform.root.position.y < -7.25f) {
      ernestoScript.totalDamage += damage;
    }
  }
}
