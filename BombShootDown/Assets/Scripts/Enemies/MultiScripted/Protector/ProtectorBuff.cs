using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectorBuff : MonoBehaviour {
  [SerializeField] int shieldBuff;
  List<IDamageable> enteredEnemies = new List<IDamageable>();
  IDamageable selfLife;
  void Awake() {
    InvokeRepeating("BuffShields", 0f, 1f);
    selfLife = transform.root.GetComponent<IDamageable>();
  }
  void OnTriggerEnter2D(Collider2D coll) {
    if (coll.tag != "Enemy" && coll.tag != "TauntEnemy") {
      return;
    }
    if (coll.transform.root.gameObject.GetComponent<IDamageable>() != null) {
      enteredEnemies.Add(coll.transform.root.GetComponent<IDamageable>());
    }
  }
  void BuffShields() {
    BuffShield(selfLife);
    foreach (IDamageable script in enteredEnemies) {
      BuffShield(script);
    }
  }
  void BuffShield(IDamageable script) {
    script.Shield = script.Shield + shieldBuff > script.MaxShield ? script.MaxShield : script.Shield + shieldBuff;
  }
  void OnTriggerExit2D(Collider2D coll) {
    if (coll.tag != "Enemy" && coll.tag != "TauntEnemy") {
      return;
    }
    enteredEnemies.Remove(coll.transform.root.GetComponent<IDamageable>());
  }
}
