using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour {
  Enemy data;
  [SerializeField] float originalInterval, range, minrange;
  [SerializeField] new BoxCollider2D collider;
  Transform main;
  float interval;
  float speed;
  float timer;
  bool teleporting = false;
  void Awake() {
    data = transform.root.GetComponent<EnemyLife>().data;
    speed = data.Speed;
    timer = Time.time;
    main = transform.root;
    interval = originalInterval;
  }
  void Update() {
    if ((Time.time > timer + interval) && !teleporting) {
      teleporting = true;
      StartCoroutine(teleport());
    }
  }
  IEnumerator teleport() {
    float movetimer = Time.time;
    float teleTime = 0.1f;
    while (Time.time < movetimer + teleTime) {
      float ratio = ((movetimer + teleTime - Time.time) / teleTime);
      main.localScale = new Vector3(ratio, ratio, 1f);
      yield return null;
    }
    collider.enabled = false;
    main.localScale = Vector3.zero;
    main.position = getNewPos();
    movetimer = Time.time;
    yield return new WaitForSeconds(0.5f);
    collider.enabled = true;
    while (Time.time < movetimer + teleTime) {
      float ratio = ((Time.time - movetimer) / teleTime);
      main.localScale = new Vector3(ratio, ratio, 1f);
      yield return null;
    }
    main.localScale = Vector3.one;
    timer = Time.time;
    teleporting = false;
    interval = originalInterval / BowManager.EnemySpeed;
  }
  Vector3 getNewPos() {
    float mag = Random.Range(minrange, range);
    float dir = Random.Range(-1f, 1f);
    float xpos = main.position.x;
    if (dir < 0) {
      mag = ((xpos - mag) < -5.25f) ? (10.5f + xpos - mag) : xpos - mag;
    } else {
      mag = ((xpos + mag) > 5.25f) ? (-10.5f + xpos + mag) : xpos + mag;
    }
    float yPos = main.root.position.y;
    float ymov = Random.Range(0.9f, 1.1f);
    Vector3 pos3 = new Vector3(mag, (yPos - ymov), 0f);
    return pos3;
  }
}
