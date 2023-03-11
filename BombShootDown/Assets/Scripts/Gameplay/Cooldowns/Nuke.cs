using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nuke : MonoBehaviour
{
  [SerializeField]
  GameObject NukeButton;
  [SerializeField]
  Image cooldownCover;
  [SerializeField]
  GameObject clickPanel;
  // [SerializeField]
//NukeExplosionPrefab;
  float BaseNukeCooldown = 250f;
  float NukeDamage = 50f;
  float remainingTime = 0f;
  float cooldownTimerChangeReceptor;
  //Initial setup including upgrades
  void Start() {
    SetBaseCooldown();
    cooldownTimerChangeReceptor = BowManager.CoolDownRate;
  }
  public void checkUpgradesForNukeEquipped() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("Nuke")) {
      NukeButton.SetActive(true);
    }
  }
  void SetBaseCooldown() {
    int lvl = UpgradesManager.returnDictionaryValue("Nuke")[1];
    BaseNukeCooldown = 250f - 10f*(float)lvl;
    NukeDamage = 50f + 10f*(float)lvl;
  }
  //Cooldown display
  void Update()
  {
    //check if cooldownrate changed and fix cooldown if it did.
    if (BowManager.CoolDownRate != cooldownTimerChangeReceptor) {
      float oldBaseTime = BaseNukeCooldown/cooldownTimerChangeReceptor;
      float newBaseTime = BaseNukeCooldown/BowManager.CoolDownRate;
      float ratioRemaining = remainingTime/oldBaseTime;
      float newRemaining = ratioRemaining * newBaseTime;
      remainingTime = newRemaining;
    }
    if (remainingTime > 0f) {
      countDownTimer();
    }
    if (remainingTime <= 0f) {
      NukeButton.GetComponent<Button>().interactable = true;
      cooldownCover.fillAmount = 0;
      remainingTime = 0f;
    }
  }
  void countDownTimer() {
    remainingTime -= Time.deltaTime;
    RenderCooldownImage();
  }
  void RenderCooldownImage() {
    float oldBaseTime = BaseNukeCooldown/cooldownTimerChangeReceptor;
    float ratioRemaining = remainingTime/oldBaseTime;
    cooldownCover.fillAmount = ratioRemaining;
  }
  // functionality
  public void UseNuke() {
    if (BowManager.UsingCooldown || remainingTime != 0f) {
      return;
    }
    NukeButton.GetComponent<Button>().interactable = false;
    remainingTime = BaseNukeCooldown/cooldownTimerChangeReceptor;
    fireNuke();
  }
  void fireNuke() {
    //instantiate and sound effects
    GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    GameObject[] TauntEnemies = GameObject.FindGameObjectsWithTag("TauntEnemy");
    foreach (GameObject enemies in Enemies) {
      print(1);
      enemies.transform.parent.gameObject.GetComponent<EnemyLife>().takeTrueDamage(NukeDamage);
    }
    foreach (GameObject enemies in TauntEnemies) {
      print(2);
      enemies.transform.parent.gameObject.GetComponent<EnemyLife>().takeTrueDamage(NukeDamage);
    }
  }










}

