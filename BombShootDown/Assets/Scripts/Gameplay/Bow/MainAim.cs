using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainAim : MonoBehaviour {
  [SerializeField]
  GameObject LeftString;
  [SerializeField]
  GameObject RightString;
  public void DragMove(Vector3 currPos, Vector3 center) {
    Vector3 dragDirection = currPos - center;
    float ratio = (dragDirection.magnitude) * Mathf.Sqrt(3f) / 5f;
    RotateBase(dragDirection);
    RotateLeft(ratio);
    RotateRight(ratio);
    MoveBullet(ratio);
  }
  void RotateBase(Vector3 direction) {
    if (direction.y <= 0) {
      float angle = Mathf.Atan(direction.x / direction.y) * 180 / Mathf.PI;
      transform.rotation = Quaternion.Euler(0, 0, -angle);
    } else {
      if (direction.x >= 0) {
        float angle = 90 + Mathf.Atan(direction.y / direction.x) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(0, 0, angle);
      } else {
        float angle = -90 + Mathf.Atan(direction.y / direction.x) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(0, 0, angle);
      }
    }
  }
  //maximum stretch goes to double length???? so scale = 2 at that point.
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
    if (ratio < Mathf.Sqrt(3f)) {
      float angle = Mathf.Atan(ratio) * 180 / Mathf.PI;
      float length = Mathf.Sqrt(Mathf.Pow(ratio, 2f) + 1);
      Vector3 scale = new Vector3(length, 1f, 1f);
      LeftString.transform.localRotation = Quaternion.Euler(0, 0, -angle);
      LeftString.transform.localScale = scale;
    }
  }
  void RotateRight(float ratio) {
    if (ratio < Mathf.Sqrt(3f)) {
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
      if (ratio < Mathf.Sqrt(3f)) {
        bullet.localPosition = new Vector3(0f, 3.234f - ratio * 5, 0f);
      } else {
        bullet.localPosition = new Vector3(0f, 3.234f - Mathf.Sqrt(3f) * 5, 0f);
      }
    }
  }
  public void ShootBullet(float mag) {
    float ratio = mag * Mathf.Sqrt(3f) / 5f;
    if (ratio >= Mathf.Sqrt(3f)) {
      ratio = Mathf.Sqrt(3f);
    }
    float angularDir = transform.eulerAngles.z;
    Transform bullet = findChildBullet();
    if (bullet != null) {
      bullet.GetComponent<Bullet>().Shoot(angularDir, ratio);
      bullet.parent = null;
    }
    // StartCoroutine("ReturnBack");
    SnapBack();
  }
  public void SnapBack() {
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
  //  COROUTINE SEEMS TOO COMPLICATED AS I CANT BE BOTHERED ATM =--------------------------------------------------------
  // IEnumerator ReturnBack() {
  //   float time = 0f;
  //   float ReturnPeriod = 1/BowManager.BulletSpeed;
  //   float interval = ReturnPeriod/5;
  //   Vector3 rightstringtemp = RightString.transform.localScale;
  //   Vector3 rightstringrottemp = RightString.transform.localRotation;
  //   Vector3 leftstringtemp = LeftString.transform.localRotation;
  //   Vector3 leftstringrottemp = LeftString.transform.localRotation;
  //   Vector3 baserottemp = transform.rotation;
  //   if (time < ReturnPeriod) {
  //     time += interval;
  //     RightString.transform.localRotation = Quaternion.Lerp(rightstringrottemp, Quaternion.Euler(0, 0, 0), time);
  //     RightString.transform.localRotation = Quaternion.Lerp(rightstringtemp, new Vector3 (1f, 1f, 1f), time);
  //     LeftString.transform.localRotation = Quaternion.Lerp(leftstringrottemp, Quaternion.Euler(0, 0, 0), time);
  //     LeftString.transform.localRotation = Quaternion.Lerp(leftstringtemp, new Vector3 (1f, 1f, 1f), time);
  //     RightString.transform.localRotation = Quaternion.Lerp(baserottemp, Quaternion.Euler(0, 0, 0), time);

  //   }
  // do the retuning thing in like.... 2 frames
  //   yield return null;
  // }
  // base positions for stuff (locally!) (string pivots are at (-5.5, 3.234) and (5.5, 3.234) respectively) (scale are both at 1 at the start point) same for the ball.
}
