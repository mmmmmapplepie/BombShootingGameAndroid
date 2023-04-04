using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outlier : MonoBehaviour {
  Enemy data;
  float speed;
  float DOTdamage;
  float initialDistance;
  float startyPos;
  bool startWait = false;
  bool doneWait = false;
  float waitTime;
  void Awake() {
    data = transform.root.GetComponent<EnemyLife>().data;
    speed = data.Speed;
    DOTdamage = data.Damage / 10f;
    initialDistance = Random.Range(1f, 4f);
    waitTime = Random.Range(40f, 60f);
    startyPos = transform.root.position.y;
  }
  void Update() {
    if (transform.root.position.y > startyPos - initialDistance) {
      transform.root.position -= new Vector3(0f, speed * BowManager.EnemySpeed * Time.deltaTime, 0f);
    }
    if (transform.root.position.y < startyPos - initialDistance && startWait == false) {
      startWait = true;
      StartCoroutine("Wait");
    }
    if (doneWait == true) {
      transform.root.position -= new Vector3(0f, speed * BowManager.EnemySpeed * Time.deltaTime, 0f);
    }
    dealDotDmg();
  }
  IEnumerator Wait() {
    yield return new WaitForSeconds(waitTime);
    doneWait = true;
  }
  void dealDotDmg() {
    if (LifeManager.ReviveRoutine == false) {
      LifeManager.CurrentLife -= DOTdamage * BowManager.EnemyDamage * Time.deltaTime;
    }
  }

}
