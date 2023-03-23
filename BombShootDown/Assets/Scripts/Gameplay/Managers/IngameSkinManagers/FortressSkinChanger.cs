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
  }
  Skin FindFortressSkin() {
    return listOfFortressSkins.Find(x => x.name == SettingsManager.currFortressSkin);
  }
}
