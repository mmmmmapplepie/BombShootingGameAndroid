using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class MinimaMovement : MonoBehaviour {
  Enemy data;
  float speed;
  [SerializeField] float distanceToCover = 40f, smoothTime = 0.05f;
  bool waiting = false;
  Vector3 newPosition;
  float newDistanceToMove = 0f;
  Vector3 referenceForSmoothDamp = Vector3.zero;
  UnityAction MovementPhase;
  void Awake() {
    MovementPhase += Phase1Movement;
  }
  void Start() {
    data = transform.root.GetComponent<IDamageable>().data;
    speed = data.Speed;
    newPosition = transform.root.position;
    chooseNewPosition();
  }
  void Update() {
    MovementPhase();
  }
  void Phase1Movement() {
    Phase1checkArrived();
    Phase1movePosition();
  }
  void Phase1movePosition() {
    if (waiting) return;
    transform.root.position = Vector3.SmoothDamp(transform.root.position, newPosition, ref referenceForSmoothDamp, smoothTime, speed * BowManager.EnemySpeed);

    // transform.root.position = Vector3.SmoothDamp(transform.root.position, new Vector3(transform.root.position.x, -20f, 0f), ref referenceForSmoothDamp, smoothTime, 2 * speed * BowManager.EnemySpeed);
  }
  void Phase1checkArrived() {
    if ((transform.root.position - newPosition).sqrMagnitude < 0.1f) {
      if (waiting) return;
      waiting = true;
      StartCoroutine(wait(Random.Range(0f, 0.5f)));
      if (distanceToCover >= 0f) {
        distanceToCover -= newDistanceToMove;
        chooseNewPosition();
      } else {
        MovementPhase -= Phase1Movement;
        MovementPhase += Phase2Movement;
      }
    }
  }
  void chooseNewPosition() {
    Vector3 trialNewPosition = new Vector3(Random.Range(-5.2f, 5.2f), Random.Range(-5f, 10f), 0f);
    while ((trialNewPosition - newPosition).sqrMagnitude < 0.5f) {
      trialNewPosition = new Vector3(Random.Range(-5.2f, 5.2f), Random.Range(-5f, 10f), 0f);
    }
    newPosition = trialNewPosition;
    newDistanceToMove = (transform.root.position - newPosition).magnitude;
  }
  void Phase2Movement() {
    if (waiting) return;
    transform.root.position = Vector3.SmoothDamp(transform.root.position, new Vector3(transform.root.position.x, -20f, 0f), ref referenceForSmoothDamp, smoothTime, 2 * speed * BowManager.EnemySpeed);
  }
  IEnumerator wait(float waitTime) {
    yield return new WaitForSeconds(waitTime);
    waiting = false;
  }
}
