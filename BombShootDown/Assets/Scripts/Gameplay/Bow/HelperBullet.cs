using UnityEngine;

public class HelperBullet : MonoBehaviour
{
  int hits;
  float damage;
  int pierce;
  float speed;
  void Awake() {
    gameObject.GetComponent<CircleCollider2D>().enabled = false;
  }
  void Update() {
    // destroy when outside area
    if (transform.position.x > 7f || transform.position.x < -7f || transform.position.y > 13f || transform.position.y < -13f) {
      Destroy(gameObject);
    }
  }
  public void Shoot(float angle) {
    float x = 0;
    float y = 1;
    if (angle >= 0f && angle < 90f) {
      float input = angle*Mathf.PI/180;
      x = -Mathf.Sin(input);
      y = Mathf.Cos(input);
    } else if (angle >= 90f && angle < 180f) {
      float a = angle - 90f;
      float input = a*Mathf.PI/180;
      x = -Mathf.Cos(input);
      y = -Mathf.Sin(input);
    } else if (angle >= 180f && angle < 270f) {
      float a = angle - 180f;
      float input = a*Mathf.PI/180;
      x = Mathf.Sin(input);
      y = -Mathf.Cos(input);
    } else if (angle >= 270f && angle < 360f) {
      float a = angle - 270f;
      float input = a*Mathf.PI/180;
      x = Mathf.Cos(input);
      y = Mathf.Sin(input);
    }
    Vector3 direction = new Vector3(x, y, 0f);
    SetHelperBulletSettings();
    GetComponent<Rigidbody2D>().velocity = speed*direction;
  }
  void SetHelperBulletSettings() {
    hits = BowManager.HitsPerHit;
    damage = BowManager.HelperDmg;
    pierce = BowManager.Pierce;
    speed = BowManager.BulletSpeed;
    gameObject.GetComponent<CircleCollider2D>().enabled = true;
  }


  void  OnTriggerEnter2D(Collider2D coll) {
    if (coll.gameObject.tag == "TauntEnemy" || coll.gameObject.tag == "Enemy") {
      EnemyLife life = coll.transform.parent.gameObject.GetComponent<EnemyLife>();
      life.takeDamage(damage);
      life.HitsPerHit(hits, damage);
      pierce--;
      if (pierce <= 0) {
        Destroy(gameObject);
      }
    }
  }



}
