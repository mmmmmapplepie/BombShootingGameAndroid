using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
  float currMag = 0f;
  bool shaking = false;
  public void cameraShake(float Damage) {
    float shakeMag = Mathf.Min(Damage / 100f, 2f);
    if (shaking == true && shakeMag < currMag) {
      return;
    } else if (shaking == true) {
      StopCoroutine("cameraShakeRoutine");
    }
    shaking = true;
    StartCoroutine(cameraShakeRoutine(shakeMag));
  }
  IEnumerator cameraShakeRoutine(float mag) {
    float time = Time.unscaledTime;
    while (Time.unscaledTime < time + 0.5f) {
      float newY = mag * (Mathf.Abs(Mathf.Sin(Mathf.PI * ((Time.unscaledTime - time) * 2f) * 6f))) * 2f * ((0.5f + time - Time.unscaledTime));
      transform.position = new Vector3(0f, newY, -100f);
      yield return null;
    }
    transform.position = new Vector3(0f, 0f, -100f);
    shaking = false;
    currMag = 0f;
  }
}
