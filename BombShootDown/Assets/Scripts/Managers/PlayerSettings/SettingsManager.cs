using UnityEngine;
using System.Collections.Generic;
public class SettingsManager {
  #region VolumeSettings
  public static float volumeTheme = 1f;
  public static float volumeEnemy = 1f;
  public static float volumeCannon = 1f;
  #endregion

  #region PlayerProgress
  // first value is the world 2nd is the stage of that world
  public static int[] world = new int[2] { 1, 1 };
  public static float endlessOriginalHS = 0f;
  public static float endlessUpgradedHS = 0f;
  public static float[] currentFocusLevelTransform = new float[2] { 443f, 682f };
  public static void clearStage(int _world, int lvl) {
    //world 1 settings 25 lvls
    if (_world == 1 && lvl < 25) {
      world[1] = lvl + 1;
    } else if (_world == 1 && lvl == 25) {
      world[0] = _world + 1;
      world[1] = 1;
    }
    //world 2 settings 30 lvls
    if (_world == 2 && lvl < 30) {
      world[1] = lvl + 1;
    } else if (_world == 2 && lvl == 30) {
      world[0] = _world + 1;
      world[1] = 1;
    }
    //world 3 settings 46 lvls
    if (_world == 3 && lvl < 46) {
      world[1] = lvl + 1;
    } else if (_world == 3 && lvl > 45) {
      world[1] = 47;
    }
  }
  #endregion
  #region skin
  public static string currFortressSkin = "Wooden Fortress";
  public static string currBowSkin = "Wooden Bow";
  public static string currBulletSkin = "Wooden Bullet";
  public static List<string> unlockedFortressSkin = new List<string>() { "Wooden Fortress" };
  public static List<string> unlockedBowSkin = new List<string>() { "Wooden Bow" };
  public static List<string> unlockedBulletSkin = new List<string>() { "Wooden Bullet" };
  #endregion
}
