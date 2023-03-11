using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesEquipped
{
  public static List<string> EquippedUpgrades = new List<string>();
  public static List<string> tempUpgHolder = new List<string>();
  public static int UpgradedSlots = 5; // set by upgrades
  public static int LevelSlots = 5; // value set by level script (depends on lvl)
  public static int AvailableSlots = 5; //starts as the smaller of the above 2 slot variables.
}
