using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shifter : MonoBehaviour {
  [SerializeField]
  float shiftRate;
  [SerializeField]
  float forceMultiplier;
  Enemy data;
  float speed;
  float lastShift;
  Rigidbody2D rb;
  void Awake() {
    rb = transform.root.GetComponent<Rigidbody2D>();
    data = transform.root.GetComponent<EnemyLife>().data;
    speed = data.Speed;
  }
  void Start() {
    lastShift = Time.time;
  }
  void Update() {
    moveDown();
    if ((Time.time - lastShift) > shiftRate) {
      lastShift = Time.time;
      shiftImpulse();
    }
  }
  void moveDown() {
    Vector3 old = transform.root.position;
    Vector3 Dir = new Vector3(0f, -1f, 0f);
    Vector3 newPos = old + Time.deltaTime * speed * BowManager.EnemySpeed * Dir;
    transform.root.position = newPos;
  }
  void shiftImpulse() {
    float dir = (float)getDirection();
    rb.AddForce(new Vector2(rb.mass * dir * forceMultiplier * BowManager.EnemySpeed, 0), ForceMode2D.Impulse);
  }
  int getDirection() {
    float ran = Random.Range(-1f, 1f);
    if (ran > 0f) {
      return 1;
    } else {
      return -1;
    }
  }
}
