using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour
{
  [SerializeField]
  Text WaveStartDisplay;
  [SerializeField]
  Text waveDisplay;
  [SerializeField]
  Text waveShadowDisplay;
  [SerializeField]
  GameObject WaveStartPanel;
  [SerializeField]
  GameObject UpgradesPanel;
  public Level thisLevelData;
  public static bool LevelCleared = false;
  public static int CurrentWave = 0;//wave should start by incrementing this.
  public static int WavesCleared = 0;
  public static bool startWave = false;
  bool inCue = false;
  void Update()
  {
    if (WavesCleared == CurrentWave && LevelCleared == false && inCue == false)
    {
      inCue = true;
      UpgradesEquipped.LevelSlots = thisLevelData.upgradesPerWave[WavesCleared];
      startWave = false;
      Invoke("CueUpgrades", 1f);
    }
  }
  void CueUpgrades()
  {
    UpgradesPanel.SetActive(true);
  }
  public void CueNextWave()
  {
    WaveStartPanel.SetActive(true);
    string waveNumStr = (CurrentWave + 1).ToString();
    WaveStartDisplay.text = "Wave " + waveNumStr;
    waveDisplay.text = waveShadowDisplay.text = "Wave : " + waveNumStr;
    StartCoroutine("MoveWaveScreen");
  }
  void makeGunsReady()
  {
    BowManager.GunsReady = true;
  }

  IEnumerator MoveWaveScreen()
  {
    Invoke("makeGunsReady", 0.05f);
    CurrentWave++;
    inCue = false;
    Vector2 pos = new Vector2(0f, 0f);
    float starttime = Time.unscaledTime;
    while (Time.unscaledTime - starttime < 2f)
    {
      float r = WaveStartPanel.GetComponent<Image>().color.r;
      float b = WaveStartPanel.GetComponent<Image>().color.b;
      float g = WaveStartPanel.GetComponent<Image>().color.g;
      WaveStartPanel.GetComponent<Image>().color = new Color(r, g, b, 2f * (2f - (Time.unscaledTime - starttime)) / 6f);
      yield return null;
    }
    float r_ = WaveStartPanel.GetComponent<Image>().color.r;
    float b_ = WaveStartPanel.GetComponent<Image>().color.b;
    float g_ = WaveStartPanel.GetComponent<Image>().color.g;
    WaveStartPanel.GetComponent<Image>().color = new Color(r_, g_, b_, 2f / 3f);
    WaveStartPanel.SetActive(false);
    startWave = true;
  }
  void OnDestroy()
  {
    //ResetAll the manager variables;
    ResetGameplayManagerVariables();
  }
  void ResetGameplayManagerVariables()
  {
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

    LevelCleared = false;
    CurrentWave = 0;//wave should start by incrementing this.
    WavesCleared = 0;
    startWave = false;
  }


}
