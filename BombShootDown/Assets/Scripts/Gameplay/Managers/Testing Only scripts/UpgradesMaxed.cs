using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UpgradesMaxed : MonoBehaviour {
  string[] world1Upg = new string[10] { "UpgradeSlot", "MaximumLife", "LifeRecovery", "AmmunitionMax", "AmmunitionRate", "Damage", "Helpers", "BulletSpeed", "ReloadTime", "BombDamage" };
  string[] world2Upg = new string[6] { "Revive", "ArmorPierce", "HitsPerHit", "Pierce", "AoeHit", "Laser" };
  string[] world3Upg = new string[4] { "Nuke", "ChainExplosion", "PullEnemies", "DoubleGun" };

  //Maxed
  int upgradeslot = 10, maxlife = 10, liferecovery = 10, ammunitionmax = 10, ammunitionrate = 10, damage = 10, helper = 10, bulletSpeed = 10, reloadtime = 10, revive = 10, armorpierce = 10, hitsperhit = 10, pierce = 10, aoehit = 10, chainexplo = 10, pull = 10;

  // int upgradeslot = 1, maxlife = 1, liferecovery = 1, ammunitionmax = 1, ammunitionrate = 1, damage = 1, helper = 1, bulletSpeed = 1, reloadtime = 1, revive = 1, armorpierce = 1, hitsperhit = 1, pierce = 1, aoehit = 1, chainexplo = 1, pull = 1;

  public static List<string> AvailableUpgrades = new List<string>();
  [SerializeField]
  GameObject bow1, bow2;
  [SerializeField]
  GameObject innerHelpers;
  [SerializeField]
  GameObject middleHelpers;
  [SerializeField]
  GameObject outerHelpers;


  void Awake() {
    //Just while testing/////////////
    UpgradesManager.loadAllData();
    /////////////////////////////////
    setUpgradeSlot();
  }
  void Update() {
    LifeRecoveryInGame();
  }
  void LifeRecoveryInGame() {
    if (LifeManager.CurrentLife < BowManager.MaxLife && Time.timeScale != 0f && UpgradesEquipped.EquippedUpgrades.Contains("LifeRecovery")) {
      float recovery = BowManager.LifeRecovery;
      if ((LifeManager.CurrentLife + recovery * Time.deltaTime) > BowManager.MaxLife) {
        LifeManager.CurrentLife = BowManager.MaxLife;
      } else {
        LifeManager.CurrentLife += recovery * Time.deltaTime;
      }
    }
  }
  public void setUpgrades() {
    setMaximumLife();
    setLifeRecovery();
    setDamage();
    setHelpers();
    setBulletSpeed();
    setReloadTime();
    setRevive();
    setArmorPierce();
    setHitsPerHit();
    setPierce();
    setAoeHit();
    setChainExplosion();
    setPullEnemies();
    //make sure double gun is last as it should double ammomax and also increase ammo rate a little;
    setAmmunitionRate();
    setAmmunitionMax();
    setDoubleGun();
  }
  void setUpgradeSlot() {
    int lvl = UpgradesManager.returnDictionaryValue("UpgradeSlot")[1];
    UpgradesEquipped.UpgradedSlots = upgradeslot * 5;
    if (lvl == 10) {
      UpgradesEquipped.UpgradedSlots = 35;
    }
  }
  void setMaximumLife() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("MaximumLife")) {
      float remainingliferatio = LifeManager.CurrentLife / BowManager.MaxLife;
      BowManager.MaxLife = 10f + 20f * (float)maxlife;
      LifeManager.CurrentLife = remainingliferatio * BowManager.MaxLife;
    }
  }
  void setLifeRecovery() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("LifeRecovery")) {
      if (liferecovery < 5) {
        BowManager.LifeRecovery = (float)liferecovery * 0.4f;
      } else {
        BowManager.LifeRecovery = (float)liferecovery * 0.8f;
      }
    }
  }
  void setDamage() {
    BowManager.BulletDmg = 1f;
    if (UpgradesEquipped.EquippedUpgrades.Contains("Damage")) {
      BowManager.BulletDmg = 1f + (float)damage;
    }
  }
  void setHelpers() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("Helpers")) {
      int lvl = helper;
      float damageUp = 0.5f + 0.3f * lvl;
      BowManager.HelperDmg = BowManager.BulletDmg * damageUp;
      outerHelpers.SetActive(true);
      if (lvl > 3) {
        middleHelpers.SetActive(true);
      }
      if (lvl > 8) {
        innerHelpers.SetActive(true);
      }
    }
  }
  void setBulletSpeed() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("BulletSpeed")) {
      BowManager.BulletSpeed = 5f + 3f * bulletSpeed;
    }
  }
  void setReloadTime() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("ReloadTime")) {
      int lvl = reloadtime;
      BowManager.ReloadRate = 2f / (4f * (float)lvl);
      if (lvl == 10) {
        BowManager.ReloadRate = 0f;
      }
    }
  }
  void setRevive() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("Revive")) {
      BowManager.Revive = revive / 10f;
      BowManager.ReviveUsable = true;
    }
  }
  void setArmorPierce() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("ArmorPierce")) {
      BowManager.ArmorPierce = armorpierce;
    }
  }
  void setHitsPerHit() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("HitsPerHit")) {
      BowManager.HitsPerHit = hitsperhit;
    }
  }
  void setPierce() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("Pierce")) {
      int lvl = UpgradesManager.returnDictionaryValue("Pierce")[1];
      BowManager.Pierce = pierce + 1;
    }
  }
  void setAoeHit() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("AoeHit")) {
      BowManager.AOE = true;
      BowManager.AOEDmg = 0.5f + aoehit / 20f;
    }
  }
  void setChainExplosion() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("ChainExplosion")) {
      BowManager.ChainExplosionDmg = 0.2f * chainexplo;
      BowManager.ChainExplosion = true;
    }
  }
  void setPullEnemies() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("PullEnemies")) {
      BowManager.PullForce = (float)pull;
    }
  }
  void setAmmunitionRate() {
    BowManager.AmmoRate = 4f;
    if (UpgradesEquipped.EquippedUpgrades.Contains("AmmunitionRate")) {
      BowManager.AmmoRate = 4f / (1f + ammunitionrate * 0.7f);
    }
  }
  void setAmmunitionMax() {
    //resetting this so that double gun doesnt repeatedly increase things.
    BowManager.MaxAmmo = 10;
    if (UpgradesEquipped.EquippedUpgrades.Contains("AmmunitionMax")) {
      BowManager.MaxAmmo = 9 * ammunitionmax;
      BowManager.CurrentAmmo = BowManager.MaxAmmo + BowManager.CurrentAmmo - 10;
    }
  }
  void setDoubleGun() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("DoubleGun")) {
      Vector3 tempos1 = new Vector3(-3.24f, -7.33f, 0f);
      Vector3 tempos2 = new Vector3(3.24f, -7.33f, 0f);
      bow2.SetActive(true);
      bow1.transform.position = tempos1;
      bow2.transform.position = tempos2;
      BowManager.AmmoRate = BowManager.AmmoRate / 1.2f;
      BowManager.MaxAmmo = Mathf.FloorToInt(BowManager.MaxAmmo * 1.2f);
      //increase ammomax and rate
    }
  }
  public void SpeedUpTimeAfterUpgrades() {
    StartCoroutine("speedupTime");
  }
  IEnumerator speedupTime() {
    while (Time.timeScale < 1f) {
      yield return new WaitForSecondsRealtime(0.01f);
      Time.timeScale += 0.003f;
      //this takes roughly 3.7sec unscaled time. Use 3.5 as its a nicer number.
    }
    Time.timeScale = 1f;
    yield return null;
  }
}