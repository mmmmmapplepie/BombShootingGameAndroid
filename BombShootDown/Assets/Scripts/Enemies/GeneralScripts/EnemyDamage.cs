using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
  [SerializeField]
  List<GameObject> damageEffects;
  Enemy data;
  float Damage;
  AudioManagerEnemy audioManager;
  void Awake()
  {
    audioManager = transform.Find("AudioManagerEnemy").GetComponent<AudioManagerEnemy>();
    data = gameObject.GetComponent<EnemyLife>().data;
    Damage = data.Damage;
  }
  void Update()
  {
    if (Time.timeScale == 0f || gameObject.GetComponent<EnemyLife>().dead)
    {
      return;
    }
    if (transform.position.y < -7.25f && GetComponent<EnemyLife>().currentLife > 0f)
    {
      LifeManager.CurrentLife -= Damage;
      if (Damage >= 50)
      {
        audioManager.PlayAudio("EnemyDamageTre");
        CreateEffect(damageEffects.Find(x => x.name == "EnemyDealDamageTremendous"), null, gameObject.transform.position);
      }
      else if (Damage >= 30)
      {
        audioManager.PlayAudio("EnemyDamageBig");
        CreateEffect(damageEffects.Find(x => x.name == "EnemyDealDamageBig"), null, gameObject.transform.position);
      }
      else if (Damage >= 10)
      {
        audioManager.PlayAudio("EnemyDamageMid");
        CreateEffect(damageEffects.Find(x => x.name == "EnemyDealDamageMedium"), null, gameObject.transform.position);
      }
      else
      {
        audioManager.PlayAudio("EnemyDamageSmall");
        CreateEffect(damageEffects.Find(x => x.name == "EnemyDealDamageSmall"), null, gameObject.transform.position);
      }
      StartCoroutine("deathSequence");
    }
  }
  IEnumerator deathSequence()
  {
    gameObject.GetComponent<EnemyLife>().dead = true;
    RemoveAtDeathComponents();
    SpriteRenderer sprite = transform.Find("Enemy").gameObject.GetComponent<SpriteRenderer>();
    for (int i = 0; i < 20; i++)
    {
      float angle = (Mathf.Sin(i)) / (i + 1);
      float ratio = 1f / (1f + i);
      transform.rotation = Quaternion.Euler(0, 0, angle);
      sprite.color = new Color(sprite.color.r / ratio, sprite.color.g / ratio, sprite.color.b / ratio, ratio);
      yield return new WaitForSeconds(0.05f);
    }
    Destroy(gameObject);
  }
  void RemoveAtDeathComponents()
  {
    gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    Destroy(transform.Find("Enemy").gameObject.GetComponent<Collider2D>());
    Destroy(transform.Find("MovementControl").gameObject);
    Destroy(transform.Find("State").gameObject);
  }
  void CreateEffect(GameObject prefab, Transform parent, Vector3 pos)
  {
    GameObject effect = Instantiate(prefab, pos, Quaternion.identity, parent);
  }
}
