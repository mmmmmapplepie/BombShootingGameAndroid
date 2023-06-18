using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBoss_Vampire : MonoBehaviour {
  [SerializeField] EnemyLife lifeScript;
  [SerializeField] float pickRadius;
  float recoveryLife;
  int recoveryShields;
  void OnEnable() {
    VampireRecovery();
  }
  void VampireRecovery() {
    Collider2D[] EnemiesTemp = Physics2D.OverlapCircleAll(transform.root.position, pickRadius);
    foreach (Collider2D coll in EnemiesTemp) {
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
