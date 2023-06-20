using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBoss_Vampire : MonoBehaviour {
  [SerializeField] HiddenBossLife lifeScript;
  [SerializeField] float pickRadius;
  float recoveryLife;
  int recoveryShields;
  void OnEnable() {
    if (!this.enabled) return;
    VampireRecovery();
  }
  void VampireRecovery() {
    Collider2D[] EnemiesTemp = Physics2D.OverlapCircleAll(transform.root.position, pickRadius);
    foreach (Collider2D coll in EnemiesTemp) {
      if (coll.transform.root.gameObject == gameObject) continue;
      if (coll.tag == "Enemy" || coll.tag == "TauntEnemy") {
        recoveryLife += coll.transform.root.gameObject.GetComponent<IDamageable>().currentLife;
        recoveryShields += coll.transform.root.gameObject.GetComponent<IDamageable>().Shield;
        coll.transform.root.gameObject.GetComponent<IDamageable>().takeTrueDamage(recoveryLife + 1f);
      }
    }
    lifeScript.currentLife += recoveryLife;
    if (lifeScript.currentLife > lifeScript.maxLife) lifeScript.maxLife = lifeScript.currentLife;
    lifeScript.Shield += recoveryShields;
    if (lifeScript.Shield > lifeScript.MaxShield) lifeScript.MaxShield = lifeScript.Shield;
    recoveryLife = 0f;
    recoveryShields = 0;
  }
}
