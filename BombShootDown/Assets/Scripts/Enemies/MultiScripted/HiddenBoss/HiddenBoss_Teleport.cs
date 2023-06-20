using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBoss_Teleport : MonoBehaviour {
  [SerializeField] HiddenBossLife lifeScript;
  void OnEnable() {
    StartCoroutine(TeleportRoutine());
  }
  IEnumerator TeleportRoutine() {
    float waitTime = 2f;
    float starttime = Time.time;
    while (true) {
      if (waitTime > 0.2f) {
        waitTime -= 0.0676f;
      }
      Teleport();
      if (waitTime < Time.deltaTime) {
        yield return null;
      } else {
        yield return new WaitForSeconds(waitTime);
      }
    }
  }
  void Teleport() {
    float xPos = Random.Range(-5f, 5f);
    float yPos = Random.Range(-3f, 10f);
    transform.root.position = new Vector3(xPos, yPos, 0f);
  }
  void OnDisable() {
    StopAllCoroutines();
  }
}
