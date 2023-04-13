using System.Collections;
using UnityEngine;

public class ReloadBow : MonoBehaviour {
  [SerializeField]
  GameObject BulletPrefab;
  bool wait = false;
  void Update() {
    if (BowManager.CurrentAmmo > 0 && findChildBullet() == null && wait == false) {
      wait = true;
      StartCoroutine(ReloadAndWait());
    }
  }
  IEnumerator ReloadAndWait() {
    BowManager.CurrentAmmo--;
    float waitTime = BowManager.ReloadRate * BowManager.CoolDownRate;
    float startTime = Time.time;
    while (Time.time - startTime <= waitTime) {
      yield return null;
    }
    GameObject bullet = Instantiate(BulletPrefab, new Vector3(0f, -10f, 0f), transform.rotation, transform);
    bullet.transform.localPosition = new Vector3(0f, 3.234f, 0f);
    wait = false;
  }
  Transform findChildBullet() {
    Transform childBullet = null;
    foreach (Transform tra in transform) {
      if (tra.CompareTag("Bullet")) {
        childBullet = tra;
        break;
      }
    }
    return childBullet;
  }
}
