using System.Collections;
using UnityEngine;

public class CoupladLife_Follower : MonoBehaviour, IDamageable {
  [HideInInspector]
  public bool coupled = false;
  [HideInInspector]
  public CoupladLife_Seeker seekerScript;

  [SerializeField]
  public float reviveTime;
  public Coroutine reviveRoutine;

  [SerializeField] Animator animator;

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
  public void stopRevive() {
    if (reviveRoutine == null) return;
    StopCoroutine(reviveRoutine);
  }
  IEnumerator revive() {
    transform.Find("Enemy").gameObject.GetComponent<Collider2D>().enabled = false;
    transform.Find("MovementControl").gameObject.SetActive(false);
    float startTime = Time.time;
    animator.speed = 0.1f;
    while (currentLife < maxLife) {
      currentLife = (maxLife * ((Time.time - startTime
      ) / reviveTime));
      yield return null;
    }
    currentLife = maxLife;
    seekerScript.halfdeath[1] = false;
    animator.speed = 1f;
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
