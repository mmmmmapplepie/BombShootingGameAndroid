using UnityEngine;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
  [SerializeField]
  List<GameObject> Effects;
  [SerializeField]
  GameObject PullEffect;
  int hits;
  float damage;
  bool aoe;
  bool chain;
  float pull = 0;
  float pulltime = 0.4f;
  bool pullStarted = false;
  int pierce;
  float speed;
  void Awake()
  {
    gameObject.GetComponent<CircleCollider2D>().enabled = false;
  }
  void Update()
  {
    // destroy when outside area
    if (transform.position.x > 7f || transform.position.x < -7f || transform.position.y > 13f || transform.position.y < -13f)
    {
      Destroy(gameObject);
    }
    if (pull > 0f && pullStarted == false && gameObject.GetComponent<CircleCollider2D>().enabled == true)
    {
      pullStarted = true;
      pulltime = 0.4f - 0.02f * pull;
      InvokeRepeating("PullEnemies", 0f, pulltime);
    }
  }
  void PullEnemies()
  {
    Collider2D[] Enemies = Physics2D.OverlapCircleAll(transform.position, 2.5f);
    CreateEffect(PullEffect, null, transform.position);
    foreach (Collider2D coll in Enemies)
    {
      if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "TauntEnemy")
      {
        if (coll.gameObject == gameObject)
        {
          continue;
        }
        //pull animation
        float force = (coll.transform.parent.position.x - transform.position.x);
        Rigidbody2D rb = coll.transform.parent.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-force * pull, 0f), ForceMode2D.Impulse);
      }
    }
  }
  public void Shoot(float angle, float ratio)
  {
    float x = 0;
    float y = 1;
    if (angle >= 0f && angle < 90f)
    {
      float input = angle * Mathf.PI / 180;
      x = -Mathf.Sin(input);
      y = Mathf.Cos(input);
    }
    else if (angle >= 90f && angle < 180f)
    {
      float a = angle - 90f;
      float input = a * Mathf.PI / 180;
      x = -Mathf.Cos(input);
      y = -Mathf.Sin(input);
    }
    else if (angle >= 180f && angle < 270f)
    {
      float a = angle - 180f;
      float input = a * Mathf.PI / 180;
      x = Mathf.Sin(input);
      y = -Mathf.Cos(input);
    }
    else if (angle >= 270f && angle < 360f)
    {
      float a = angle - 270f;
      float input = a * Mathf.PI / 180;
      x = Mathf.Cos(input);
      y = Mathf.Sin(input);
    }
    Vector3 direction = new Vector3(x, y, 0f);
    SetBulletSettings();
    GetComponent<Rigidbody2D>().velocity = speed * direction * ratio;
  }
  void SetBulletSettings()
  {
    hits = BowManager.HitsPerHit;
    damage = BowManager.BulletDmg;
    aoe = BowManager.AOE;
    chain = BowManager.ChainExplosion;
    pull = BowManager.PullForce;
    pierce = BowManager.Pierce;
    speed = BowManager.BulletSpeed;
    gameObject.GetComponent<CircleCollider2D>().enabled = true;
  }
  void CreateEffect(GameObject prefab, Transform parent, Vector3 pos)
  {
    GameObject effect = Instantiate(prefab, pos, Quaternion.identity, parent);
  }
  void OnTriggerEnter2D(Collider2D coll)
  {
    if (coll.gameObject.tag == "TauntEnemy" || coll.gameObject.tag == "Enemy")
    {

      if (chain && coll.transform.parent.GetComponent<ChainExplosion>().Chained == false)
      {
        Transform Lifebar = coll.transform.parent.Find("State").Find("Life").Find("Background");
        coll.transform.parent.GetComponent<ChainExplosion>().Chained = true;
        CreateEffect(Effects.Find(x => x.name == "ChainedEffect"), Lifebar, Lifebar.position);
      }
      EnemyLife life = coll.transform.parent.gameObject.GetComponent<EnemyLife>();
      Transform enemyCenter = coll.transform.parent;
      life.takeDamage(damage);
      CreateEffect(Effects.Find(x => x.name == "NormalHitEffect"), enemyCenter, enemyCenter.position);
      for (int i = 0; i < hits; i++)
      {
        life.takeDamage(damage);
        if (life.currentLife > 0f)
        {
          CreateEffect(Effects.Find(x => x.name == "NormalHitEffect"), enemyCenter, enemyCenter.position);
        }
      }
      if (aoe)
      {
        life.AoeHit(damage);
        CreateEffect(Effects.Find(x => x.name == "AoeHit"), enemyCenter, enemyCenter.position);
      }
      pierce--;
      if (pierce <= 0)
      {
        gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
      }
    }
  }



}
