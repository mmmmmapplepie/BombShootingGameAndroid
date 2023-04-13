using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AmmoLifeReviveUI : MonoBehaviour {
  [SerializeField]
  Text remainingAmmo;
  [SerializeField]
  GameObject Revive;
  [SerializeField]
  GameObject AmmoReloadMask;
  [SerializeField]
  GameObject LifeMask;
  bool ammoLoadWait = false;
  void Update() {
    lifeRender();
    ammoRender();
    reviveRender();
  }
  void ammoRender() {
    ammoText();
    if (BowManager.CurrentAmmo < BowManager.MaxAmmo && ammoLoadWait == false) {
      ammoLoadWait = true;
      StartCoroutine("LoadAmmo");
    }
    if (BowManager.CurrentAmmo == BowManager.MaxAmmo) AmmoReloadMask.GetComponent<Image>().fillAmount = 0f;
  }
  void ammoText() {
    remainingAmmo.text = BowManager.CurrentAmmo.ToString();
  }
  IEnumerator LoadAmmo() {
    float startT = Time.time;
    float rate = (BowManager.AmmoRate * BowManager.CoolDownRate);
    while (Time.time - startT < rate) {
      float ratio = (Time.time - startT) / rate;
      if (ratio > 0f) {
        AmmoReloadMask.GetComponent<Image>().fillAmount = 1f - ratio;
      }
      yield return null;
    }
    BowManager.CurrentAmmo++;
    ammoLoadWait = false;
  }
  void lifeRender() {
    LifeMask.GetComponent<Image>().fillAmount = 1f - (LifeManager.CurrentLife / BowManager.MaxLife);
  }
  void reviveRender() {
    if (Revive.activeSelf && LifeManager.ReviveUsed == true) {
      float r = Revive.GetComponent<Image>().color.r;
      float b = Revive.GetComponent<Image>().color.b;
      float g = Revive.GetComponent<Image>().color.g;
      Revive.GetComponent<Image>().color = new Color(r, 0, 0, 0.5f);
    }
  }
  public void checkUpgradesForReviveEquipped() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("Revive")) {
      Revive.SetActive(true);
    }
  }
}
