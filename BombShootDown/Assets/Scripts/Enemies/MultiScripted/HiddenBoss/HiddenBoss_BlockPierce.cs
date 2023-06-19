using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBoss_BlockPierce : MonoBehaviour {
  void Start() {
    //just so i can disable in the inspector
  }
  void OnTriggerEnter2D(Collider2D coll) {
    if (!this.enabled) return;
    if (coll.tag == "Bullet") {
      coll.GetComponent<IBullet>().pierce = 1;
    }
  }
}
