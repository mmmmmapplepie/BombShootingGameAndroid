using UnityEngine;

public class BowManager
{

  // bullet related
  public static int MaxAmmo = 10;//base 10
  public static int CurrentAmmo = MaxAmmo;
  public static float AmmoRate = 4f;//base 4
  public static float ReloadRate = 2f;//base 2
  public static float BulletDmg = 1f;//base 1
  public static float HelperDmg = 0.5f;//helpers get the hits/hit and pierce/ and armor pierce.
  public static int ArmorPierce = 0;//0 at base +1 with pierce upgrade base
  public static float BulletSpeed = 5f;//base 5
  public static bool AOE = false;//false base
  public static float AOEDmg = 0.5f;//starts at half the base dmg to base dmg at max
  public static bool ChainExplosion = false;//false base
  public static float ChainExplosionDmg = 0.2f;//base 0.2;
  public static float PullForce = 0f;//base 0;
  public static int Pierce = 1; // base 1
  public static int HitsPerHit = 0; // base 0


  //Fortress Related
  public static float MaxLife = 10f;//max at 110f;
  public static float LifeRecovery = 0f;//base at 1/10 of max lifemax
  public static float Revive = 0.1f;//max at 1f;
  public static bool ReviveUsable = false;//false base

  // Gun movement related
  public static int[] bowTouchID = new int[2] { -1, -1 };
  public static bool GunsReady = false;
  public static Vector3[] center = new Vector3[2];
  public static Vector3[] touchpos = new Vector3[2];


  // Cooldowns
  public static float CoolDownRate = 1f;
  public static bool UsingCooldown = false;


  //Enemies stats
  public static float EnemySpeed = 1f;
  public static float EnemyDamage = 1f;


}
