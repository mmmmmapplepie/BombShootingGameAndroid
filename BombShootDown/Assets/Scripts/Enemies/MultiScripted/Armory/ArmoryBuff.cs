using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoryBuff : MonoBehaviour {
  [SerializeField] int armorBuff;
  List<Collider2D> enteredEnemies = new List<Collider2D>();
  void OnTriggerEnter2D(Collider2D coll) {
    if (coll.tag != "Enemy" && coll.tag != "TauntEnemy") {
      return;
    }
    enteredEnemies.Add(coll);
    BuffArmor(coll.transform.root.gameObject.GetComponent<EnemyLife>());
  }
  void BuffArmor(EnemyLife script) {
    script.Armor += armorBuff;
  }
  void OnTriggerExit2D(Collider2D coll) {
    if (coll.tag != "Enemy" && coll.tag != "TauntEnemy") {
      return;
    }
    enteredEnemies.Remove(coll);
    RemoveArmor(coll.transform.root.gameObject.GetComponent<EnemyLife>());
  }
  void RemoveArmor(EnemyLife script) {
    script.Armor -= armorBuff;
  }
  void OnDestroy() {
    foreach (Collider2D coll in enteredEnemies) {
      if (coll != null)
        coll.transform.root.gameObject.GetComponent<EnemyLife>().Armor -= armorBuff;
    }
  }
}
