using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectorBuff : MonoBehaviour {
  [SerializeField] int shieldBuff;
  List<EnemyLife> enteredEnemies = new List<EnemyLife>();
  EnemyLife selfLife;
  void Awake() {
    InvokeRepeating("BuffShields", 0f, 1f);
    selfLife = transform.root.GetComponent<EnemyLife>();
  }
  void OnTriggerEnter2D(Collider2D coll) {
    if (coll.tag != "Enemy" && coll.tag != "TauntEnemy") {
      return;
    }
    enteredEnemies.Add(coll.transform.root.GetComponent<EnemyLife>());
  }
  void BuffShields() {
    BuffShield(selfLife);
    foreach (EnemyLife script in enteredEnemies) {
      BuffShield(script);
    }
  }
  void BuffShield(EnemyLife script) {
    script.Shield = script.Shield + shieldBuff > script.MaxShield ? script.MaxShield : script.Shield + shieldBuff;
  }
  void OnTriggerExit2D(Collider2D coll) {
    if (coll.tag != "Enemy" && coll.tag != "TauntEnemy") {
      return;
    }
    enteredEnemies.Remove(coll.transform.root.GetComponent<EnemyLife>());
  }
}
