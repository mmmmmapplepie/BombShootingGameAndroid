using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyDamage : MonoBehaviour, IEnemyDealsDamage {
  [SerializeField]
  List<GameObject> damageEffects;
  Enemy data;
  [HideInInspector]
  public float Damage { get; set; }
  AudioManagerEnemy audioManager;
  void Awake() {
    audioManager = transform.Find("AudioManagerEnemy").GetComponent<AudioManagerEnemy>();
    data = gameObject.GetComponent<IDamageable>().data;
    Damage = data.Damage;
  }
  void Update() {
    if (Time.timeScale == 0f || gameObject.GetComponent<IDamageable>().dead) {
      return;
    }
    if (transform.position.y < -7.25f && GetComponent<IDamageable>().currentLife > 0f) {
      if (data.Boss == 0 && LifeManager.ReviveRoutine == true) {
        DamageEffect();
        StartCoroutine("deathSequence");
        return;
      }
      DamageEffect();
      StartCoroutine("deathSequence");
    }
  }
  void DamageEffect() {
    float dmg = Damage * BowManager.EnemyDamage;
    LifeManager.CurrentLife -= dmg;
    Camera.main.gameObject.GetComponent<CameraShake>().cameraShake(dmg);
    if (dmg >= 100) {
      audioManager.PlayAudio("EnemyDamageTre");
      CreateEffect(damageEffects.Find(x => x.name == "EnemyDealDamageTremendous"), null, gameObject.transform.position);
    } else if (dmg >= 50) {
      audioManager.PlayAudio("EnemyDamageBig");
      CreateEffect(damageEffects.Find(x => x.name == "EnemyDealDamageBig"), null, gameObject.transform.position);
    } else if (dmg >= 15) {
      audioManager.PlayAudio("EnemyDamageMid");
      CreateEffect(damageEffects.Find(x => x.name == "EnemyDealDamageMedium"), null, gameObject.transform.position);
    } else {
      audioManager.PlayAudio("EnemyDamageSmall");
      CreateEffect(damageEffects.Find(x => x.name == "EnemyDealDamageSmall"), null, gameObject.transform.position);
    }
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
    gameObject.GetComponent<IDamageable>().dead = true;
    RemoveAtDeathComponents();
    Transform enemySpriteBase = transform.Find("Enemy");
    for (int i = 0; i < 20; i++) {
      float ratio = 1f / (1f + i);
      DepthColorChange(enemySpriteBase, ratio);
      yield return new WaitForSeconds(0.05f);
    }
    Destroy(gameObject);
  }
  void RemoveAtDeathComponents() {
    gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    Destroy(transform.Find("Enemy").gameObject.GetComponent<Collider2D>());
    Destroy(transform.Find("MovementControl").gameObject);
    Destroy(transform.Find("State").gameObject);
  }
  void CreateEffect(GameObject prefab, Transform parent, Vector3 pos) {
    GameObject effect = Instantiate(prefab, pos, Quaternion.identity, parent);
  }
}
