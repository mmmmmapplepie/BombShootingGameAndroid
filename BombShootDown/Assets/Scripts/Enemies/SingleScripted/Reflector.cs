using System.Collections;
using UnityEngine;
public class Reflector : MonoBehaviour, IdestroyFunction {
  [SerializeField] float reflectRatio;
  Enemy data;
  EnemyLife currLife;
  float health;
  float speed;
  float driftMag;
  float drift;
  void Awake() {
    currLife = transform.root.GetComponent<EnemyLife>();
    data = currLife.data;
    speed = data.Speed;
    newDrift();
    health = data.Life;
    StartCoroutine(driftChanger());
  }
  void Update() {
    reflectDamage();
  }
  IEnumerator driftChanger() {
    while (true) {
      if (Random.Range(0f, 5f) < 1f) {
        newDrift();
      }
      yield return new WaitForSeconds(1f);
    }
  }
  void newDrift() {
    drift = Random.Range(-1.5f, 1.5f);
    driftMag = Mathf.Abs(drift);
  }
  void reflectDamage() {
    if (health != currLife.currentLife) {
      float diff;
      if (currLife.currentLife < 0f) {
        diff = health;
      } else {
        diff = health - currLife.currentLife;

      }
      if (LifeManager.ReviveRoutine == false) {
        LifeManager.CurrentLife -= diff * reflectRatio * BowManager.EnemyDamage;
      }
      health = currLife.currentLife;
    }
  }
  void checkFlip() {
    if (transform.position.x < -5f && drift < 0f) {
      drift = driftMag;
    }
    if (transform.position.x > 5f && drift > 0f) {
      drift = -driftMag;
    }
  }
  void FixedUpdate() {
    checkFlip();
    Vector3 old = transform.root.position;
    Vector3 normDir = new Vector3(drift, -speed, 0f);
    Vector3 newPos = old + Time.deltaTime * BowManager.EnemySpeed * normDir;
    transform.root.position = newPos;
  }
  public void DestroyFunction() {
    if (LifeManager.ReviveRoutine == false) {
      LifeManager.CurrentLife -= health * reflectRatio * BowManager.EnemyDamage;
    }
  }
}
