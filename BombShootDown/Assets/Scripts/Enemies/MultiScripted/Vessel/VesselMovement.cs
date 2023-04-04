using UnityEngine;
public class VesselMovement : MonoBehaviour {
  Enemy data;
  float speed;
  float driftMag;
  float drift;
  void Awake() {
    data = transform.root.GetComponent<EnemyLife>().data;
    speed = data.Speed;
    drift = Random.Range(-4f, 4f);
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
  void FixedUpdate() {
    checkFlip();
    Vector3 old = transform.root.position;
    Vector3 normDir = new Vector3(drift, -1f, 0f);
    normDir.Normalize();
    Vector3 newPos = old + Time.deltaTime * speed * BowManager.EnemySpeed * normDir;
    transform.root.position = newPos;
  }
}
