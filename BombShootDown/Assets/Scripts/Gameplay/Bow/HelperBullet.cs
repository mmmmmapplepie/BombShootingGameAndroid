using UnityEngine;
using System.Collections.Generic;

public class HelperBullet : MonoBehaviour {
  [SerializeField]
  GameObject HitEffect;
  AudioManagerCannon audioManager;
  int hits;
  float damage;
  int pierce;
  float speed;
  bool used = false;
  void Awake() {
    audioManager = GameObject.Find("AudioManagerCannon").GetComponent<AudioManagerCannon>();
    gameObject.GetComponent<CircleCollider2D>().enabled = false;
  }
  void Start() {
    if (GameObject.Find("SkinManager") != null) {
      GameObject.Find("SkinManager").GetComponent<BulletSkinChanger>().changeBulletSprite(gameObject);
    }
  }
  void Update() {
    if (transform.position.x > 7f || transform.position.x < -7f || transform.position.y > 13f || transform.position.y < -13f) {
      Destroy(gameObject);
    }
  }
  public void Shoot(float angle) {
    float x = 0;
    float y = 1;
    if (angle >= 0f && angle < 90f) {
      float input = angle * Mathf.PI / 180;
      x = -Mathf.Sin(input);
      y = Mathf.Cos(input);
    } else if (angle >= 90f && angle < 180f) {
      float a = angle - 90f;
      float input = a * Mathf.PI / 180;
      x = -Mathf.Cos(input);
      y = -Mathf.Sin(input);
    } else if (angle >= 180f && angle < 270f) {
      float a = angle - 180f;
      float input = a * Mathf.PI / 180;
      x = Mathf.Sin(input);
      y = -Mathf.Cos(input);
    } else if (angle >= 270f && angle < 360f) {
      float a = angle - 270f;
      float input = a * Mathf.PI / 180;
      x = Mathf.Cos(input);
      y = Mathf.Sin(input);
    }
    Vector3 direction = new Vector3(x, y, 0f);
    SetHelperBulletSettings();
    shootSound(speed * direction.magnitude);
    GetComponent<Rigidbody2D>().velocity = speed * direction;
  }
  void SetHelperBulletSettings() {
    if (BowManager.HitsPerHit > 5) {
      hits = 1;
    }
    if (BowManager.HitsPerHit > 10) {
      hits = 2;
    }
    damage = BowManager.HelperDmg * BowManager.BulletMultiplier;
    pierce = BowManager.Pierce;
    speed = BowManager.BulletSpeed * BowManager.BulletMultiplier;
    gameObject.GetComponent<CircleCollider2D>().enabled = true;
  }

  void CreateEffect(GameObject prefab, Transform parent, Vector3 pos) {
    GameObject effect = Instantiate(prefab, pos, Quaternion.identity, parent);
  }

  void OnTriggerEnter2D(Collider2D coll) {
    if (used == true) {
      return;
    }
    if (coll.gameObject.tag == "TauntEnemy" || coll.gameObject.tag == "Enemy") {
      EnemyLife life = coll.transform.root.gameObject.GetComponent<EnemyLife>();
      Transform enemyCenter = coll.transform.root;
      life.takeDamage(damage);
      CreateEffect(HitEffect, enemyCenter, enemyCenter.position);
      for (int i = 0; i < hits; i++) {
        life.takeDamage(damage);
        if (life.currentLife > 0f) {
          CreateEffect(HitEffect, enemyCenter, enemyCenter.position);
        }
      }
      pierce--;
      if (pierce <= 0) {
        gameObject.GetComponent<Collider2D>().enabled = false;
        used = true;
        Destroy(gameObject);
      }
    }
  }
  void shootSound(float speed) {
    if (speed < 10f) {
      audioManager.PlayAudio("SlowShot");
    } else if (speed < 30f) {
      audioManager.PlayAudio("MidShot");
    } else {
      audioManager.PlayAudio("FastShot");
    }
  }
}
