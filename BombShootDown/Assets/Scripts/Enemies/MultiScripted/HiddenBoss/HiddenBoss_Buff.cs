using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBoss_Buff : MonoBehaviour {
  void OnEnable() {
    BowManager.EnemyDamage *= 3f;
    BowManager.EnemySpeed *= 3f;
  }

  void OnDisable() {
    BowManager.EnemyDamage /= 3f;
    BowManager.EnemySpeed /= 3f;
  }
}
