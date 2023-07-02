using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaximaLife : MonoBehaviour, IDamageable {
  [SerializeField]
  Enemy _data;
  public Enemy data {
    get {
      return _data;
    }
  }
  GameObject bombObject;
  [SerializeField]
  GameObject chainExplosionEffect;
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

  enum phase { phase1, phase2 };
  phase phaseChecker = phase.phase1;
  phase currentPhase = phase.phase1;
  [SerializeField] MaximaPhase1 phase1Script;
  [SerializeField] MaximaPhase2 phase2Script;
  [SerializeField] Slider bigDamageTimerSlider;
  [SerializeField] float BigDamageTimer = 50f;
  float phaseChangeTime;
  bool changingPhase = false;
  void Awake() {
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
    if (!dead && !changingPhase) {
      checkPhaseChangeDamage();
    }
  }
  void Start() {
    phase1Script.enabled = true;
    phaseChangeTime = Time.time;
  }
  void checkPhaseChangeDamage() {
    float timeElapsed = Time.time - phaseChangeTime;
    if (currentPhase != phaseChecker) {
      phaseChecker = currentPhase;
    } else {
      if (timeElapsed > BigDamageTimer) {
        bigDamageTimerSlider.value = 1f;
        phaseChangeTime = Time.time;
        LifeManager.CurrentLife -= 200f;
      } else {
        bigDamageTimerSlider.value = (BigDamageTimer - timeElapsed) / BigDamageTimer;
      }
    }
  }
  void checkPhase() {
    if (changingPhase) return;
    if (currentPhase == phase.phase1) {
      phase1Script.enabled = false;
      changingPhase = true;
      currentPhase = phase.phase2;
      StartCoroutine(revive());
    } else {
      phase2Script.enabled = false;
      ShotDeath();
    }
  }
  IEnumerator revive() {
    transform.Find("Enemy").gameObject.GetComponent<Collider2D>().enabled = false;
    float startTime = Time.time;
    transform.Find("MovementControl").gameObject.SetActive(false);
    //start revive animation
    while (currentLife < maxLife) {
      currentLife = (maxLife * ((Time.time - startTime
      ) / 2f));
      yield return null;
    }
    transform.Find("Enemy").gameObject.GetComponent<Collider2D>().enabled = true;
    phaseChangeTime = Time.time;
    currentLife = maxLife;
    phase2Script.enabled = true;
    changingPhase = false;
    transform.Find("MovementControl").gameObject.SetActive(true);
  }
  public void takeTrueDamage(float damage) {
    audioManager.PlayAudio("NormalHit");
    currentLife -= damage;
    if (currentLife <= 0f && !dead) {
      checkPhase();
    }
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
          currentLife -= damage / 50f; //2% damage only
        } else {
          audioManager.PlayAudio("ArmorHit");
          currentLife -= damage - damage * ((float)Armordiff / 10f); //each lvl diff takes a 10% decrease in dmg
        }
      } else {
        audioManager.PlayAudio("NormalHit");
        currentLife -= damage;
      }
    }
    if (currentLife <= 0f && !dead) {
      checkPhase();
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
    takeDamage(BowManager.ChainExplosionDmg * BowManager.BulletDmg * BowManager.BulletMultiplier);
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
