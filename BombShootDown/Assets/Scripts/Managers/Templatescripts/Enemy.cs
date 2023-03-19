using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject {
  //Visuals / Identifiers for WorldMap Data
  public new string name;
  public string enemyDescription;
  public Sprite sprite;

  //0 is normal enemy, 1 is mini boss, 2 is main boss
  public int Boss = 0;



  //Prefab
  public GameObject enemyPrefab;

  //Mechanics
  public float Life;
  public float Damage;
  public float Speed;
  public int MaxShield;
  public int Shield;
  public int Armor;
  public bool Taunt;
}
