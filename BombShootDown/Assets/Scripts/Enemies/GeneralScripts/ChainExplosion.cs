using UnityEngine;
public class ChainExplosion : MonoBehaviour
{
  [HideInInspector]
  public bool Chained = false;
  bool animationAdd = false;
  RectTransform rect;
  void Start()
  {
    rect = GetComponent<RectTransform>();
  }
  void Update()
  {
    if (Chained && !animationAdd)
    {
      animationAdd = true;
      //Add "chained" animation
    }
  }
  public void Explode()
  {
    //explosion sound and animation
    Collider2D[] Objects = Physics2D.OverlapCircleAll(transform.position, 1.5f);
    foreach (Collider2D coll in Objects)
    {
      if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "TauntEnemy")
      {
        if (coll.gameObject == gameObject)
        {
          continue;
        }
        coll.transform.parent.GetComponent<ChainExplosion>().Chained = true;
        if (coll.transform.parent.GetComponent<EnemyLife>().currentLife <= 0)
        {
          continue;
        }
        coll.transform.parent.GetComponent<EnemyLife>().ChainExplosion();
      }
    }
  }
}
