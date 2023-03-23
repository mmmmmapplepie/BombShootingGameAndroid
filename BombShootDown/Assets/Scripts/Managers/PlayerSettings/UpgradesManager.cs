using System.Collections.Generic;
using UnityEngine;
public class UpgradesManager {
  public static int[] pricing = { 0, 50, 60, 70, 80, 90, 100, 120, 140, 170, 0 };
  //translates to 880 max for pricing "1" upgrade.
  //totals to 61600 in total for ALL upgrades as of 23rd of march 2023.
  public static int DoubleGunPricing = 10000;

  public static Dictionary<string, int[]> UpgradeOptions = new Dictionary<string, int[]>();
  public static int[] returnDictionaryValue(string s) {
    return UpgradeOptions[s];
  }
  public static void setDictionary(string key, int index, int value) {
    int[] temp = UpgradeOptions[key];
    temp[index] = value;
    UpgradeOptions[key] = temp;
  }

  //[currentlvl, openlvls, pricing, upgradespacerequired]
  static void dictionaryBaseLog() {
    //world 1
    UpgradeOptions.Add("UpgradeSlot", new int[] { 1, 1, 10, 0 });
    UpgradeOptions.Add("MaximumLife", new int[] { 1, 1, 2, 1 });
    UpgradeOptions.Add("LifeRecovery", new int[] { 1, 1, 5, 2 });
    UpgradeOptions.Add("AmmunitionMax", new int[] { 1, 1, 1, 1 });
    UpgradeOptions.Add("AmmunitionRate", new int[] { 1, 1, 3, 1 });
    UpgradeOptions.Add("Damage", new int[] { 1, 1, 3, 1 });
    UpgradeOptions.Add("Helpers", new int[] { 1, 1, 5, 2 });
    UpgradeOptions.Add("BulletSpeed", new int[] { 1, 1, 1, 1 });
    UpgradeOptions.Add("ReloadTime", new int[] { 1, 1, 3, 1 });
    UpgradeOptions.Add("BombDamage", new int[] { 1, 1, 2, 1 });
    //world 2
    UpgradeOptions.Add("Revive", new int[] { 1, 1, 3, 1 });
    UpgradeOptions.Add("ArmorPierce", new int[] { 1, 1, 2, 1 });
    UpgradeOptions.Add("HitsPerHit", new int[] { 1, 1, 4, 2 });
    UpgradeOptions.Add("Pierce", new int[] { 1, 1, 4, 1 });
    UpgradeOptions.Add("AoeHit", new int[] { 1, 1, 3, 2 });
    UpgradeOptions.Add("Laser", new int[] { 1, 1, 4, 2 });
    //world 3
    UpgradeOptions.Add("Nuke", new int[] { 1, 1, 5, 2 });
    UpgradeOptions.Add("ChainExplosion", new int[] { 1, 1, 5, 2 });
    UpgradeOptions.Add("PullEnemies", new int[] { 1, 1, 5, 2 });
    //for DoubleGun [usingstatus, unlockedstatus, pricing(never used), upgslots]
    UpgradeOptions.Add("DoubleGun", new int[] { 0, 0, 5, 4 });
  }
  public static void loadAllData() {
    PlayerData data = SaveSystem.loadSettings();
    if (data != null) {
      UpgradeOptions.Clear();
      for (int i = 0; i < data.keyList.Count; i++) {
        UpgradeOptions.Add(data.keyList[i], data.upgradeStateList[i]);
      }
      SettingsManager.world = data.currentworld;
      SettingsManager.currentFocusLevelTransform = data.FocusLevelTransform;
      SettingsManager.endlessOriginalHS = data.endlessOriginalHS;
      SettingsManager.endlessUpgradedHS = data.endlessUpgradedHS;
      MoneyManager.money = data.money;
      SettingsManager.currBowSkin = data.currBowSkin;
      SettingsManager.currBulletSkin = data.currBulletSkin;
      SettingsManager.currFortressSkin = data.currFortressSkin;
      SettingsManager.unlockedBowSkin = data.unlockedBowSkin;
      SettingsManager.unlockedBulletSkin = data.unlockedBulletSkin;
      SettingsManager.unlockedFortressSkin = data.unlockedFortressSkin;
    } else {
      UpgradeOptions.Clear();
      dictionaryBaseLog();
      SettingsManager.world = new int[2] { 3, 1 };
      SettingsManager.currentFocusLevelTransform = new float[2] { 443, 682 };
      SettingsManager.endlessOriginalHS = 0f;
      SettingsManager.endlessUpgradedHS = 0f;
      MoneyManager.money = 300;
      SettingsManager.currBowSkin = "Bronze Bow";
      SettingsManager.currBulletSkin = "Wooden Bullet";
      SettingsManager.currFortressSkin = "Wooden Fortress";
      SettingsManager.unlockedBowSkin = new List<string>() { "Wooden Bow", "Bronze Bow" };
      SettingsManager.unlockedBulletSkin = new List<string>() { "Wooden Bullet" };
      SettingsManager.unlockedFortressSkin = new List<string>() { "Wooden Fortress" };
    }
  }
}
