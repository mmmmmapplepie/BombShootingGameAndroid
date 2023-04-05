using UnityEngine;
using System.Collections;

public class Helper : MonoBehaviour {
  [SerializeField]
  GameObject LeftString;
  [SerializeField]
  GameObject RightString;
  [SerializeField]
  GameObject HelperBulletPrefab;
  GameObject LockedOnEnemy;
  bool aiming = false;
  bool waiting = false;

  // || findChildBullet() == null || waiting == true
  void Update() {
    //get a lock on
    if (LockedOnEnemy == null) {
      if (waiting == false) {
        EnemyDed();
      } else {
        SnapBack();
      }
      LockedOnEnemy = GameObject.FindWithTag("TauntEnemy");
      if (LockedOnEnemy == null) {
        LockedOnEnemy = GameObject.FindWithTag("Enemy");
      }
    }
    if (LockedOnEnemy != null && findChildBullet() != null && aiming == false) {
      aiming = true;
      StartCoroutine("AimAndShoot");
    }
  }
  void Move(float stage, Vector3 Direction) {
    RotateBase(Direction);
    RotateLeft(stage);
    RotateRight(stage);
    MoveBullet(stage);
  }
  void RotateBase(Vector3 direction) {
    if (direction[1] >= 0) {
      float angle = Mathf.Atan(direction[0] / direction[1]) * 180 / Mathf.PI;
      transform.rotation = Quaternion.Euler(0, 0, -angle);
    } else {
      if (direction[0] >= 0) {
        float angle = 90 - Mathf.Atan(direction[1] / direction[0]) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(0, 0, -angle);
      } else {
        float angle = 90 + Mathf.Atan(direction[1] / direction[0]) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(0, 0, angle);
      }
    }
  }
  Transform findChildBullet() {
    Transform childBullet = null;
    foreach (Transform tra in transform) {
      if (tra.CompareTag("Bullet")) {
        childBullet = tra;
        break;
      }
    }
    return childBullet;
  }
  void RotateLeft(float ratio) {
    if (ratio < 1) {
      float angle = Mathf.Atan(ratio) * 180 / Mathf.PI;
      float length = Mathf.Sqrt(Mathf.Pow(ratio, 2f) + 1);
      Vector3 scale = new Vector3(length, 1f, 1f);
      LeftString.transform.localRotation = Quaternion.Euler(0, 0, -angle);
      LeftString.transform.localScale = scale;
    }
  }
  void RotateRight(float ratio) {
    if (ratio < 1) {
      float angle = Mathf.Atan(ratio) * 180 / Mathf.PI;
      float length = Mathf.Sqrt(Mathf.Pow(ratio, 2f) + 1);
      Vector3 scale = new Vector3(length, 1f, 1f);
      RightString.transform.localRotation = Quaternion.Euler(0, 0, angle);
      RightString.transform.localScale = scale;
    }
  }
  void MoveBullet(float ratio) {
    Transform bullet = findChildBullet();
    if (bullet != null) {
      if (ratio < 1) {
        bullet.localPosition = new Vector3(0f, 3.234f - ratio * 5, 0f);
      } else {
        bullet.localPosition = new Vector3(0f, 3.234f - 5f, 0f);
      }
    }
  }
  void EnemyDed() {
    StopCoroutine("AimAndShoot");
    SnapBack();
    aiming = false;
  }
  IEnumerator AimAndShoot() {
    float AimTime = BowManager.ReloadRate * BowManager.CoolDownRate;
    float bulletLoad = Random.Range(1f, 2f) * BowManager.AmmoRate * BowManager.CoolDownRate;
    float ratio = 0f;
    SnapBack();
    while (ratio < 1f) {
      if (LockedOnEnemy != null) {
        Vector3 EnemyPosition = LockedOnEnemy.transform.position;
        Vector3 BowPosition = transform.position;
        Vector3 Direction = EnemyPosition - BowPosition;
        ratio += 1f / 40f;
        Move(ratio, Direction);
        yield return new WaitForSeconds(AimTime / 40f);
      } else {
        EnemyDed();
        break;
      }
    }
    if (ratio >= 1f) {
      waiting = true;
      float angularDir = transform.eulerAngles.z;
      Transform bullet = findChildBullet();
      bullet.GetComponent<HelperBullet>().Shoot(angularDir);
      bullet.parent = null;
      SnapBack();
      StartCoroutine("Reload", bulletLoad);
    }
    yield return null;
  }
  IEnumerator Reload(float time) {
    yield return new WaitForSeconds(time);
    GameObject bullet = Instantiate(HelperBulletPrefab, new Vector3(0f, -10f, 0f), Quaternion.identity, transform);
    bullet.transform.localPosition = new Vector3(0f, 3.234f, 0f);
    aiming = false;
    waiting = false;
  }
  void SnapBack() {
    Transform bullet = findChildBullet();
    if (bullet != null) {
      bullet.localPosition = new Vector3(0f, 3.234f, 0f);
    }
    transform.rotation = Quaternion.Euler(0, 0, 0);
    RightString.transform.localRotation = Quaternion.Euler(0, 0, 0);
    RightString.transform.localScale = new Vector3(1f, 1f, 1f);
    LeftString.transform.localRotation = Quaternion.Euler(0, 0, 0);
    LeftString.transform.localScale = new Vector3(1f, 1f, 1f);
  }
}
