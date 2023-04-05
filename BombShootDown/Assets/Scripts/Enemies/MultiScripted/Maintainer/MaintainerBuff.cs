using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainerBuff : MonoBehaviour {
  [SerializeField] float healthBuff;
  List<EnemyLife> enteredEnemies = new List<EnemyLife>();
  EnemyLife selfLife;
  void Awake() {
    InvokeRepeating("BuffHealths", 0f, 1f);
    selfLife = transform.root.GetComponent<EnemyLife>();
  }
  void OnTriggerEnter2D(Collider2D coll) {
    if (coll.tag != "Enemy" && coll.tag != "TauntEnemy") {
      return;
    }
    enteredEnemies.Add(coll.transform.root.GetComponent<EnemyLife>());
  }
  void BuffHealths() {
    BuffHealth(selfLife);
    foreach (EnemyLife script in enteredEnemies) {
      BuffHealth(script);
    }
  }
  void BuffHealth(EnemyLife script) {
    script.currentLife = script.currentLife + healthBuff > script.maxLife ? script.maxLife : script.currentLife + healthBuff;
  }
  void OnTriggerExit2D(Collider2D coll) {
    if (coll.tag != "Enemy" && coll.tag != "TauntEnemy") {
      return;
    }
    enteredEnemies.Remove(coll.transform.root.GetComponent<EnemyLife>());
  }
}
