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
    float shakeDuration = 1f;
    while (Time.unscaledTime < time + shakeDuration) {
      float newY = mag * (Mathf.Abs(Mathf.Sin(Mathf.PI * ((Time.unscaledTime - time) / shakeDuration) * 6f))) * 2f * ((shakeDuration + time - Time.unscaledTime));
      transform.position = new Vector3(0f, newY, -100f);
      yield return null;
    }
    transform.position = new Vector3(0f, 0f, -100f);
    shaking = false;
    currMag = 0f;
  }
}
