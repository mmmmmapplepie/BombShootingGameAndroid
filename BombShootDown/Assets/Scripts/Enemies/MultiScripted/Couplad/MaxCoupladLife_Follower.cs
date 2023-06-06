using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxCoupladLife_Follower : CoupladLife_Follower {
  [HideInInspector]
  public new MaxCoupladLife_Seeker seekerScript;
  //basic enemy fields
  GameObject bombObject;
  [HideInInspector]
  bool Taunt;
  void Awake() {
    CoupladStatsSettings();
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

  void checkDamageCondition(float damage) {
    if (currentLife - damage <= 0 && !seekerScript.halfdeath[1]) {
      currentLife = 0;
      seekerScript.deaths += 1;
      seekerScript.halfdeath[1] = true;
      reviveRoutine = StartCoroutine("revive");
    } else {
      currentLife -= damage;
    }
    if (seekerScript.halfdeath[0] && seekerScript.halfdeath[1]) {
      ShotDeath();
      seekerScript.ShotDeath();
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
    seekerScript.halfdeath[1] = false;
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
