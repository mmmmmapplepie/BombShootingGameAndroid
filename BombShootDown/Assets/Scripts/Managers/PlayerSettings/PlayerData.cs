using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData {
  #region upgrades related
  public List<string> keyList = new List<string>();
  public List<int[]> upgradeStateList = new List<int[]>();
  public int money;
  #endregion
  #region progress related
  public int[] currentworld;
  public float[] FocusLevelTransform;
  public float endlessOriginalHS;
  public float endlessUpgradedHS;
  #endregion
  #region skin related
  public string currFortressSkin = null;
  public string currBowSkin = null;
  public string currBulletSkin = null;
  public List<string> unlockedFortressSkin = new List<string>();
  public List<string> unlockedBowSkin = new List<string>();
  public List<string> unlockedBulletSkin = new List<string>();
  #endregion


  public PlayerData() {
    Dictionary<string, int[]> UM = UpgradesManager.UpgradeOptions;
    foreach (KeyValuePair<string, int[]> upg in UM) {
      keyList.Add(upg.Key);
      upgradeStateList.Add(upg.Value);
    }
    currentworld = SettingsManager.world;
    FocusLevelTransform = SettingsManager.currentFocusLevelTransform;
    money = MoneyManager.money;
    endlessOriginalHS = SettingsManager.endlessOriginalHS;
    endlessUpgradedHS = SettingsManager.endlessUpgradedHS;
    currFortressSkin = SettingsManager.currFortressSkin;
    currBowSkin = SettingsManager.currBowSkin;
    currBulletSkin = SettingsManager.currBulletSkin;
    unlockedFortressSkin = SettingsManager.unlockedFortressSkin;
    unlockedBowSkin = SettingsManager.unlockedBowSkin;
    unlockedBulletSkin = SettingsManager.unlockedBulletSkin;
  }
}
