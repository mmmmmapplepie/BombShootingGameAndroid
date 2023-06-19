using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoupladLife_Seeker : MonoBehaviour, IDamageable {
  //couplad specific fields
  [HideInInspector]
  public CoupladLife_Follower LinkFollower;
  [HideInInspector]
  public int deaths = 0;
  [HideInInspector]
  public bool[] halfdeath = { false, false };//seeker is 0th term, follower is the 1st term.
  public Coroutine reviveRoutine;
  [SerializeField]
  public float reviveTime;

  //basic enemy fields
  [SerializeField]
  public Enemy _data;
  public Enemy data {
    get {
      return _data;
    }
  }
  GameObject bombObject;
  [SerializeField]
  public GameObject chainExplosionEffect;
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
  public AudioManagerEnemy audioManager;

  void Awake() {
    CoupladStatsSettings();
    transform.Find("Enemy").gameObject.GetComponent<Collider2D>().enabled = false;
    transform.Find("MovementControl").gameObject.SetActive(false);
  }
  void Start() {
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
    if (FindObjectsOfType<CoupladLife_Follower>().Length > 0) {
      foreach (CoupladLife_Follower follower in FindObjectsOfType<CoupladLife_Follower>()) {
        if (follower.coupled == false) {
          LinkFollower = follower;
          LinkFollower.seekerScript = this;
          LinkFollower.coupled = true;
          LinkFollower.transform.Find("Enemy").gameObject.GetComponent<Collider2D>().enabled = true;
          LinkFollower.transform.Find("MovementControl").gameObject.SetActive(true);
          transform.Find("Enemy").gameObject.GetComponent<Collider2D>().enabled = true;
          transform.Find("MovementControl").gameObject.SetActive(true);
          return;
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
  public void stopRevive() {
    if (reviveRoutine == null) return;
    StopCoroutine(reviveRoutine);
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
          checkDamageCondition(damage / 50f);
        } else {
          audioManager.PlayAudio("ArmorHit");
          checkDamageCondition(damage - damage * ((float)Armordiff / 10f));
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
        coll.transform.root.gameObject.GetComponent<IDamageable>().takeDamage(BowManager.AOEDmg * damage);
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
  public void ShotDeath() {
    ChainExplosion script = gameObject.GetComponent<ChainExplosion>();
    if (script.Chained == true) {
      Instantiate(chainExplosionEffect, transform.position, Quaternion.identity);
      StartCoroutine("ChainExplodePreheat", script);
    }
    StartCoroutine("deathSequence");
  }
}
