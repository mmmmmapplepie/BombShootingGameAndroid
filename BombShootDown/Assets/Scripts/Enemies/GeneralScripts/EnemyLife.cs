using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
  [SerializeField]
  public Enemy data;
  [SerializeField]
  GameObject bombObject;
  RectTransform rect;
  [HideInInspector]
  public float maxLife;
  [HideInInspector]
  public float currentLife;
  [HideInInspector]
  public int Armor;
  [HideInInspector]
  public int MaxShield;
  [HideInInspector]
  public int Shield;
  [HideInInspector]
  bool Taunt;
  void Awake()
  {
    maxLife = data.Life;
    currentLife = data.Life;
    Armor = data.Armor;
    Shield = data.Shield;
    MaxShield = data.MaxShield;
    Taunt = data.Taunt;
    if (Taunt)
    {
      bombObject.tag = "TauntEnemy";
    }
    else
    {
      bombObject.tag = "Enemy";
    }
  }
  public void takeTrueDamage(float damage)
  {
    currentLife -= damage;
    if (currentLife <= 0f)
    {
      ShotDeath();
    }
  }
  public void takeDamage(float damage)
  {
    if (Shield > 0)
    {
      //shield sound
      Shield--;
    }
    else
    {
      int Armordiff = Armor - BowManager.ArmorPierce;
      if (Armordiff > 0)
      {
        if (Armordiff > 9)
        {
          //heavy armor diff sound
          currentLife -= damage / 50f; //2% damage only
        }
        else
        {
          //armored sound
          currentLife -= damage - damage * ((float)Armordiff / 10f); //each lvl diff takes a 10% decrease in dmg
        }
      }
      else
      {
        //normal hit sound
        currentLife -= damage;
      }
    }
    if (currentLife <= 0f)
    {
      ShotDeath();
    }
  }
  public void AoeHit(float damage)
  {
    Collider2D[] Objects = Physics2D.OverlapCircleAll(transform.position, 2f);
    foreach (Collider2D coll in Objects)
    {
      if ((coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "TauntEnemy") && coll.gameObject != gameObject)
      {
        coll.transform.parent.gameObject.GetComponent<EnemyLife>().takeDamage(BowManager.AOEDmg * damage);
      }
    }
  }
  public void ChainExplosion()
  {
    takeDamage(BowManager.ChainExplosionDmg * BowManager.BulletDmg);
  }
  void disableMovement()
  {
    gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
    Destroy(transform.Find("Enemy").gameObject.GetComponent<Collider2D>());
    Destroy(transform.Find("MovementControl").gameObject);
  }
  IEnumerator deathSequence()
  {
    disableMovement();
    SpriteRenderer sprite = transform.Find("Enemy").gameObject.GetComponent<SpriteRenderer>();
    for (int i = 0; i < 20; i++)
    {
      float angle = (Mathf.Sin(i)) / (i + 1);
      float ratio = 1f / (1f + i);
      transform.rotation = Quaternion.Euler(0, 0, angle);
      sprite.color = new Color(sprite.color.r / ratio, sprite.color.g / ratio, sprite.color.b / ratio, ratio);
      yield return new WaitForSeconds(0.01f);
    }
    Destroy(gameObject);
  }
  void ShotDeath()
  {
    ChainExplosion script = gameObject.GetComponent<ChainExplosion>();
    if (script.Chained == true)
    {
      script.Explode();
    }
    StartCoroutine("deathSequence");

  }
}
