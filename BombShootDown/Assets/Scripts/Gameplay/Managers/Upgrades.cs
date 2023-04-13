using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Upgrades : MonoBehaviour {
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
    int lvl = UpgradesManager.returnDictionaryValue("UpgradeSlot")[0];
    UpgradesEquipped.UpgradedSlots = 5 * lvl;
    if (lvl == 10) {
      UpgradesEquipped.UpgradedSlots = 35;
    }
  }
  void setMaximumLife() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("MaximumLife")) {
      int lvl = UpgradesManager.returnDictionaryValue("MaximumLife")[0];
      float remainingliferatio = LifeManager.CurrentLife / BowManager.MaxLife;
      BowManager.MaxLife = 10f + (float)lvl * 20f;
      LifeManager.CurrentLife = remainingliferatio * BowManager.MaxLife;
    }
  }
  void setLifeRecovery() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("LifeRecovery")) {
      int lvl = UpgradesManager.returnDictionaryValue("LifeRecovery")[0];
      if (lvl < 5) {
        BowManager.LifeRecovery = (float)lvl * 0.4f;
      } else {
        BowManager.LifeRecovery = (float)lvl * 0.8f;
      }
    }
  }
  void setDamage() {
    BowManager.BulletDmg = 1f;
    if (UpgradesEquipped.EquippedUpgrades.Contains("Damage")) {
      int lvl = UpgradesManager.returnDictionaryValue("Damage")[0];
      BowManager.BulletDmg = 1f + (float)lvl;
    }
  }
  void setHelpers() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("Helpers")) {
      int lvl = UpgradesManager.returnDictionaryValue("Helpers")[0];
      float damageUp = 0.3f + (float)lvl * 0.05f;
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
      int lvl = UpgradesManager.returnDictionaryValue("BulletSpeed")[0];
      BowManager.BulletSpeed = 5f + 3f * (float)lvl;
    }
  }
  void setReloadTime() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("ReloadTime")) {
      int lvl = UpgradesManager.returnDictionaryValue("ReloadTime")[0];
      BowManager.ReloadRate = 2f / (4f * (float)lvl);
      if (lvl == 10) {
        BowManager.ReloadRate = 0f;
      }
    }
  }
  void setRevive() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("Revive")) {
      int lvl = UpgradesManager.returnDictionaryValue("Revive")[0];
      BowManager.Revive = (float)lvl / 10;
      BowManager.ReviveUsable = true;
    }
  }
  void setArmorPierce() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("ArmorPierce")) {
      int lvl = UpgradesManager.returnDictionaryValue("ArmorPierce")[0];
      BowManager.ArmorPierce = lvl;
    }
  }
  void setHitsPerHit() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("HitsPerHit")) {
      int lvl = UpgradesManager.returnDictionaryValue("HitsPerHit")[0];
      BowManager.HitsPerHit = lvl;
    }
  }
  void setPierce() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("Pierce")) {
      int lvl = UpgradesManager.returnDictionaryValue("Pierce")[0];
      BowManager.Pierce = lvl + 1;
    }
  }
  void setAoeHit() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("AoeHit")) {
      int lvl = UpgradesManager.returnDictionaryValue("AoeHit")[0];
      BowManager.AOE = true;
      BowManager.AOEDmg = 0.5f + (float)lvl / 20f;
    }
  }
  void setChainExplosion() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("ChainExplosion")) {
      int lvl = UpgradesManager.returnDictionaryValue("ChainExplosion")[0];
      BowManager.ChainExplosionDmg = 0.2f * (float)lvl;
      BowManager.ChainExplosion = true;
    }
  }
  void setPullEnemies() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("PullEnemies")) {
      int lvl = UpgradesManager.returnDictionaryValue("PullEnemies")[0];
      BowManager.PullForce = (float)lvl;
    }
  }
  void setAmmunitionRate() {
    //resetting this so that double gun doesnt repeatedly increase things.
    BowManager.AmmoRate = 4f;
    if (UpgradesEquipped.EquippedUpgrades.Contains("AmmunitionRate")) {
      int lvl = UpgradesManager.returnDictionaryValue("AmmunitionRate")[0];
      BowManager.AmmoRate = 4f / (1f + (float)lvl * 0.5f);
    }
  }
  void setAmmunitionMax() {
    //resetting this so that double gun doesnt repeatedly increase things.
    BowManager.MaxAmmo = 10;
    if (UpgradesEquipped.EquippedUpgrades.Contains("AmmunitionMax")) {
      int lvl = UpgradesManager.returnDictionaryValue("AmmunitionMax")[0];
      int extraAmmmo = 9 * lvl;
      BowManager.MaxAmmo = extraAmmmo + 10;
      BowManager.CurrentAmmo = Mathf.Min(BowManager.CurrentAmmo + extraAmmmo, BowManager.MaxAmmo);
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
