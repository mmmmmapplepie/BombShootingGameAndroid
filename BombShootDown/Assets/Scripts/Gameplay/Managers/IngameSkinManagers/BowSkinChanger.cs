using System.Collections.Generic;
using UnityEngine;

public class BowSkinChanger : MonoBehaviour {
  [SerializeField]
  List<Skin> listOfBowSkins = new List<Skin>();
  [SerializeField]
  Transform Bow1, Bow2;
  [SerializeField]
  List<Transform> helpers = new List<Transform>();
  void Awake() {
    changeBowSkins();
  }
  Skin FindBowSkin() {
    return listOfBowSkins.Find(x => x.name == SettingsManager.currBowSkin);
  }
  void changeBowSkins() {
    Skin skin = FindBowSkin();
    changeMainBowSkin(skin);
    changeHelperSkin(skin);
  }
  void changeMainBowSkin(Skin skin) {
    changeMainBowBody(skin);
    changeStrings(Bow1, skin);
    changeBolts(Bow1, skin);
    changeStrings(Bow2, skin);
    changeBolts(Bow2, skin);
  }
  void changeMainBowBody(Skin skin) {

    Bow1.GetComponent<SpriteRenderer>().sprite = skin.mainBody;
    Bow2.GetComponent<SpriteRenderer>().sprite = skin.mainBody;
    if (skin.particleEffect != null) {
      addEffect(Bow1, skin);
      addEffect(Bow2, skin);
    }
  }
  void changeHelperSkin(Skin skin) {
    foreach (Transform helper in helpers) {
      changeHelperBody(helper, skin);
      changeStrings(helper, skin);
      changeBolts(helper, skin);
    }
  }
  void changeHelperBody(Transform tra, Skin skin) {
    tra.GetComponent<SpriteRenderer>().sprite = skin.mainBody;
  }
  void changeStrings(Transform tra, Skin skin) {
    tra.Find("LeftString").GetComponent<SpriteRenderer>().sprite = skin.LeftString;
    tra.Find("RightString").GetComponent<SpriteRenderer>().sprite = skin.RightString;
  }
  void changeBolts(Transform tra, Skin skin) {
    tra.Find("StringHolderL").GetComponent<SpriteRenderer>().sprite = skin.LeftBolt;
    tra.Find("StringHolderR").GetComponent<SpriteRenderer>().sprite = skin.RightBolt;
  }
  void addEffect(Transform parent, Skin skin) {
    GameObject effect = skin.particleEffect;
    effect.transform.localScale = skin.PS_Scale * new Vector3(1f, 1f, 1f/skin.PS_Scale);
    Instantiate(effect, parent);
  }
}
