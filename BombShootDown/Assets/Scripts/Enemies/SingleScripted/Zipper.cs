using System.Collections;
using UnityEngine;
public class Zipper : MonoBehaviour {
  [SerializeField] float evadeForce = 1f, dodgeRadius = 2f;
  Enemy data;
  EnemyLife currLife;
  float health;
  float speed;
  float speedmultiplier = 1f;
  float driftMag;
  float drift;
  float radiusSqr;
  Rigidbody2D rb;
  bool evadeTurn = true;
  bool newBullet = true;
  bool newBulletPause = false;
  void Awake() {
    currLife = transform.root.GetComponent<EnemyLife>();
    rb = transform.root.gameObject.GetComponent<Rigidbody2D>();
    data = currLife.data;
    speed = data.Speed;
    newDrift();
    health = data.Life;
    StartCoroutine(blink());
    radiusSqr = Mathf.Pow(dodgeRadius, 2f);
  }
  void Update() {
    changeDrift();
    if (currLife.currentLife > 0f) {
      speedmultiplier = speed + 10f * (1f - (currLife.currentLife / data.Life));
      speedmultiplier = speedmultiplier > 4f ? 4f : speedmultiplier;
    }
    if (newBulletPause == false) {
      evadingMove();
    }
    correctPosition();
  }
  void correctPosition() {
    if (transform.root.position.x > 5.25f) {
      if (rb.velocity.x > 0f) {
        moveOpposite(1);
      }
    } else if (transform.root.position.x < -5.25f) {
      if (rb.velocity.x < 0f) {
        moveOpposite(-1);
      }
    }
  }
  void moveOpposite(int side) {
    if (side > 0) {
      float xoffset = (transform.root.position.x - 5.25f) % 10.5f;
      transform.root.position = new Vector3(xoffset - 5.25f, transform.root.position.y, 0f);
    } else {
      float xoffset = (transform.root.position.x + 5.25f) % 10.5f;
      transform.root.position = new Vector3(5.25f + xoffset, transform.root.position.y, 0f);
    }
  }
  IEnumerator blink() {
    bool on = true;
    SpriteRenderer sr = transform.root.Find("Enemy").gameObject.GetComponent<SpriteRenderer>();
    Color or = sr.color;
    while (true) {
      if (on == true) {
        sr.color = new Color(or.r, or.g, or.b, 0.5f);
        on = false;
        yield return new WaitForSeconds(0.25f);
      } else {
        sr.color = new Color(or.r, or.g, or.b, 1f);
        on = true;
        yield return new WaitForSeconds(0.25f);
      }
    }
  }
  void evadingMove() {
    if (evadeTurn == false) {
      evadeTurn = true;
      return;
    }
    evadeTurn = false;
    Collider2D[] Objects = Physics2D.OverlapCircleAll(transform.root.position, dodgeRadius);
    float direction = 0f;
    bool hostileChecker = false;
    foreach (Collider2D col in Objects) {
      if (col.transform.tag == "Bullet") {
        hostileChecker = true;
        direction += -1f / (col.transform.position.x - transform.root.position.x);
      }
    }
    direction = Mathf.Abs(direction) > dodgeRadius ? dodgeRadius * direction / Mathf.Abs(direction) : direction;
    if (hostileChecker) {
      StartCoroutine(exertShiftForce(direction));
    } else {
      newBullet = true;
    }
  }
  IEnumerator exertShiftForce(float direction) {
    if (newBullet) {
      newBullet = false;
      newBulletPause = true;
      yield return new WaitForSeconds(Random.Range(0f, 0.1f));
    }
    newBulletPause = false;
    rb.AddForce(new Vector2(evadeForce * direction * rb.mass * BowManager.EnemySpeed, 0f), ForceMode2D.Impulse);
  }
  void changeDrift() {
    if (drift > 0f && rb.velocity.x < 0f) {
      drift *= -1f;
    }
    if (drift < 0f && rb.velocity.x > 0f) {
      drift *= -1f;
    }
  }
  void newDrift() {
    // drift = Random.Range(-1.5f, 1.5f);
    drift = Random.Range(0f, 0f);
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
    Vector3 normDir = new Vector3(drift, -speed, 0f);
    Vector3 newPos = old + Time.deltaTime * BowManager.EnemySpeed * normDir * speedmultiplier;
    transform.root.position = newPos;
  }
}
