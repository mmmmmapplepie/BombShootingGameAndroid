using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxCoupladLife_Seeker : CoupladLife_Seeker {
  [HideInInspector] new MaxCoupladLife_Follower LinkFollower;

  //basic enemy fields
  GameObject bombObject;
  [HideInInspector]
  bool Taunt;
  [HideInInspector]

  void Awake() {
    CoupladStatsSettings();
    if (LinkFollower == null) {
      SearchFollower();
    }
  }
  void CoupladStatsSettings() {
    bombObject = transform.Find("Enemy").gameObject;
    audioManager = transform.Find("AudioManagerEnemy").GetComponent<AudioManagerEnemy>();
    maxLife = data.Life;
    currentLife = data.Life;
    Armor = data.Armor;
    Shield = data.Shield;
    MaxShield = data.MaxShield;
    Taunt = data.Taunt;
    if (Taunt) {
      bombObject.tag = "TauntEnemy";
    } else {
      bombObject.tag = "Enemy";
    }
  }

  void Update() {
    if (LinkFollower == null) {
      SearchFollower();
    }
  }

  void SearchFollower() {
    if (FindObjectsOfType<MaxCoupladLife_Follower>().Length > 0) {
      foreach (MaxCoupladLife_Follower follower in FindObjectsOfType<MaxCoupladLife_Follower>()) {
        if (follower.coupled == false) {
          LinkFollower = FindObjectOfType<MaxCoupladLife_Follower>();
          LinkFollower.seekerScript = this;
          LinkFollower.coupled = true;
        }
      }
    }
  }

  void checkDamageCondition(float damage) {
    if (currentLife - damage <= 0 && !halfdeath[0]) {
      currentLife = 0;
      deaths += 1;
      halfdeath[0] = true;
      reviveRoutine = StartCoroutine("revive");
    } else {
      currentLife -= damage;
    }
    if (halfdeath[0] && halfdeath[1]) {
      ShotDeath();
      LinkFollower.ShotDeath();
    }
  }

  IEnumerator revive() {
    transform.Find("Enemy").gameObject.GetComponent<Collider2D>().enabled = false;
    transform.Find("MovementControl").gameObject.SetActive(false);
    float startTime = Time.time;
    //start revive animation
    while (currentLife < maxLife) {
      currentLife = (maxLife * ((Time.time - startTime
      ) / reviveTime));
      yield return null;
    }
    currentLife = maxLife;
    halfdeath[0] = false;
    //return to normal animation
    //normal animation
    transform.Find("Enemy").gameObject.GetComponent<Collider2D>().enabled = true;
    transform.Find("MovementControl").gameObject.SetActive(true);
  }
  void RemoveAtDeathComponents() {
    gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    Destroy(transform.Find("Enemy").gameObject.GetComponent<Collider2D>());
    EnemyFunctionalityDestroy();
    Destroy(transform.Find("State").gameObject);
  }
  void EnemyFunctionalityDestroy() {
    GameObject funct = transform.Find("MovementControl").gameObject;
    if (funct.GetComponent<IdestroyFunction>() != null) {
      funct.GetComponent<IdestroyFunction>().DestroyFunction();
    }
    Destroy(funct);
  }
  IEnumerator deathSequence() {
    dead = true;
    stopRevive();
    RemoveAtDeathComponents();
    SpriteRenderer sprite = transform.Find("Enemy").gameObject.GetComponent<SpriteRenderer>();
    for (int i = 0; i < 20; i++) {
      float ratio = 1f / (1f + i);
      sprite.color = new Color(sprite.color.r / ratio, sprite.color.g / ratio, sprite.color.b / ratio, ratio);
      foreach (Transform tra in transform.Find("Enemy")) {
        if (tra.GetComponent<SpriteRenderer>() != null) {
          SpriteRenderer spra = tra.gameObject.GetComponent<SpriteRenderer>();
          spra.color = new Color(spra.color.r / ratio, spra.color.g / ratio, spra.color.b / ratio, ratio);
        }
      }
      yield return new WaitForSeconds(0.05f);
    }
    Destroy(gameObject);
  }
  IEnumerator ChainExplodePreheat(ChainExplosion script) {
    yield return new WaitForSeconds(0.2f);
    script.Explode();
  }
}
