using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temporarySkinHolder : MonoBehaviour {
  public Skin tempBow;
  public Skin tempBullet;
  public Skin tempFortress;
  [SerializeField]
  List<Skin> allBowSkins;
  [SerializeField]
  List<Skin> allBulletSkins;
  [SerializeField]
  List<Skin> allFortressSkins;
  void Awake() {
    tempBow = FindSkin(allBowSkins, SettingsManager.currBowSkin);
    tempBullet = FindSkin(allBulletSkins, SettingsManager.currBulletSkin);
    tempFortress = FindSkin(allFortressSkins, SettingsManager.currFortressSkin);
  }
  public void changeTempBow(string name) {
    tempBow = FindSkin(allBowSkins, name);
  }
  public void changeTempBullet(string name) {
    tempBow = FindSkin(allBulletSkins, name);
  }
  public void changeTempFortress(string name) {
    tempBow = FindSkin(allFortressSkins, name);
  }
  Skin FindSkin(List<Skin> searchList, string name) {
    return searchList.Find(x => x.name == SettingsManager.currFortressSkin);
  }
}
