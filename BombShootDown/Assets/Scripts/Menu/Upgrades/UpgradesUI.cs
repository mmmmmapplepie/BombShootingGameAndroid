using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpgradesUI : MonoBehaviour
{
  public Text currentpricetxt;
  public Sprite[] images;
  public GameObject moneyUI;
  private moneyUI moneyscript;

  // public enum Upgrades : string {UpgradeSlot = "UpgradeSlot", MaximumLife = "MaximumLife", LifeRecovery = "LifeRecovery", AmmunitionMax = "LifeRecovery", AmmunitionRate = "AmmunitionRate", Damage = "Damage", Helpers = "Helpers", BulletSpeed = "BulletSpeed", ReloadTime = "ReloadTime", BombDamage = "BombDamage", Revive = "Revive", ArmorPierce = "ArmorPierce", HitsPerHit = "HitsPerHit", Pierce = "Pierce", AoeHit = "AoeHit", Laser = "Laser", Nuke = "Nuke", ChainExplosion = "ChainExplosion", PullEnemies = "PullEnemies", DoubleGun = "DoubleGun"};
  public enum Upgrades {UpgradeSlot, MaximumLife, LifeRecovery, AmmunitionMax, AmmunitionRate, Damage, Helpers, BulletSpeed, ReloadTime, BombDamage, Revive, ArmorPierce, HitsPerHit, Pierce, AoeHit, Laser, Nuke, ChainExplosion, PullEnemies, DoubleGun};
  public Upgrades upgUI;
  void Start()
  {
    // UpgradesManager.dictionaryLog(); remove this line later as it is done in gamemanager.
    //need to figure out how to actually save the upgrades kekw and then set them when loaded
    moneyscript = moneyUI.GetComponent<moneyUI>();
    renderUpgradesUI();
  }

  public bool checkPrice(Upgrades upg) {
    string upgstring = upg.ToString();
    int[] upgrade = UpgradesManager.returnDictionaryValue(upgstring);
    if (upg == Upgrades.DoubleGun)
    {
      if (MoneyManager.money > UpgradesManager.DoubleGunPricing) {
        return true;
      } else {
        return false;
      }
    } else {
      int currentUpgLvl = upgrade[1];
      int priceweight = upgrade[2];
      int realPrice = UpgradesManager.pricing[currentUpgLvl]*priceweight;
      if (MoneyManager.money > realPrice) {
        return true;
      } else {
        return false;
      }
    }
  }
  private void renderUpgradesUI() {
    SaveSystem.saveSettings();
    string upgstring = upgUI.ToString();
    int[] upgrade = UpgradesManager.returnDictionaryValue(upgstring);
    int currentUpgLvl = upgrade[1];
    int priceweight = upgrade[2];
    int pricebase = UpgradesManager.pricing[currentUpgLvl];
    int realPrice = pricebase*priceweight;
    moneyscript.changeCurrencyUI();
    int length = 2;
    //loop through children and set spirtes
    if (upgUI != Upgrades.DoubleGun) {
      length = 11;
      currentpricetxt.text = realPrice.ToString();
    } else {
      if (upgrade[1] != 1) {
        currentpricetxt.text = UpgradesManager.DoubleGunPricing.ToString();
      } else {
        currentpricetxt.text = "0";
      }

    }
    for (int i = 1; i < length; i++) {
      GameObject child = transform.GetChild(i).gameObject;
      child.GetComponent<Image>().sprite = images[2];
    }
    for (int i = 1; i < currentUpgLvl+1; i++) {
      GameObject child = transform.GetChild(i).gameObject;
      child.GetComponent<Image>().sprite = images[1];
    }
    for (int i = 1; i < upgrade[0]+1; i++) {
      GameObject child = transform.GetChild(i).gameObject;
      child.GetComponent<Image>().sprite = images[0];
    }
  }
  public void raiseUpgrade(Upgrades upg) {
    string upgstring = upg.ToString();
    int[] upgrade = UpgradesManager.returnDictionaryValue(upgstring);
    if (upg == Upgrades.DoubleGun){
      if (upgrade[1] == 1) {
        UpgradesManager.setDictionary(upgstring, 0, 1);
      }
    } else {
      int currentLvl = upgrade[0];
      int currentUpgLvl = upgrade[1];
      if (currentUpgLvl > currentLvl && currentLvl < 10) {
        UpgradesManager.setDictionary(upgstring, 0, currentLvl+1);
      }
    }
    renderUpgradesUI();
  }
  public void lowerUpgrade(Upgrades upg) {
    string upgstring = upg.ToString();
    int[] upgrade = UpgradesManager.returnDictionaryValue(upgstring);
    if (upg == Upgrades.DoubleGun){
      if (upgrade[1] == 1) {
        UpgradesManager.setDictionary(upgstring, 0, 0);
      }
    } else {
      int currentLvl = upgrade[0];
      if (currentLvl > 1) {
        UpgradesManager.setDictionary(upgstring, 0, currentLvl - 1);
      }
    }
    renderUpgradesUI();
  }
  public void upgradeUpgrade(Upgrades upg) {
    //upgraded sound with little delay
    string upgstring = upg.ToString();
    int[] upgrade = UpgradesManager.returnDictionaryValue(upgstring);
    bool havemoney = checkPrice(upg);
    if (havemoney) {
      if (upg == Upgrades.DoubleGun){
        if (upgrade[1] != 1) {
          UpgradesManager.setDictionary(upgstring, 1, 1);
          MoneyManager.useMoney((float)UpgradesManager.DoubleGunPricing);
        }
      } else {
        int currentUpgLvl = upgrade[1];
        if (currentUpgLvl < 10) {
          int priceweight = upgrade[2];
          int realPrice = UpgradesManager.pricing[currentUpgLvl]*priceweight;
          MoneyManager.useMoney((float)realPrice);
          UpgradesManager.setDictionary(upgstring, 1, currentUpgLvl + 1);
        }
      }
    }
    renderUpgradesUI();
  }
}
