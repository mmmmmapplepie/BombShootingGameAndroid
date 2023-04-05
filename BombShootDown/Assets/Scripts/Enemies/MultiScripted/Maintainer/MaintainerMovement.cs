using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainerMovement : MonoBehaviour {
  Enemy data;
  float speed;
  float initialDistance;
  float startyPos;
  bool startWait = false;
  bool doneWait = false;
  float waitTime;
  float driftMag;
  float drift;
  void Awake() {
    data = transform.root.gameObject.GetComponent<EnemyLife>().data;
    speed = data.Speed;
    initialDistance = Random.Range(1f, 4f);
    waitTime = Random.Range(0f, 5f);
    startyPos = transform.root.position.y;
    drift = Random.Range(-1f, 1f);
    driftMag = Mathf.Abs(drift);
  }
  void Update() {
    if (transform.root.position.y > startyPos - initialDistance) {
      transform.root.position -= new Vector3(0f, speed * BowManager.EnemySpeed * Time.deltaTime, 0f);
    }
    if (transform.root.position.y < startyPos - initialDistance && startWait == false) {
      startWait = true;
      StartCoroutine("Wait");
    }
    if (doneWait == true) {
      transform.root.position -= new Vector3(0f, speed * BowManager.EnemySpeed * Time.deltaTime, 0f);
    }
    driftSide();
  }
  IEnumerator Wait() {
    yield return new WaitForSeconds(waitTime);
    doneWait = true;
  }
  void checkFlip() {
    if (transform.position.x < -5f && drift < 0f) {
      drift = driftMag;
    }
    if (transform.position.x > 5f && drift > 0f) {
      drift = -driftMag;
    }
  }
  void driftSide() {
    checkFlip();
    Vector3 old = transform.root.position;
    Vector3 Dir = new Vector3(drift, 0f, 0f);
    Vector3 newPos = old + Time.deltaTime * speed * BowManager.EnemySpeed * Dir;
    transform.root.position = newPos;
  }
}
