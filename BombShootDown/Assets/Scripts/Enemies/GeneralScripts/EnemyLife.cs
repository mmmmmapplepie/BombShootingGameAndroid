using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour, IDamageable {
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
  public void takeTrueDamage(float damage) {
    audioManager.PlayAudio("NormalHit");
    currentLife -= damage;
    if (currentLife <= 0f && !dead) {
      ShotDeath();
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
      ShotDeath();
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
  void DepthColorChange(Transform tra, float AlphaRatio) {
    SpriteRenderer renderer = tra.gameObject.GetComponent<SpriteRenderer>();
    if (renderer != null) {
      renderer.color = new Color(renderer.color.r / AlphaRatio, renderer.color.g / AlphaRatio, renderer.color.b / AlphaRatio, AlphaRatio); ;
    }
    if (tra.childCount > 0) {
      foreach (Transform t in tra) {
        DepthColorChange(t, AlphaRatio);
      }
    }
  }
  IEnumerator deathSequence() {
    dead = true;
    RemoveAtDeathComponents();
    Transform enemySpriteBase = transform.Find("Enemy");
    for (int i = 0; i < 20; i++) {
      float ratio = 1f / (1f + i);
      DepthColorChange(enemySpriteBase, ratio);
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
