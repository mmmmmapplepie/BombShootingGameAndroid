using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
  //Visuals / Identifiers for WorldMap Data
  public new string name;
  public string enemyDescription;
  public Sprite sprite;


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
