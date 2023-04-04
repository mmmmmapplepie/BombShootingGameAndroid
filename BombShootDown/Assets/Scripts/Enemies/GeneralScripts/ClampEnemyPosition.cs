using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampEnemyPosition : MonoBehaviour {
  Rigidbody2D rb;
  void Awake() {
    rb = GetComponent<Rigidbody2D>();
  }
  void Update() {
    if (Time.timeScale == 0f) {
      return;
    }
    if (transform.position.x > 5.25f) {
      if (rb.velocity.x > 0f) {
        float x = rb.velocity.x;
        float y = rb.velocity.y;
        rb.velocity = new Vector2(-x, y);
      }
      correctPosition(5.1f);
    } else if (transform.position.x < -5.25f) {
      if (rb.velocity.x < 0f) {
        float x = rb.velocity.x;
        float y = rb.velocity.y;
        rb.velocity = new Vector2(-x, y);
      }
      correctPosition(-5.1f);
    }
  }
  void correctPosition(float x) {
    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-(5f * transform.position.x * gameObject.GetComponent<Rigidbody2D>().mass - x), 0f));
  }
}
