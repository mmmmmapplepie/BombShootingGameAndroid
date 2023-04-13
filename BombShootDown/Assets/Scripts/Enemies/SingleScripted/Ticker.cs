using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticker : MonoBehaviour {
  [SerializeField] float addedWaitTimeMin, addedWaitTimeMax;
  Enemy data;
  Animator animator;
  float speed;
  float initialDistance;
  float initialWait;
  float startyPos;
  bool startWait = false;
  bool doneWait = false;
  void Awake() {
    data = transform.root.GetComponent<EnemyLife>().data;
    speed = data.Speed;
    initialDistance = Random.Range(2f, 3f);
    initialWait = Random.Range(0f, 5f);
    startyPos = transform.root.position.y;
    animator = transform.root.Find("Enemy").gameObject.GetComponent<Animator>();
  }
  void Update() {
    animator.speed = BowManager.EnemySpeed;
    if (transform.root.position.y > startyPos - initialDistance) {
      transform.root.position -= new Vector3(0f, speed * BowManager.EnemySpeed * Time.deltaTime, 0f);
    }
    if (transform.root.position.y < startyPos - initialDistance && startWait == false) {
      startWait = true;
      StartCoroutine("TransformPhase");
    }
  }
  void FixedUpdate() {
    if (doneWait == true) {
      Rigidbody2D rb = transform.root.gameObject.GetComponent<Rigidbody2D>();
      rb.drag = 0f;
      rb.AddForce(new Vector2(0f, -2 * speed * BowManager.EnemySpeed * rb.mass), ForceMode2D.Force);
    }
  }
  IEnumerator TransformPhase() {
    float addedWait = Random.Range(addedWaitTimeMin, addedWaitTimeMax);
    yield return new WaitForSeconds(addedWait);
    animator.Play("Ticker");
    yield return null;
    while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) {
      yield return null;
    }

    doneWait = true;
  }
}
