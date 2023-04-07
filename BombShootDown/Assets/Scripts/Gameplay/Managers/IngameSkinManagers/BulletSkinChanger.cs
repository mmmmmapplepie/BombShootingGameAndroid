using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSkinChanger : MonoBehaviour {
  [SerializeField]
  List<Skin> listOfBulletSkins = new List<Skin>();
  Skin currSkin;
  GameObject effect = null;
  void Awake() {
    currSkin = FindBulletSkin();
    if (currSkin.particleEffect != null) {
      effect = currSkin.particleEffect;
    }
  }
  Skin FindBulletSkin() {
    return listOfBulletSkins.Find(x => x.name == SettingsManager.currBulletSkin);
  }
  public void changeBulletSprite(GameObject ob) {
    ob.GetComponent<SpriteRenderer>().sprite = currSkin.mainBody;
    if (effect != null) {
      Transform tra = ob.GetComponent<Transform>();
      effect.transform.localScale = new Vector3(currSkin.PS_Scale, currSkin.PS_Scale, 1f / currSkin.PS_Scale);
      Instantiate(effect, tra);
    }
  }
}
