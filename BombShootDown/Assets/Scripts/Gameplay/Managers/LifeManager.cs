using UnityEngine;

public class LifeManager {
  // public static float CurrentLife = 10f;
  static float _CurrentLife = 10f;
  public static float CurrentLife {
    get { return _CurrentLife; }
    set {
      if (value < 0f) {
        _CurrentLife = 0f;
      } else {
        _CurrentLife = value;
      }
    }
  }
  public static bool ReviveUsed = false;
  public static bool Alive = true;
  public static bool ReviveRoutine = false;
}
