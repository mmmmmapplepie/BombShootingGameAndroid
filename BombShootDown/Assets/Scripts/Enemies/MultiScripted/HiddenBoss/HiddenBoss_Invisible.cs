using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBoss_Invisible : MonoBehaviour {
  [SerializeField] float pickRadius;
  [SerializeField] Collider2D effectRange;
  List<GameObject> Enemies = new List<GameObject>();
  void OnEnable() {
    InitialAddEnemies();
  }
  void InitialAddEnemies() {
    Collider2D[] EnemiesTemp = Physics2D.OverlapCircleAll(transform.root.position, pickRadius);
    foreach (Collider2D coll in EnemiesTemp) {
      if (coll.tag == "Enemy" || coll.tag == "TauntEnemy") {
        Enemies.Add(coll.gameObject);
        ChangeVisibility(coll.gameObject, true);
      }
    }
  }

  void ChangeVisibility(GameObject imageObject, bool Invisible) {
    float opacity = 1f;
    GameObject chained = transform.Find("State").Find("Life").Find("Background").GetChild(0).gameObject;
    if (Invisible) {
      if (chained != null) chained.SetActive(false);
      opacity = 0f;
    } else {
      if (chained != null) chained.SetActive(true);
    }
    Transform parent = imageObject.transform;
    Color originalColor = imageObject.GetComponent<SpriteRenderer>().color;
    imageObject.GetComponent<SpriteRenderer>().color = new Color(originalColor.r, originalColor.g, originalColor.b, opacity);
    if (parent.childCount != 0) {
      foreach (Transform child in parent) {
        ChangeVisibility(child.gameObject, Invisible);
      }
    }
  }
  void OnTriggerEnter2D(Collider2D coll) {
    if (coll.tag != "Enemy" && coll.tag != "TauntEnemy") {
      return;
    }
    Enemies.Add(coll.gameObject);
    ChangeVisibility(coll.gameObject, true);
  }
  void OnTriggerExit2D(Collider2D coll) {
    if (coll.tag != "Enemy" && coll.tag != "TauntEnemy") {
      return;
    }
    Enemies.Remove(coll.gameObject);
    ChangeVisibility(coll.gameObject, false);
  }
  void OnDisable() {
    foreach (GameObject enemies in Enemies) {
      ChangeVisibility(enemies, false);
    }
    Enemies.Clear();
  }
}