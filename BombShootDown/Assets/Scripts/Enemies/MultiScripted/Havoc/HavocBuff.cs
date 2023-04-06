using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavocBuff : MonoBehaviour {
  [SerializeField] bool hyperHavoc;
  public static int havocCount = 0;
  public static int hyperHavocCount = 0;
  void OnEnable() {
    addHavocCount();
    adjustDamageBuff();
  }
  void addHavocCount() {
    if (hyperHavoc) {
      hyperHavocCount++;
    } else {
      havocCount++;
    }
  }
  void adjustDamageBuff() {
    BowManager.EnemyDamage = 1f + (float)havocCount * 0.1f + (float)hyperHavocCount * 0.3f;
  }
  void OnDestroy() {
    if (hyperHavoc) {
      hyperHavocCount--;
    } else {
      havocCount--;
    }
    adjustDamageBuff();
  }
}
