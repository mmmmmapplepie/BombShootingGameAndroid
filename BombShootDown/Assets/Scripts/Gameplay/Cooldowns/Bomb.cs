using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour
{
  [SerializeField]
  GameObject BombButton;
  [SerializeField]
  Image cooldownCover;
  [SerializeField]
  GameObject clickPanel;
  // [SerializeField]
//BombExplosionPrefab;
  bool shot = false;
  float bombRadius = 2f;
  float BombDamage = 5f;
  float BaseBombCooldown = 20f; //base is actually 19f due to lvl being 1 at the beginning;
  float remainingTime = 0f;
  float cooldownTimerChangeReceptor;
  //Initial setup including upgrades
  void Start() {
    SetBaseSettings();
    cooldownTimerChangeReceptor = BowManager.CoolDownRate;
  }
  public void checkUpgradesForBombDamageEquipped() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("BombDamage")) {
      BombButton.SetActive(true);
    }
  }
  void SetBaseSettings() {
    int lvl = UpgradesManager.returnDictionaryValue("BombDamage")[1];
    BaseBombCooldown = 20f - (float)lvl;
    bombRadius = 2f + (float)lvl/10f;
    BombDamage = 5f + (float)lvl;
  }
  //Cooldown display
  void Update()
  {
    //check if cooldownrate changed and fix cooldown if it did.
    if (BowManager.CoolDownRate != cooldownTimerChangeReceptor) {
      float oldBaseTime = BaseBombCooldown/cooldownTimerChangeReceptor;
      float newBaseTime = BaseBombCooldown/BowManager.CoolDownRate;
      float ratioRemaining = remainingTime/oldBaseTime;
      float newRemaining = ratioRemaining * newBaseTime;
      remainingTime = newRemaining;
    }
    if (remainingTime > 0f) {
      countDownTimer();
    }
    if (remainingTime <= 0f) {
      BombButton.GetComponent<Button>().interactable = true;
      cooldownCover.fillAmount = 0;
      remainingTime = 0f;
    }
  }
  void countDownTimer() {
    remainingTime -= Time.deltaTime;
    RenderCooldownImage();
  }
  void RenderCooldownImage() {
    float oldBaseTime = BaseBombCooldown/cooldownTimerChangeReceptor;
    float ratioRemaining = remainingTime/oldBaseTime;
    cooldownCover.fillAmount = ratioRemaining;
  }
  //Bomb functionality
  public void UseBomb() {
    if (BowManager.UsingCooldown || remainingTime != 0f) {
      return;
    }
    BombButton.GetComponent<Button>().interactable = false;
    remainingTime = BaseBombCooldown/cooldownTimerChangeReceptor;

    clickPanel.SetActive(true);
    BowManager.UsingCooldown = true;
    StartCoroutine("RemoveButtonTouch");
    StartCoroutine("clickTimer");
  }
  IEnumerator RemoveButtonTouch() {
    while (Input.touchCount != 0 && clickPanel.activeSelf && shot == false) {
      yield return null;
    }
    if (clickPanel.activeSelf && shot == false) {
      StartCoroutine("BombPlacement");
    }
  }
  IEnumerator clickTimer() {
    float timer = 0f;//3sec
    float timescale = 0.2f;//0.2 is the base
    float opacity = 0.4f;//0.4 is base
    while (timer < 2f && shot == false) {
      timer += Time.deltaTime;
      Time.timeScale = timescale;
      timescale = 0.2f + 0.8f*timer/2f;
      opacity = 0.4f - 0.4f*timer/2f;
      clickPanel.GetComponent<Image>().color = new Color(0f, 0f, 0f, opacity);
      yield return null;
    }
    if (shot == false) {
      RandomPlacement();
    }
    clickPanel.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.4f);
    ResetValues();
  }
  void ResetValues() {
    clickPanel.SetActive(false);
    shot = false;
    BowManager.UsingCooldown = false;
    Time.timeScale = 1f;
  }
  IEnumerator BombPlacement() {
    while(clickPanel.activeSelf && shot == false) {
      if (Input.touchCount > 0 && remainingTime > 0f) {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began) {
          shot = true;
          Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
          fireBomb(touchPos.x, touchPos.y);
          yield break;
          //Instantiate(BombExplosionPrefab, touchPos, Quaternion.Identity);
        }
      }
      yield return null;
    }
  }
  void RandomPlacement() {
    float x = Random.Range(-5.625f, 5.625f);
    float y = Random.Range(-7f, 10f);
    fireBomb(x, y);
  }
  void fireBomb(float x, float y) {
    Collider2D[] Objects = Physics2D.OverlapCircleAll(new Vector2 (x, y), bombRadius);
    foreach (Collider2D coll in Objects)
    {
      if ((coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "TauntEnemy")) {
        //effects instantiate
        coll.transform.parent.gameObject.GetComponent<EnemyLife>().takeTrueDamage(BombDamage);
      }
    }
  }











}
