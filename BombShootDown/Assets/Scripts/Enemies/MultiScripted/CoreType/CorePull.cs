using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePull : MonoBehaviour {
  [SerializeField] float bulletSlowFactor, forceMultiplier, xDamping;
  List<Rigidbody2D> bullets = new List<Rigidbody2D>();
  float transformXPos;
  Transform root;
  void Awake() {
    root = transform.root;
  }
  void Update() {
    transformXPos = root.position.x;
  }
  void FixedUpdate() {
    PullAllBullets();
  }
  void PullAllBullets() {
    foreach (Rigidbody2D rb in bullets) {
      if (rb != null) {
        PullBullet(rb);
      }
    }
    bullets.RemoveAll(rb => rb == null);
  }
  void PullBullet(Rigidbody2D rb) {
    float force;
    float diff = rb.transform.position.x - transformXPos;
    float forcemag = forceMultiplier / Mathf.Pow((1.2f + Mathf.Abs(diff)), 2f);
    if (diff > 0f) {
      force = -forcemag;
    } else {
      force = forcemag;
    }
    // Mathf.Log(GetComponent<Rigidbody2D>().velocity.magnitude + 1) *
    rb.AddForce(new Vector2(force, 0f), ForceMode2D.Force);
    rb.AddForce(new Vector2(-xDamping * rb.velocity.x, 0f));
  }
  void OnTriggerEnter2D(Collider2D coll) {
    if (coll.gameObject.tag == "Bullet") {
      bullets.Add(coll.gameObject.GetComponent<Rigidbody2D>());
      slowBullet(coll);

    }
  }
  void OnTriggerExit2D(Collider2D coll) {
    if (coll.gameObject.tag == "Bullet") {
      bullets.Remove(coll.gameObject.GetComponent<Rigidbody2D>());
      speedUpBullet(coll);
    }
  }
  void OnDestroy() {
    foreach (Rigidbody2D rb in bullets) {
      if (rb != null) {
        Vector3 velocity = rb.velocity;
        rb.velocity = velocity / bulletSlowFactor;
      }
    }
  }
  void slowBullet(Collider2D coll) {
    Rigidbody2D rb = coll.gameObject.GetComponent<Rigidbody2D>();
    Vector3 velocity = rb.velocity;
    rb.velocity = bulletSlowFactor * velocity;
  }
  void speedUpBullet(Collider2D coll) {
    Rigidbody2D rb = coll.gameObject.GetComponent<Rigidbody2D>();
    Vector3 velocity = rb.velocity;
    rb.velocity = velocity / bulletSlowFactor;
  }
}
