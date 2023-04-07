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
    UpgradesManager.loadAllData();
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
    emptyEffectsChild(BowMain.transform);
    if (currBow.particleEffect != null) {
      addEffect(BowMain.transform, currBow);
    }
  }
  void changeBullet() {
    Bullet.sprite = currBullet.mainBody;
    emptyEffectsChild(Bullet.transform);
    if (currBullet.particleEffect != null) {
      addEffect(Bullet.transform, currBullet);
    }
  }
  void changeFortress() {
    Fortress.sprite = currFortress.mainBody;
    emptyEffectsChild(Fortress.transform);
    if (currFortress.particleEffect != null) {
      addEffect(Fortress.transform, currFortress);
    }
  }
  void addEffect(Transform parent, Skin skin) {
    print(parent.localScale);
    print(parent.position);
    GameObject PSprefab = skin.particleEffect;
    float scale = parent.gameObject.GetComponent<RectTransform>().rect.size.y * skin.PS_Scale / 80f;
    PSprefab.transform.localScale = new Vector3(scale, scale, 1f);
    PSprefab.transform.position = Vector3.zero;
    Instantiate(PSprefab, parent);
  }
  void emptyEffectsChild(Transform parent) {
    if (parent.childCount > 0) {
      foreach (Transform tra in parent) {
        Destroy(tra.gameObject);
      }
    }
  }
}
