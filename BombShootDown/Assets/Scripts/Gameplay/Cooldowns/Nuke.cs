using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Effectsdelays:
// bomb,0.3;
// laser 0.5;
// nuke 0.5s;
// spawn 0.5;
// chained 0.2;


public class Nuke : MonoBehaviour {
  [SerializeField]
  GameObject NukeEffect;
  [SerializeField]
  GameObject NukeButton;
  [SerializeField]
  Image cooldownCover;
  [SerializeField]
  GameObject clickPanel;
  AudioManagerCannon audioManager;
  float BaseNukeCooldown = 250f;
  float NukeDamage = 500f;
  float remainingTime = 0f;
  float cooldownTimerChangeReceptor = 1f;
  void Awake() {
    audioManager = GameObject.Find("AudioManagerCannon").GetComponent<AudioManagerCannon>();
    SetBaseCooldown();
  }
  public void checkUpgradesForNukeEquipped() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("Nuke")) {
      NukeButton.SetActive(true);
    }
  }
  void SetBaseCooldown() {
    int lvl = UpgradesManager.returnDictionaryValue("Nuke")[1];
    BaseNukeCooldown = 250f - 5f * (float)lvl;
    NukeDamage = 500f + 150f * (float)lvl; // max at 2000 dmg
  }
  void Update() {
    if (BowManager.CoolDownRate != cooldownTimerChangeReceptor) {
      float oldBaseTime = BaseNukeCooldown * cooldownTimerChangeReceptor;
      float newBaseTime = BaseNukeCooldown * BowManager.CoolDownRate;
      float ratioRemaining = remainingTime / oldBaseTime;
      float newRemaining = ratioRemaining * newBaseTime;
      remainingTime = newRemaining;
      cooldownTimerChangeReceptor = BowManager.CoolDownRate;
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
    float BaseTime = BaseNukeCooldown * cooldownTimerChangeReceptor;
    float ratioRemaining = remainingTime / BaseTime;
    cooldownCover.fillAmount = ratioRemaining;
  }
  IEnumerator FireNuke() {
    Instantiate(NukeEffect, new Vector3(0f, 0f, 0f), Quaternion.identity);
    audioManager.PlayAudio("Nuke");
    yield return new WaitForSeconds(0.5f);
    nukeDamage();
  }

  public void UseNuke() {
    if (BowManager.UsingCooldown || remainingTime != 0f) {
      return;
    }
    NukeButton.GetComponent<Button>().interactable = false;
    remainingTime = BaseNukeCooldown * cooldownTimerChangeReceptor;
    StartCoroutine("FireNuke");
  }
  void nukeDamage() {
    //instantiate and sound effects
    GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    GameObject[] TauntEnemies = GameObject.FindGameObjectsWithTag("TauntEnemy");
    foreach (GameObject enemies in Enemies) {
      enemies.transform.root.gameObject.GetComponent<EnemyLife>().takeTrueDamage(NukeDamage);
    }
    foreach (GameObject enemies in TauntEnemies) {
      enemies.transform.root.gameObject.GetComponent<EnemyLife>().takeTrueDamage(NukeDamage);
    }
  }










}

