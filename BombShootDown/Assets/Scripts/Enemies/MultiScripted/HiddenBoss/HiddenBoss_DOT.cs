using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBoss_DOT : MonoBehaviour {
  void OnEnable() {
    if (!this.enabled) return;
    LifeManager.CurrentLife -= 100f;
  }
  void Update() {
    LifeManager.CurrentLife -= 4f * Time.deltaTime;
  }
}
