using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErnestoStringScript : MonoBehaviour {
  [SerializeField] Transform core;

  float previousDistance;

  float xscale, zscale;

  void Start() {
    previousDistance = Vector3.Distance(core.position, transform.position);
    xscale = transform.localScale.x;
    zscale = transform.localScale.z;

  }
  void Update() {
    changeAngle();
    changeScale();
  }

  void changeAngle() {
    float angleDiff = Vector3.Angle(Vector3.up, core.position - transform.position);
    angleDiff = transform.position.x > core.position.x ? angleDiff : -angleDiff;
    transform.rotation = Quaternion.Euler(0f, 0f, angleDiff);
  }
  void changeScale() {
    float currentDistance = Vector3.Distance(core.position, transform.position);
    transform.localScale = new Vector3(xscale, transform.localScale.y * (currentDistance / previousDistance), zscale);
    previousDistance = currentDistance;
  }
}
