using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JammerDebuff : MonoBehaviour {
  [SerializeField] bool hyperJammer;
  public static int JammerCount = 0;
  public static int hyperJammerCount = 0;
  void Start() {
    addJammerCount();
    adjustDisruptionBuff();
  }
  void addJammerCount() {
    if (hyperJammer) {
      hyperJammerCount++;
    } else {
      JammerCount++;
    }
  }
  void adjustDisruptionBuff() {
    BowManager.CoolDownRate = (Mathf.Pow(1.4f, (float)JammerCount) * Mathf.Pow(2f, (float)hyperJammerCount));
  }
  void OnDestroy() {
    if (hyperJammer) {
      hyperJammerCount--;
    } else {
      JammerCount--;
    }
    adjustDisruptionBuff();
  }
}
