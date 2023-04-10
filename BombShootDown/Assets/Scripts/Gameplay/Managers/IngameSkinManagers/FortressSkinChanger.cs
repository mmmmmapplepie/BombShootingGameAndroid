using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortressSkinChanger : MonoBehaviour {
  [SerializeField]
  List<Skin> listOfFortressSkins = new List<Skin>();
  [SerializeField]
  GameObject fortress;
  void Awake() {
    Skin skin = FindFortressSkin();
    fortress.GetComponent<SpriteRenderer>().sprite = skin.mainBody;
    addEffect(skin);
  }
  Skin FindFortressSkin() {
    return listOfFortressSkins.Find(x => x.name == SettingsManager.currFortressSkin);
  }
  void addEffect(Skin skin) {
    if (skin.particleEffect != null) {
      Transform tra = fortress.transform;
      GameObject effect = skin.particleEffect;
      GameObject PS = Instantiate(effect, effect.transform.localPosition, Quaternion.identity, tra);
      PS.transform.localScale = skin.PS_Scale * Vector3.one / tra.localScale.x;
    }
  }
}
