using UnityEngine;
using UnityEngine.UI;

public class skinPrefabSetup : MonoBehaviour {
  [HideInInspector]
  public Skin skin;
  [SerializeField]
  Image main, LS = null, RS = null, LB = null, RB = null;
  [SerializeField]
  GameObject prePurchasePanel;
  [SerializeField]
  Text skinName, price;
  [SerializeField]
  GameObject confirmationPanel, equipBtn;
  temporarySkinHolder temp;
  void Start() {
    main.sprite = skin.mainBody;
    if (skin.type == Skin.skinType.Bow) {
      LS.sprite = skin.LeftString;
      RS.sprite = skin.RightString;
      LB.sprite = skin.LeftBolt;
      RB.sprite = skin.LeftBolt;
    }
    skinName.text = skin.name;
    price.text = skin.price.ToString();
    boughtOrNotCheck();
    temp = GameObject.Find("PreviewBox").GetComponent<temporarySkinHolder>();
  }
  void Update() {
    if (skin.type == Skin.skinType.Bow && SettingsManager.unlockedBowSkin.Contains(skin.name)) {
      equipBtn.SetActive(true);
      if (skin.name == SettingsManager.currBowSkin) {
        equipBtn.SetActive(false);
      }
    }
    if (skin.type == Skin.skinType.Bullet && SettingsManager.unlockedBulletSkin.Contains(skin.name)) {
      equipBtn.SetActive(true);
      if (skin.name == SettingsManager.currBulletSkin) {
        equipBtn.SetActive(false);
      }
    }
    if (skin.type == Skin.skinType.Fortress && SettingsManager.unlockedFortressSkin.Contains(skin.name)) {
      equipBtn.SetActive(true);
      if (skin.name == SettingsManager.currFortressSkin) {
        equipBtn.SetActive(false);
      }
    }
  }
  void boughtOrNotCheck() {
    if (skin.type == Skin.skinType.Bow && SettingsManager.unlockedBowSkin.Contains(skin.name)) {
      prePurchasePanel.SetActive(false);
      equipBtn.SetActive(true);
    }
    if (skin.type == Skin.skinType.Bullet && SettingsManager.unlockedBulletSkin.Contains(skin.name)) {
      prePurchasePanel.SetActive(false);
      equipBtn.SetActive(true);
    }
    if (skin.type == Skin.skinType.Fortress && SettingsManager.unlockedFortressSkin.Contains(skin.name)) {
      prePurchasePanel.SetActive(false);
      equipBtn.SetActive(true);
    }
  }
  public void closeConfirmation() {
    confirmationPanel.SetActive(false);
  }
  public void checkConfirmation() {
    if (MoneyManager.money > skin.price) {
      confirmationPanel.SetActive(true);
    }
  }
  public void buyUpgrade() {
    if (skin.type == Skin.skinType.Bow) {
      SettingsManager.unlockedBowSkin.Add(skin.name);
    }
    if (skin.type == Skin.skinType.Bullet) {
      SettingsManager.unlockedBulletSkin.Add(skin.name);
    }
    if (skin.type == Skin.skinType.Fortress) {
      SettingsManager.unlockedFortressSkin.Add(skin.name);
    }
    MoneyManager.useMoney(skin.price);
    boughtOrNotCheck();
    closeConfirmation();
    SaveSystem.saveSettings();
  }
  public void changePreview() {
    if (skin.type == Skin.skinType.Bow) {
      temp.tempBow = skin;
    }
    if (skin.type == Skin.skinType.Bullet) {
      temp.tempBullet = skin;
    }
    if (skin.type == Skin.skinType.Fortress) {
      temp.tempFortress = skin;
    }
  }
  public void equip() {
    if (skin.type == Skin.skinType.Bow) {
      SettingsManager.currBowSkin = skin.name;
    }
    if (skin.type == Skin.skinType.Bullet) {
      SettingsManager.currBulletSkin = skin.name;
    }
    if (skin.type == Skin.skinType.Fortress) {
      SettingsManager.currFortressSkin = skin.name;
    }
    changePreview();
    SaveSystem.saveSettings();
  }
}
