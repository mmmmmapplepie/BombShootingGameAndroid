using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePull : MonoBehaviour {
  [SerializeField] float bulletSlowFactor, forceMultiplier;
  List<Rigidbody2D> bullets = new List<Rigidbody2D>();
  float transformXPos;
  Transform root;
  void Awake() {
    root = transform.root;
  }
  void Update() {
    transformXPos = root.position.x;
    foreach (Rigidbody2D rb in bullets) {

    }
  }


  void PullEnemies(Rigidbody2D rb) {
    float force;
    float diff = rb.transform.position.x - transformXPos;
    float forcemag = forceMultiplier / Mathf.Pow((3f + Mathf.Abs(diff)), 2f);
    if (diff > 0f) {
      force = -forcemag;
    } else {
      force = forcemag;
    }
    rb.AddForce(new Vector2(Mathf.Log(GetComponent<Rigidbody2D>().velocity.magnitude + 1) * force, 0f), ForceMode2D.Impulse);

  }
}
