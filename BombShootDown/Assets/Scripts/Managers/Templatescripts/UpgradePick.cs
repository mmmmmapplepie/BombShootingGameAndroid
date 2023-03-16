using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeIcon", menuName = "UpgIcon")]
public class UpgradePick : ScriptableObject
{
  public new string name;
  public Sprite sprite;
  public int upgradeSlots;
}
