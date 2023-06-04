using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoupladDamage_Follower : MonoBehaviour {
  [HideInInspector]
  [SerializeField]
  CoupladLife_Seeker lifeScript;
  [SerializeField]
  float deathDamageMultiplier = 0.4f;
  [SerializeField]
  List<GameObject> damageEffects;
  Enemy data;
  [HideInInspector]
  public float Damage;
  AudioManagerEnemy audioManager;
  void Start() {
    audioManager = transform.Find("AudioManagerEnemy").GetComponent<AudioManagerEnemy>();
    data = lifeScript.data;
    Damage = data.Damage;
  }
  void Update() {
    //need to make sure other one also dies if 1 dies.
    if (Time.timeScale == 0f || lifeScript.dead) {
      return;
    }
    if (transform.position.y < -7.25f && GetComponent<CoupladLife_Seeker>().currentLife > 0f) {
      if (data.Boss == 0 && LifeManager.ReviveRoutine == true) {
        DamageEffect();
        StartCoroutine("deathSequence");
        return;
      }
      LifeManager.CurrentLife -= Damage * BowManager.EnemyDamage;
      DamageEffect();
      StartCoroutine("deathSequence");
    }
  }
  void DamageEffect() {
    float dmg = lifeScript.deaths * deathDamageMultiplier * Damage * BowManager.EnemyDamage;
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
  IEnumerator deathSequence() {
    lifeScript.dead = true;
    lifeScript.fulldeath = true;
    lifeScript.deathProcess = true;
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
