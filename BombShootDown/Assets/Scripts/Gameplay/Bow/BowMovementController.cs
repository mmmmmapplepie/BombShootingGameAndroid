using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowMovementController : MonoBehaviour {
  GameObject childBullet;
  [SerializeField]
  GameObject Bow1;
  [SerializeField]
  GameObject Bow2;
  void Update() {
    touchMove(0);
    if (UpgradesEquipped.EquippedUpgrades.Contains("DoubleGun")) {
      touchMove(1);
    }
  }
  void touchMove(int i) {
    if (BowManager.GunsReady == false) {
      BowManager.bowTouchID[0] = -1;
      BowManager.bowTouchID[1] = -1;
      ReturnBow(Bow1);
      ReturnBow(Bow2);
      return;
    }
    if (UpgradesEquipped.EquippedUpgrades.Contains("DoubleGun")) {
      if (Input.touchCount > 0 + i && !BowManager.UsingCooldown) {
        Touch touch = Input.GetTouch(i);
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
        touchPos.z = 0f;
        //only move 1st bow-------------------------------------
        if (findChildBullet(Bow1)) {
          if (touch.phase == TouchPhase.Began && touchPos.x <= 0f) {
            if (BowManager.bowTouchID[0] != -1) {
              //-1 is the "taken" indicator
              return;
            }
            BowManager.bowTouchID[0] = touch.fingerId;
            BowManager.center[0] = touchPos;
          }
          BowControl(touch, 0, Bow1);
        }
        if (findChildBullet(Bow2)) {
          if (touch.phase == TouchPhase.Began && touchPos.x > 0f) {
            if (BowManager.bowTouchID[1] != -1) {
              //-1 is the "taken" indicator
              return;
            }
            BowManager.bowTouchID[1] = touch.fingerId;
            BowManager.center[1] = touchPos;
          }
          BowControl(touch, 1, Bow2);
        }
      }
    }
    // single case
    else {
      if (Input.touchCount > 0 + i && !BowManager.UsingCooldown) {
        Touch touch = Input.GetTouch(i);
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
        touchPos.z = 0f;
        if (findChildBullet(Bow1)) {
          if (touch.phase == TouchPhase.Began) {
            if (BowManager.bowTouchID[0] != -1) {
              //-1 is the "taken" indicator
              return;
            }
            BowManager.bowTouchID[0] = touch.fingerId;
            BowManager.center[0] = touchPos;
          }
          BowControl(touch, 0, Bow1);
        }
      }
    }
  }
  void BowControl(Touch touch, int i, GameObject bow) {
    Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
    touchPos.z = 0f;
    if (touch.phase == TouchPhase.Ended && touch.fingerId == BowManager.bowTouchID[i]) {
      Vector3 diff = BowManager.center[i] - touchPos;
      if (diff.sqrMagnitude > 1.5) {
        ControlShoot(bow, diff.magnitude);
      } else {
        ReturnBow(bow);
      }
      BowManager.bowTouchID[i] = -1;
    }
    if (touch.phase == TouchPhase.Moved && touch.fingerId == BowManager.bowTouchID[i]) {
      ControlMovement(bow, touchPos, BowManager.center[i]);
    }
  }
  GameObject findChildBullet(GameObject Bow) {
    childBullet = null;
    Transform parent = Bow.transform;
    foreach (Transform tra in parent) {
      if (tra.CompareTag("Bullet")) {
        childBullet = tra.gameObject;
        break;
      }
    }
    return childBullet;
  }
  void ControlMovement(GameObject bow, Vector3 pos, Vector3 cent) {
    bow.GetComponent<MainAim>().DragMove(pos, cent);
  }
  void ControlShoot(GameObject bow, float mag) {
    bow.GetComponent<MainAim>().ShootBullet(mag);
  }
  void ReturnBow(GameObject bow) {
    bow.GetComponent<MainAim>().SnapBack();
  }
}
