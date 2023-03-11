using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
  Enemy data;
  float Damage;
  void Awake()
  {
    data = gameObject.GetComponent<EnemyLife>().data;
    Damage = data.Damage;
  }
  void Update()
  {
    if (transform.position.y < -7.25f)
    {
      LifeManager.CurrentLife -= Damage;
      //dmg animation
      //death animation?
      Destroy(gameObject);
    }
  }
}
