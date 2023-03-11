using System.Collections;
using UnityEngine;

public class ReloadBow : MonoBehaviour
{
  [SerializeField]
  GameObject BulletPrefab;
  bool wait = false;
  void Update()
  {
    if (BowManager.CurrentAmmo > 0 && findChildBullet() == null && wait == false) {
      wait = true;
      StartCoroutine(WaitReload());
    }
  }
  IEnumerator WaitReload() {
    BowManager.CurrentAmmo--;
    yield return new WaitForSeconds(BowManager.ReloadRate);
    GameObject bullet = Instantiate(BulletPrefab, new Vector3 (0f, -10f, 0f), Quaternion.identity);
    bullet.transform.SetParent(transform);
    bullet.transform.localPosition = new Vector3 (0f, 3.234f, 0f);
    wait = false;
  }
  Transform findChildBullet() {
    Transform childBullet = null;
    foreach(Transform tra in transform) {
      if (tra.CompareTag("Bullet")) {
         childBullet = tra;
         break;
      }
    }
    return childBullet;
  }
}
