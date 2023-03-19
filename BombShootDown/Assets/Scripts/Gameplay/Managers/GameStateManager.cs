using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour {
  [SerializeField]
  GameObject GameEndScreen;
  [SerializeField]
  GameObject WinScreen;
  [SerializeField]
  GameObject revivePanel;
  void Update() {
    if (LifeManager.CurrentLife <= 0f) {
      if (BowManager.ReviveUsable == true && LifeManager.ReviveUsed == false) {
        LifeManager.ReviveUsed = true;
        Revive();
      } else {
        GameEndScreen.SetActive(true);
      }
    }
    if (WaveController.LevelCleared == true && LifeManager.CurrentLife > 0f) {
      WinScreen.SetActive(true);
    }
  }
  void Revive() {
    revivePanel.SetActive(true);
    float revtime = BowManager.Revive * 10f;
    StartCoroutine("Reviving", revtime);
    Invoke("deactivatePanel", revtime);
  }
  void deactivatePanel() {
    revivePanel.SetActive(false);
  }
  IEnumerator Reviving(float revtime) {
    float time = Time.time;
    LifeManager.CurrentLife = BowManager.MaxLife * BowManager.Revive;
    LifeManager.ReviveRoutine = true;
    while (revivePanel.activeSelf == true) {
      float ratio = 1f - 1f * (Time.time - time) / revtime;
      revivePanel.GetComponent<Image>().color = new Color(1f, 1f, 1f, ratio);
      yield return null;
    }
    revivePanel.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    LifeManager.ReviveRoutine = false;
  }




  void OnDestroy() {
    //ResetAll the manager variables;
    ResetGameplayManagerVariables();
  }
  void ResetGameplayManagerVariables() {
    BowManager.MaxAmmo = 10;//base 10
    BowManager.CurrentAmmo = BowManager.MaxAmmo;
    BowManager.AmmoRate = 4f;//base 4
    BowManager.ReloadRate = 2f;//base 2
    BowManager.BulletDmg = 1f;//base 1
    BowManager.HelperDmg = 0.5f; //base 0.5. max at equal to bulletdmg
    BowManager.ArmorPierce = 0;//0 at base
    BowManager.BulletSpeed = 5f;//base 5
    BowManager.AOE = false;//false base
    BowManager.AOEDmg = 0.5f;
    BowManager.ChainExplosion = false;//false base
    BowManager.ChainExplosionDmg = 0.2f;//base 0.2;
    BowManager.PullForce = 0f;//base 0;
    BowManager.Pierce = 1; // base 1
    BowManager.HitsPerHit = 0; // base 0
    BowManager.MaxLife = 10f;//max at 100f;
    BowManager.LifeRecovery = 0f;//base 0f
    BowManager.Revive = 0.1f;//max at 1f;
    BowManager.ReviveUsable = false;

    BowManager.bowTouchID = new int[2] { -1, -1 };
    BowManager.center = new Vector3[2];
    BowManager.touchpos = new Vector3[2];
    BowManager.GunsReady = false;

    BowManager.CoolDownRate = 1f;
    BowManager.UsingCooldown = false;

    BowManager.EnemySpeed = 1f;
    BowManager.EnemyDamage = 1f;

    //EquippedUpgradesManager
    UpgradesEquipped.EquippedUpgrades.Clear();
    UpgradesEquipped.tempUpgHolder.Clear();
    UpgradesEquipped.UpgradedSlots = 5;
    UpgradesEquipped.LevelSlots = 5;
    UpgradesEquipped.AvailableSlots = 5;


    LifeManager.CurrentLife = 10f;
    LifeManager.Alive = true;
    LifeManager.ReviveUsed = false;
    LifeManager.ReviveRoutine = false;

    if (GameObject.Find("WaveController") != null) {
      WaveController.LevelCleared = false;
      WaveController.CurrentWave = 0;
      WaveController.WavesCleared = 0;
      WaveController.startWave = false;
    }

  }
}
