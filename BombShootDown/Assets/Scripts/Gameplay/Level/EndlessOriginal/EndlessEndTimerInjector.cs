using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessEndTimerInjector : MonoBehaviour {
  [SerializeField] EndlessGameDefeat script;

  void Awake() {
    script.StartTime = Time.time;
  }
}
