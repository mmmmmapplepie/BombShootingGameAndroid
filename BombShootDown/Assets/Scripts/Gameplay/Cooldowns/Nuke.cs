using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Effectsdelays:
// bomb,0.3;
// laser 0.5;
// nuke 0.5s;
// spawn 0.5;
// chained 0.2;


public class Nuke : MonoBehaviour
{
  [SerializeField]
  GameObject NukeEffect;
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
  void Start()
  {
    SetBaseCooldown();
    cooldownTimerChangeReceptor = BowManager.CoolDownRate;
  }
  public void checkUpgradesForNukeEquipped()
  {
    if (UpgradesEquipped.EquippedUpgrades.Contains("Nuke"))
    {
      NukeButton.SetActive(true);
    }
  }
  void SetBaseCooldown()
  {
    int lvl = UpgradesManager.returnDictionaryValue("Nuke")[1];
    BaseNukeCooldown = 250f - 10f * (float)lvl;
    NukeDamage = 50f + 10f * (float)lvl;
  }
  //Cooldown display
  void Update()
  {
    //check if cooldownrate changed and fix cooldown if it did.
    if (BowManager.CoolDownRate != cooldownTimerChangeReceptor)
    {
      float oldBaseTime = BaseNukeCooldown / cooldownTimerChangeReceptor;
      float newBaseTime = BaseNukeCooldown / BowManager.CoolDownRate;
      float ratioRemaining = remainingTime / oldBaseTime;
      float newRemaining = ratioRemaining * newBaseTime;
      remainingTime = newRemaining;
    }
    if (remainingTime > 0f)
    {
      countDownTimer();
    }
    if (remainingTime <= 0f)
    {
      NukeButton.GetComponent<Button>().interactable = true;
      cooldownCover.fillAmount = 0;
      remainingTime = 0f;
    }
  }
  void countDownTimer()
  {
    remainingTime -= Time.deltaTime;
    RenderCooldownImage();
  }
  void RenderCooldownImage()
  {
    float oldBaseTime = BaseNukeCooldown / cooldownTimerChangeReceptor;
    float ratioRemaining = remainingTime / oldBaseTime;
    cooldownCover.fillAmount = ratioRemaining;
  }
  IEnumerator FireNuke()
  {
    Instantiate(NukeEffect, new Vector3(0f, 0f, 0f), Quaternion.identity);
    yield return new WaitForSeconds(0.5f);
    nukeDamage();
  }

  public void UseNuke()
  {
    if (BowManager.UsingCooldown || remainingTime != 0f)
    {
      return;
    }
    NukeButton.GetComponent<Button>().interactable = false;
    remainingTime = BaseNukeCooldown / cooldownTimerChangeReceptor;
    StartCoroutine("FireNuke");
  }
  void nukeDamage()
  {
    //instantiate and sound effects
    GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    GameObject[] TauntEnemies = GameObject.FindGameObjectsWithTag("TauntEnemy");
    foreach (GameObject enemies in Enemies)
    {
      enemies.transform.parent.gameObject.GetComponent<EnemyLife>().takeTrueDamage(NukeDamage);
    }
    foreach (GameObject enemies in TauntEnemies)
    {
      enemies.transform.parent.gameObject.GetComponent<EnemyLife>().takeTrueDamage(NukeDamage);
    }
  }










}

