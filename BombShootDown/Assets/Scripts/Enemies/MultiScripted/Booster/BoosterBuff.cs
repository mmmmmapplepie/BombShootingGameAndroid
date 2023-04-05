using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterBuff : MonoBehaviour {
  [SerializeField] bool hyperBooster;
  public static int BoosterCount = 0;
  public static int hyperBoosterCount = 0;
  void OnEnable() {
    addBoosterCount();
    adjustSpeedBuff();
  }
  void addBoosterCount() {
    if (hyperBooster) {
      hyperBoosterCount++;
    } else {
      BoosterCount++;
    }
  }
  void adjustSpeedBuff() {
    BowManager.EnemySpeed = 1f + (float)BoosterCount * 0.2f + (float)hyperBoosterCount * 0.5f;
  }
  void OnDestroy() {
    if (hyperBooster) {
      hyperBoosterCount--;
    } else {
      BoosterCount--;
    }
    adjustSpeedBuff();
  }
}