using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoupladMovement : MonoBehaviour {
  Enemy data;
  float speed;
  [SerializeField] float distanceToCover = 40f, smoothTime = 0.5f;
  float initialXPosition;
  bool waiting = false, finalStretch = false;
  Vector3 newPosition;
  float newDistanceToMove = 0f;
  Vector3 referenceForSmoothDamp = Vector3.zero;
  void Start() {
    data = transform.root.GetComponent<IDamageable>().data;
    speed = data.Speed;
    newPosition = transform.root.position;
    initialXPosition = transform.root.position.x;
    chooseNewPosition();
  }
  void OnEnable() {
    waiting = false;
  }
  void Update() {
    checkArrived();
    movePosition();
  }
  void movePosition() {
    if (waiting) return;
    if (!finalStretch) {
      transform.root.position = Vector3.SmoothDamp(transform.root.position, newPosition, ref referenceForSmoothDamp, smoothTime, speed * BowManager.EnemySpeed);
    }
    if (finalStretch) {
      transform.root.position = Vector3.SmoothDamp(transform.root.position, new Vector3(transform.root.position.x, -20f, 0f), ref referenceForSmoothDamp, smoothTime, 2 * speed * BowManager.EnemySpeed);
    }
  }
  void checkArrived() {
    if ((transform.root.position - newPosition).sqrMagnitude < 0.1f) {
      if (waiting || finalStretch) return;
      waiting = true;
      StartCoroutine(wait(Random.Range(3f, 5f)));
      if (distanceToCover >= 0f) {
        distanceToCover -= newDistanceToMove;
        chooseNewPosition();
      } else {
        finalStretch = true;
      }
    }
  }
  void chooseNewPosition() {
    if (distanceToCover > 0f) {
      Vector3 trialNewPosition = new Vector3(Random.Range(-5.2f, 5.2f), Random.Range(-5f, 10f), 0f);
      while ((trialNewPosition - newPosition).sqrMagnitude < 0.5f) {
        trialNewPosition = new Vector3(Random.Range(-5.2f, 5.2f), Random.Range(-5f, 10f), 0f);
      }
      newPosition = trialNewPosition;
      newDistanceToMove = (transform.root.position - newPosition).magnitude;
    }
    if (distanceToCover <= 0f && !finalStretch) {
      newPosition = new Vector3(initialXPosition, Random.Range(6f, 9f), 0f);
      newDistanceToMove = (transform.root.position - newPosition).magnitude;
    }
  }
  IEnumerator wait(float waitTime) {
    yield return new WaitForSeconds(waitTime);
    waiting = false;
  }
}
