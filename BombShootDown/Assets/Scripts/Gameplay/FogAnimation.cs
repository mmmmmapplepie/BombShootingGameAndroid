using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogAnimation : MonoBehaviour
{
  float timerange;
  float x;
  float y;
  float a;
  float r;
  void Start() {
    timerange = Random.Range(0.05f, 0.2f);
    x = transform.position.x;
    y = transform.position.y;
    a = Random.Range(0.2f, 0.5f);
    r = gameObject.GetComponent<SpriteRenderer>().color.r;
  }
  void Update()
  {
    Vector3 updatepos = new Vector3 (x, y + Mathf.PingPong(timerange*Time.time, 0.5f), 0f);
    transform.position = updatepos;
    gameObject.GetComponent<SpriteRenderer>().color = new Color(r, r, r, 0.5f+Mathf.PingPong(0.7f*timerange*Time.time, a));
  }
}
