using UnityEngine;
using System.Collections.Generic;

public class EnemyDamage : MonoBehaviour
{
  [SerializeField]
  List<GameObject> damageEffects;
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
      if (Damage >= 50)
      {
        CreateEffect(damageEffects.Find(x => x.name == "EnemyDealDamageTremendous"), null, gameObject.transform.position);
      }
      else if (Damage >= 30)
      {
        CreateEffect(damageEffects.Find(x => x.name == "EnemyDealDamageBig"), null, gameObject.transform.position);
      }
      else if (Damage >= 10)
      {
        CreateEffect(damageEffects.Find(x => x.name == "EnemyDealDamageMedium"), null, gameObject.transform.position);
      }
      else
      {
        CreateEffect(damageEffects.Find(x => x.name == "EnemyDealDamageSmall"), null, gameObject.transform.position);
      }
      Destroy(gameObject);
    }
  }

  void CreateEffect(GameObject prefab, Transform parent, Vector3 pos)
  {
    GameObject effect = Instantiate(prefab, pos, Quaternion.identity, parent);
  }
}
