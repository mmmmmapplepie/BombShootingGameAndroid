using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
  public List<string> keyList = new List<string>();
  public List<int[]> upgradeStateList = new List<int[]>();
  public int[] currentworld;
  public float money;
  public float[] FocusLevelTransform;

  public PlayerData () {
    Dictionary<string, int[]> UM = UpgradesManager.UpgradeOptions;
    foreach (KeyValuePair<string, int[]> upg in UM) {
      keyList.Add(upg.Key);
      upgradeStateList.Add(upg.Value);
    }
    currentworld = SettingsManager.world;
    FocusLevelTransform = SettingsManager.currentFocusLevelTransform;
    money = MoneyManager.money;
  }
}
