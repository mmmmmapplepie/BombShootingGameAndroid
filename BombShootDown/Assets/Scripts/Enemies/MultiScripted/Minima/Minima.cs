using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minima : MonoBehaviour {
  [SerializeField] EnemyLife lifeScript;
  [SerializeField] GameObject EnemyImage, StateBar;
  float BoostMultiplier = 1f;
  bool Emergency = false;
  bool invisible = false;

  void Update() {
    if (lifeScript.dead) {
      StopAllCoroutines();
      return;
    }
    checkEmergency();
  }

  void Awake() {
    BoostSpeed();
  }
  void checkEmergency() {
    Emergency = lifeScript.currentLife / lifeScript.maxLife > 0.5f ? false : true;
    BoostSpeed();
    if (Emergency) checkNewInvisibilityRoutine();
  }
  void checkNewInvisibilityRoutine() {
    if (!invisible) StartCoroutine(goInvisible());
  }
  IEnumerator goInvisible() {
    invisible = true;
    StateBar.GetComponent<Canvas>().enabled = false;
    if (transform.Find("State").Find("Life").Find("Background").childCount > 0) transform.Find("State").Find("Life").Find("Background").GetChild(0).gameObject.SetActive(false);
    float startTime = Time.time;
    while (Time.time - startTime < 1f) {
      ChangeVisibility(EnemyImage, 1f - Time.time + startTime);
      yield return null;
    }
    ChangeVisibility(EnemyImage, 0f);
    yield return new WaitForSeconds(3f);
    while (Time.time - startTime < 0.5f) {
      ChangeVisibility(EnemyImage, Time.time - startTime);
      yield return null;
    }
    StateBar.GetComponent<Canvas>().enabled = true;
    if (transform.Find("State").Find("Life").Find("Background").childCount > 0) transform.Find("State").Find("Life").Find("Background").GetChild(0).gameObject.SetActive(true);
    ChangeVisibility(EnemyImage, 1f);
    yield return new WaitForSeconds(2f);
    invisible = false;
  }
  void ChangeVisibility(GameObject imageObject, float opacity) {
    Transform parent = imageObject.transform;
    Color originalColor = imageObject.GetComponent<SpriteRenderer>().color;
    imageObject.GetComponent<SpriteRenderer>().color = new Color(originalColor.r, originalColor.g, originalColor.b, opacity);
    if (parent.childCount != 0) {
      foreach (Transform child in parent) {
        ChangeVisibility(child.gameObject, opacity);
      }
    }
  }
  void BoostSpeed() {
    BowManager.EnemySpeed /= BoostMultiplier;
    if (!Emergency) {
      BoostMultiplier = 1.5f;
      BowManager.EnemySpeed *= BoostMultiplier;
    } else {
      BoostMultiplier = 2f;
      BowManager.EnemySpeed *= BoostMultiplier;
    }
  }
  void OnDestroy() {
    BowManager.EnemySpeed /= BoostMultiplier;
  }
}
