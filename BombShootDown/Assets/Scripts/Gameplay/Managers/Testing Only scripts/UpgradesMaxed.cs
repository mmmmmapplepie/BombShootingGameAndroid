using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UpgradesMaxed : MonoBehaviour {
  string[] world1Upg = new string[10] { "UpgradeSlot", "MaximumLife", "LifeRecovery", "AmmunitionMax", "AmmunitionRate", "Damage", "Helpers", "BulletSpeed", "ReloadTime", "BombDamage" };
  string[] world2Upg = new string[6] { "Revive", "ArmorPierce", "HitsPerHit", "Pierce", "AoeHit", "Laser" };
  string[] world3Upg = new string[4] { "Nuke", "ChainExplosion", "PullEnemies", "DoubleGun" };
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
    UpgradesEquipped.UpgradedSlots = 30;
  }
  void setMaximumLife() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("MaximumLife")) {
      float remainingliferatio = LifeManager.CurrentLife / BowManager.MaxLife;
      BowManager.MaxLife = 10f + 10f * 10f;
      LifeManager.CurrentLife = remainingliferatio * BowManager.MaxLife;
    }
  }
  void setLifeRecovery() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("LifeRecovery")) {
      BowManager.LifeRecovery = 10f / 5f;
    }
  }
  void setDamage() {
    BowManager.BulletDmg = 1f;
    if (UpgradesEquipped.EquippedUpgrades.Contains("Damage")) {
      BowManager.BulletDmg = 1f + 10f;
    }
  }
  void setHelpers() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("Helpers")) {
      int lvl = 10;
      float damageUp = 0.5f + (float)lvl * 0.05f;
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
      BowManager.BulletSpeed = 5f + 3f * 10f;
    }
  }
  void setReloadTime() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("ReloadTime")) {
      BowManager.ReloadRate = 2f / (1f + 20f);
    }
  }
  void setRevive() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("Revive")) {
      BowManager.Revive = 10f / 10f;
      BowManager.ReviveUsable = true;
    }
  }
  void setArmorPierce() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("ArmorPierce")) {
      BowManager.ArmorPierce = 10;
    }
  }
  void setHitsPerHit() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("HitsPerHit")) {
      BowManager.HitsPerHit = 10;
    }
  }
  void setPierce() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("Pierce")) {
      int lvl = UpgradesManager.returnDictionaryValue("Pierce")[1];
      BowManager.Pierce = lvl + 1;
    }
  }
  void setAoeHit() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("AoeHit")) {
      BowManager.AOE = true;
      BowManager.AOEDmg = 0.5f + 10f / 20f;
    }
  }
  void setChainExplosion() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("ChainExplosion")) {
      BowManager.ChainExplosionDmg = 0.2f * 10f;
      BowManager.ChainExplosion = true;
    }
  }
  void setPullEnemies() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("PullEnemies")) {
      BowManager.PullForce = 10f;
    }
  }
  void setAmmunitionRate() {
    BowManager.AmmoRate = 4f;
    if (UpgradesEquipped.EquippedUpgrades.Contains("AmmunitionRate")) {
      BowManager.AmmoRate = 4f / (0.2f + 10f);
    }
  }
  void setAmmunitionMax() {
    //resetting this so that double gun doesnt repeatedly increase things.
    BowManager.MaxAmmo = 10;
    if (UpgradesEquipped.EquippedUpgrades.Contains("AmmunitionMax")) {
      BowManager.MaxAmmo = 60;
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
      BowManager.AmmoRate = BowManager.AmmoRate / 1.1f;
      BowManager.MaxAmmo = BowManager.MaxAmmo * 2;
      //double ammomax and rate
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