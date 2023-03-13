using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampEnemyPosition : MonoBehaviour
{
  void Update()
  {
    if (transform.position.x > 5.1f)
    {
      if (transform.position.x > 6.5f)
      {
        Destroy(gameObject);
      }
      correctPosition(5.1f);
    }
    else if (transform.position.x < -5.1f)
    {
      if (transform.position.x < -6.5f)
      {
        Destroy(gameObject);
      }
      correctPosition(-5.1f);
    }
  }
  void correctPosition(float x)
  {
    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-(5f * transform.position.x - x), 0f));
  }
}
