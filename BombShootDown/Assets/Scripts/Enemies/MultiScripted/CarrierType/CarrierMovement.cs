using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierMovement : MonoBehaviour {
  float speed;
  float driftMag;
  float drift;
  void Awake() {
    speed = transform.root.GetComponent<EnemyLife>().data.Speed;
    newDrift();
    StartCoroutine(driftChanger());
  }
  void Update() {
    move();
  }
  IEnumerator driftChanger() {
    while (true) {
      if (Random.Range(0f, 30f) < 1f) {
        newDrift();
      }
      yield return new WaitForSeconds(1f);
    }
  }
  void newDrift() {
    drift = Random.Range(-1f, 1f);
    driftMag = Mathf.Abs(drift);
  }
  void checkFlip() {
    if (transform.position.x < -5f && drift < 0f) {
      drift = driftMag;
    }
    if (transform.position.x > 5f && drift > 0f) {
      drift = -driftMag;
    }
  }
  void move() {
    checkFlip();
    Vector3 old = transform.root.position;
    Vector3 normDir = new Vector3(drift, -speed, 0f);
    Vector3 newPos = old + Time.deltaTime * BowManager.EnemySpeed * normDir;
    transform.root.position = newPos;
  }
}

