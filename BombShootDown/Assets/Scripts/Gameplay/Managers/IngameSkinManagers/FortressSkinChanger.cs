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
      effect.transform.localScale = skin.PS_Scale * new Vector3(1f, 1f, 1f / skin.PS_Scale);
      Instantiate(effect, tra);
    }
  }
}
