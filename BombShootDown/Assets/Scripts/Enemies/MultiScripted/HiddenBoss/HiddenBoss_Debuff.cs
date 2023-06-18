using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBoss_Debuff : MonoBehaviour {
  void OnEnable() {
    BowManager.BulletMultiplier /= 3f;
    BowManager.CoolDownRate *= 3f;
  }
  void OnDisable() {
    BowManager.BulletMultiplier *= 3f;
    BowManager.CoolDownRate /= 3f;
  }
}
