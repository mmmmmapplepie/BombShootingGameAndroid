using UnityEngine;
public class MoneyManager
{

  public static float money = 100000; //cant be larger than 10000000000000000000;
  public static void useMoney(float val) {
    if (money > val) {
      money = Mathf.Floor(money - val);
    }
    SaveSystem.saveSettings();
  }
  public static void addMoney(float val) {
    if (money + val < 10000000000000000000f) {
      money = Mathf.Floor(money + val);
    } else {
      money = 10000000000000000000;
    }
    SaveSystem.saveSettings();
  }
}
