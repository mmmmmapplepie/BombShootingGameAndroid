using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class temporarySkinHolder : MonoBehaviour {
  public GameObject clickedButton;
  [HideInInspector]
  public Skin tempBow, tempBullet, tempFortress;
  [SerializeField]
  public List<Skin> allBowSkins;
  [SerializeField]
  public List<Skin> allBulletSkins;
  [SerializeField]
  public List<Skin> allFortressSkins;
  [SerializeField]
  GameObject bowpanel, bulletpanel, fortresspanel;
  [SerializeField]
  GameObject bowPreview, fortressPreview;

  void Awake() {
    tempBow = FindSkin(allBowSkins, SettingsManager.currBowSkin);
    tempBullet = FindSkin(allBulletSkins, SettingsManager.currBulletSkin);
    tempFortress = FindSkin(allFortressSkins, SettingsManager.currFortressSkin);
  }
  public void changeTempBow(string name) {
    tempBow = FindSkin(allBowSkins, name);
  }
  public void changeTempBullet(string name) {
    tempBullet = FindSkin(allBulletSkins, name);
  }
  public void changeTempFortress(string name) {
    tempFortress = FindSkin(allFortressSkins, name);
  }
  Skin FindSkin(List<Skin> searchList, string name) {
    return searchList.Find(x => x.name == SettingsManager.currFortressSkin);
  }
  public void changeClickedButton(GameObject newBtn) {
    Color original = newBtn.GetComponent<Image>().color;
    Color newColor = new Color(0.9f, 0.2f, 0.1f, 1f);
    clickedButton.GetComponent<Image>().color = original;
    newBtn.GetComponent<Image>().color = newColor;
    clickedButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(clickedButton.GetComponent<RectTransform>().anchoredPosition.x, 0f);
    newBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(newBtn.GetComponent<RectTransform>().anchoredPosition.x, 30f);
    clickedButton = newBtn;
    changeSkinPanel();
  }
  void changeSkinPanel() {
    bowpanel.SetActive(false);
    bulletpanel.SetActive(false);
    fortresspanel.SetActive(false);
    if (clickedButton.name == "BowBtn") {
      bowpanel.SetActive(true);
      bowPreview.SetActive(true);
      fortressPreview.SetActive(false);
    }
    if (clickedButton.name == "BulletBtn") {
      bulletpanel.SetActive(true);
      bowPreview.SetActive(true);
      fortressPreview.SetActive(false);
    }
    if (clickedButton.name == "FortressBtn") {
      fortresspanel.SetActive(true);
      bowPreview.SetActive(false);
      fortressPreview.SetActive(true);
    }
  }
}
