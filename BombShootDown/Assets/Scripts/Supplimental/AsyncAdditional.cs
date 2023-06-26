using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class AsyncAdditional : MonoBehaviour {
  public static async Task Delay(float seconds, bool ignoreTimeScale = false) {
    float elapsed = 0f;
    while (elapsed < seconds) {
      elapsed += ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
      await Task.Yield();
    }
  }
}
