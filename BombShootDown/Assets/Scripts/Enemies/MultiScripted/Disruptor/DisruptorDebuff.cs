using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisruptorDebuff : MonoBehaviour {
  [SerializeField] bool hyperDisruptor;
  public static int DisruptorCount = 0;
  public static int hyperDisruptorCount = 0;
  void Start() {
    addDisruptorCount();
    adjustDisruptionBuff();
  }
  void addDisruptorCount() {
    if (hyperDisruptor) {
      hyperDisruptorCount++;
    } else {
      DisruptorCount++;
    }
  }
  void adjustDisruptionBuff() {
    BowManager.BulletMultiplier = Mathf.Pow(0.8f, (float)DisruptorCount) * Mathf.Pow(0.6f, (float)hyperDisruptorCount);
  }
  void OnDestroy() {
    if (hyperDisruptor) {
      hyperDisruptorCount--;
    } else {
      DisruptorCount--;
    }
    adjustDisruptionBuff();
  }
}

