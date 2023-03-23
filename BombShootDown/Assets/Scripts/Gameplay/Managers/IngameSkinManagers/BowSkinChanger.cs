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
    changeMainBowSkin(skin, skin.particleEffect);
    changeHelperSkin(skin);
  }
  void changeMainBowSkin(Skin skin, GameObject effect) {
    changeMainBowBody(skin, skin.particleEffect);
    changeStrings(Bow1, skin);
    changeBolts(Bow1, skin);
    changeStrings(Bow2, skin);
    changeBolts(Bow2, skin);
  }
  void changeMainBowBody(Skin skin, GameObject effect) {

    Bow1.GetComponent<SpriteRenderer>().sprite = skin.mainBody;
    Bow2.GetComponent<SpriteRenderer>().sprite = skin.mainBody;
    if (effect != null) {
      addEffect(Bow1, effect);
      addEffect(Bow2, effect);
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
  void addEffect(Transform parent, GameObject effect) {
    Instantiate(effect, parent.position, parent.rotation, parent);
  }
}
