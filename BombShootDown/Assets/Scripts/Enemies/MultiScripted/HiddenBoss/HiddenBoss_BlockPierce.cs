using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBoss_BlockPierce : MonoBehaviour {
  void OnTriggerEnter2D(Collider2D coll) {
    if (coll.tag == "Bullet") {
      coll.GetComponent<IBullet>().pierce = 1;
    }
  }
}
