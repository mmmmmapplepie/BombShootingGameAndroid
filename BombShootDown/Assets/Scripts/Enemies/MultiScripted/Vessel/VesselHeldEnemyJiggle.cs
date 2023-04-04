using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselHeldEnemyJiggle : MonoBehaviour {
  float startoffset;
  void Awake() {
    startoffset = Random.Range(0f, 360f);
  }
  void Update() {
    Quaternion rot = Quaternion.Euler(0f, 0f, 5f * Mathf.Sin((Time.time + startoffset) * 5f));
    transform.rotation = rot;
  }
}
