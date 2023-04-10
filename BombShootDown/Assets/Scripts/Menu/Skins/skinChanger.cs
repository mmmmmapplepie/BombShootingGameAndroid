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
    if (currBow.particleEffect != null) {
      StartCoroutine(BowEffectAdd());
    } else {
      emptyEffectsChild(BowMain.transform);
    }
  }
  void changeBullet() {
    Bullet.sprite = currBullet.mainBody;
    emptyEffectsChild(Bullet.transform);
    if (currBullet.particleEffect != null) {
      addEffect(Bullet.transform, currBullet);
    } else {
      emptyEffectsChild(Bullet.transform);
    }
  }
  void changeFortress() {
    Fortress.sprite = currFortress.mainBody;
    emptyEffectsChild(Fortress.transform);
    if (currFortress.particleEffect != null) {
      addEffect(Fortress.transform, currFortress);
    } else {
      emptyEffectsChild(Fortress.transform);
    }
  }
  IEnumerator BowEffectAdd() {
    yield return null;
    emptyEffectsChild(BowMain.transform);
    addEffect(BowMain.transform, currBow);
  }
  void addEffect(Transform parent, Skin skin) {
    if (skin.type == Skin.skinType.Fortress) {
      fortressPS(parent, skin);
    }
    if (skin.type == Skin.skinType.Bullet) {
      bulletPS(parent, skin);
    }
    if (skin.type == Skin.skinType.Bow) {
      bowPS(parent, skin);
    }
  }
  void fortressPS(Transform parent, Skin skin) {
    GameObject PSprefab = skin.particleEffect;
    float scaleRatio = parent.gameObject.GetComponent<RectTransform>().rect.size.x / 900f;
    GameObject PS = Instantiate(PSprefab, parent);
    //because pivot in this case is from bottom of image we add the 10 units.
    PS.transform.localPosition = new Vector3(0f, 80f * scaleRatio * (PSprefab.transform.localPosition.y + 10f), 0f);
    PS.transform.localScale = new Vector3(scaleRatio * skin.PS_Scale, scaleRatio * skin.PS_Scale, 1f);
    SpriteMask(PS);
    //for some reason the child objects dont seem to actually scale down with the parent. (same with the position as now they are in pixel units and not "units aka. 80px/unit"). So we have to manually scale them.
    foreach (Transform tra in PS.transform) {
      float xscale = tra.localScale.x * scaleRatio * skin.PS_Scale;
      float yscale = tra.localScale.y * scaleRatio * skin.PS_Scale;
      tra.transform.localScale = new Vector3(xscale, yscale, 1f);
      tra.localPosition = 80f * (new Vector3(tra.localPosition.x, tra.localPosition.y, 0f));
      SpriteMask(tra.gameObject);
    }
  }
  void bulletPS(Transform parent, Skin skin) {
    GameObject PSprefab = skin.particleEffect;
    GameObject PS = Instantiate(PSprefab, parent);
    PS.transform.localPosition = new Vector3(0f, 0f, 0f);
    float scale = (parent.gameObject.GetComponent<RectTransform>().rect.size.x / 80f) * PSprefab.transform.localScale.x;
    PS.transform.localScale = new Vector3(scale, scale, 1f);
    foreach (Transform tra in PS.transform) {
      float xscale = tra.localScale.x * scale;
      float yscale = tra.localScale.y * scale;
      tra.transform.localScale = new Vector3(xscale, yscale, 1f);
    }
  }
  void bowPS(Transform parent, Skin skin) {
    GameObject PSprefab = skin.particleEffect;
    float scale = (parent.gameObject.GetComponent<RectTransform>().rect.size.x) / (parent.gameObject.GetComponent<Image>().sprite.bounds.extents.x * 2f * 0.2f * 80f);
    GameObject PS = Instantiate(PSprefab, parent);
    PS.transform.localScale = new Vector3(scale, scale, 1f);
    PS.transform.localPosition = new Vector3(0f, 0f, 0f);
    SpriteMask(PS);
    foreach (Transform tra in PS.transform) {
      float xscale = tra.localScale.x * skin.PS_Scale * scale;
      float yscale = tra.localScale.y * skin.PS_Scale * scale;
      tra.transform.localScale = new Vector3(xscale, yscale, 1f);
      tra.localPosition = new Vector3(tra.localPosition.x * 80f, tra.localPosition.y * 80f, 0f);
      SpriteMask(tra.gameObject);
    }
  }
  void emptyEffectsChild(Transform parent) {
    if (parent.childCount > 0) {
      foreach (Transform tra in parent) {
        Destroy(tra.gameObject);
      }
    }
  }
  void SpriteMask(GameObject GO) {
    ParticleSystemRenderer masker = GO.GetComponent<ParticleSystemRenderer>();
    masker.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
  }
}
