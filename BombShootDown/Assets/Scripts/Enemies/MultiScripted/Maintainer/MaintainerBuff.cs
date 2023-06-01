using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainerBuff : MonoBehaviour {
  [SerializeField] float healthBuff;
  List<IDamageable> enteredEnemies = new List<IDamageable>();
  IDamageable selfLife;
  void Awake() {
    InvokeRepeating("BuffHealths", 0f, 1f);
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
  void BuffHealths() {
    BuffHealth(selfLife);
    foreach (IDamageable script in enteredEnemies) {
      BuffHealth(script);
    }
  }
  void BuffHealth(IDamageable script) {
    script.currentLife = script.currentLife + healthBuff > script.maxLife ? script.maxLife : script.currentLife + healthBuff;
  }
  void OnTriggerExit2D(Collider2D coll) {
    if (coll.tag != "Enemy" && coll.tag != "TauntEnemy") {
      return;
    }
    enteredEnemies.Remove(coll.transform.root.GetComponent<IDamageable>());
  }
}
