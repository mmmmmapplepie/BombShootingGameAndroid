using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skinChanger : MonoBehaviour {
  [SerializeField]
  Image Fortress;
  [SerializeField]
  Image BowMain, LS, RS, LB, RB;
  [SerializeField]
  Image Bullet;
  temporarySkinHolder temp;

  Skin currFortress;
  Skin currBullet;
  Skin currBow;
  void Awake() {
    temp = gameObject.GetComponent<temporarySkinHolder>();
  }
  void Start() {
    currFortress = temp.tempFortress;
    currBullet = temp.tempBullet;
    currBow = temp.tempBow;
    changeBow();
    changeBullet();
    changeFortress();
  }
  void Update() {
    if (currBow != temp.tempBow) {
      currBow = temp.tempBow;
      changeBow();
    }
    if (currBullet != temp.tempBullet) {
      currBullet = temp.tempBullet;
      changeBullet();
    }
    if (currFortress != temp.tempFortress) {
      currFortress = temp.tempFortress;
      changeFortress();
    }
  }
  void changeBow() {
    BowMain.sprite = currBow.mainBody;
    LS.sprite = currBow.LeftString;
    RS.sprite = currBow.RightString;
    LB.sprite = currBow.LeftBolt;
    RB.sprite = currBow.RightBolt;
    if (currBow.particleEffect != null) {
      addEffect(BowMain.transform, currBow.particleEffect);
    }
  }
  void changeBullet() {
    Bullet.sprite = currBullet.mainBody;
    if (currBullet.particleEffect != null) {
      addEffect(Bullet.transform, currBullet.particleEffect);
    }
  }
  void changeFortress() {
    Fortress.sprite = currFortress.mainBody;
    if (currFortress.particleEffect != null) {
      addEffect(Fortress.transform, currFortress.particleEffect);
    }
  }
  void addEffect(Transform parent, GameObject effect) {
    emptyEffectsChild(parent);
    Instantiate(effect, parent.position, Quaternion.identity, parent);
  }
  void emptyEffectsChild(Transform parent) {
    if (parent.childCount > 0) {
      foreach (Transform tra in parent) {
        Destroy(tra.gameObject);
      }
    }
  }
}
