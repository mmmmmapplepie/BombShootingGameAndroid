using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoupladLife : MonoBehaviour, IDamageable {
  public static GameObject instance;
  public static int deaths = 0;
  static bool halfdeath = false;
  public static bool fulldeath = false;

  [SerializeField]
  float reviveTime;
  [SerializeField]
  public Enemy data { get; set; }
  GameObject bombObject;
  [SerializeField]
  GameObject chainExplosionEffect;
  RectTransform rect;
  [HideInInspector]
  public float maxLife { get; set; }
  [HideInInspector]
  public float currentLife { get; set; }
  [HideInInspector]
  public int Armor { get; set; }
  [HideInInspector]
  public int MaxShield { get; set; }
  [HideInInspector]
  public int Shield { get; set; }
  [HideInInspector]
  bool Taunt;
  [HideInInspector]
  public bool dead { get; set; } = false;
  AudioManagerEnemy audioManager;

  void Awake() {
    //only 1 couplad at a time
    if (instance == null) {
      instance = gameObject;
    } else {
      Destroy(gameObject);
    }
    if (deaths != 0) {
      deaths = 0;
    }
    if (halfdeath != false) {
      halfdeath = false;
    }
    if (fulldeath != false) {
      fulldeath = false;
    }
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

  void Update() {
    checkDeathFinal();
  }

  void checkDeathFinal() {
    if (fulldeath) {
      StopCoroutine("revive");
      ShotDeath();
    }
  }

  void checkDamageCondition(float damage) {
    if (currentLife - damage < 0) {
      currentLife = 0;
      deaths += 1;
      StartCoroutine("revive");
    }
    if (halfdeath == true) {
      if (dead == true) {
        return;
      } else {
        dead = true;
        fulldeath = true;
      }
    }
  }

  IEnumerator revive() {
    gameObject.GetComponent<Collider2D>().enabled = false;
    float startTime = Time.time;
    //start revive animation
    while (currentLife < maxLife) {
      currentLife += currentLife + (maxLife * ((Time.time - startTime
      ) / reviveTime));
      yield return null;
    }
    currentLife = maxLife;
    yield return new WaitForSeconds(1f);
    //return to normal animation
    gameObject.GetComponent<Collider2D>().enabled = true;
  }

  public void takeTrueDamage(float damage) {
    audioManager.PlayAudio("NormalHit");
    checkDamageCondition(damage);
  }
  public void takeDamage(float damage) {
    if (Shield > 0) {
      audioManager.PlayAudio("ShieldHit");
      Shield--;
    } else {
      int Armordiff = Armor - BowManager.ArmorPierce;
      if (Armordiff > 0) {
        if (Armordiff > 9) {
          audioManager.PlayAudio("HeavyArmorHit");
          checkDamageCondition(damage / 50f); //2% damage only
        } else {
          audioManager.PlayAudio("ArmorHit");
          checkDamageCondition(damage - damage * ((float)Armordiff / 10f)); //each lvl diff takes a 10% decrease in dmg
        }
      } else {
        audioManager.PlayAudio("NormalHit");
        checkDamageCondition(damage);
      }
    }
  }
  public void AoeHit(float damage) {
    Collider2D[] Objects = Physics2D.OverlapCircleAll(transform.position, 1f);
    foreach (Collider2D coll in Objects) {
      if ((coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "TauntEnemy") && coll.gameObject != gameObject) {
        coll.transform.root.gameObject.GetComponent<EnemyLife>().takeDamage(BowManager.AOEDmg * damage);
      }
    }
  }
  public void ChainExplosion() {
    checkDamageCondition(BowManager.ChainExplosionDmg * BowManager.BulletDmg * BowManager.BulletMultiplier);
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
  void ShotDeath() {
    ChainExplosion script = gameObject.GetComponent<ChainExplosion>();
    if (script.Chained == true) {
      Instantiate(chainExplosionEffect, transform.position, Quaternion.identity);
      StartCoroutine("ChainExplodePreheat", script);
    }
    StartCoroutine("deathSequence");
  }
}
