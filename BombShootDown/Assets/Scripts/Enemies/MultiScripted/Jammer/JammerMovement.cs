using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JammerMovement : MonoBehaviour {
  [SerializeField] Transform aniO, aniM, aniI;
  Enemy data;
  float speed;
  float initialDistance;
  float startyPos;
  bool startWait = false;
  bool doneWait = false;
  float waitTime;
  float driftMag;
  float drift;
  float IniO, IniM, IniI, magO, magM, magI, dirO, dirM, dirI;
  void Awake() {
    data = transform.root.gameObject.GetComponent<EnemyLife>().data;
    speed = data.Speed;
    initialDistance = Random.Range(1f, 4f);
    waitTime = Random.Range(35f, 45f);
    startyPos = transform.root.position.y;
    drift = Random.Range(-0.5f, 0.5f);
    driftMag = Mathf.Abs(drift);
    havocAniIniSet();
  }
  void havocAniIniSet() {
    //initial scales are 4,3,2 respectively for Outer Middle Inner
    IniO = Random.Range(0f, 360f);
    IniM = Random.Range(0f, 360f);
    IniI = Random.Range(0f, 360f);
    dirO = Random.Range(-1f, 1f);
    dirM = Random.Range(-1f, 1f);
    dirI = Random.Range(-1f, 1f);
    magO = Random.Range(-2f, 2f);
    magM = Random.Range(-0.1f, 0.1f);
    magI = Random.Range(-0.1f, 0.1f);
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
    driftSide();
    havocAnimate();
  }
  IEnumerator Wait() {
    yield return new WaitForSeconds(waitTime);
    doneWait = true;
  }
  void checkFlip() {
    if (transform.position.x < -5f && drift < 0f) {
      drift = driftMag;
    }
    if (transform.position.x > 5f && drift > 0f) {
      drift = -driftMag;
    }
  }
  void driftSide() {
    checkFlip();
    Vector3 old = transform.root.position;
    Vector3 Dir = new Vector3(drift, 0f, 0f);
    Vector3 newPos = old + Time.deltaTime * speed * BowManager.EnemySpeed * Dir;
    transform.root.position = newPos;
  }
  void havocAnimate() {
    float frequency = magO == 0 ? 0.001f : magO;
    float xscale = 4f * (magO * Mathf.Sin(Time.time / frequency) + 1f);
    float yscale = 4f * (magO * -Mathf.Sin(Time.time / frequency) + 1f);
    aniO.localScale = new Vector3(xscale, yscale, 1f);
    frequency = magM == 0 ? 0.001f : magM;
    xscale = 3f * (magM * Mathf.Sin(Time.time / frequency) + 1f);
    yscale = 3f * (magM * -Mathf.Sin(Time.time / frequency) + 1f);
    aniM.localScale = new Vector3(xscale, yscale, 1f);
    frequency = magI == 0 ? 0.001f : magI;
    xscale = 2f * (magI * Mathf.Sin(Time.time / frequency) + 1f);
    yscale = 2f * (magI * -Mathf.Sin(Time.time / frequency) + 1f);
    aniI.localScale = new Vector3(xscale, yscale, 1f);
    aniO.rotation = Quaternion.Euler(0f, 0f, IniO + 20 * dirO * Time.time);
    aniM.rotation = Quaternion.Euler(0f, 0f, IniM + 30 * dirM * Time.time);
    aniI.rotation = Quaternion.Euler(0f, 0f, IniI + 40 * dirI * Time.time);
  }
}

