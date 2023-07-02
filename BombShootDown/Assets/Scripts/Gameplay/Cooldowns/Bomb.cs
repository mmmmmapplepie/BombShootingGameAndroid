using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour {
  [SerializeField]
  GameObject BombGuide;
  [SerializeField]
  GameObject BombEffect;
  [SerializeField]
  GameObject BombButton;
  [SerializeField]
  Image cooldownCover;
  [SerializeField]
  GameObject clickPanel;
  AudioManagerCannon audioManager;
  bool shot = false;
  float bombRadius = 2.5f;
  float BombDamage = 5f;
  float BaseBombCooldown = 20f; //base is actually 19f due to lvl being 1 at the beginning;
  float remainingTime = 0f;
  float cooldownTimerChangeReceptor = 1f;
  void Awake() {
    audioManager = GameObject.Find("AudioManagerCannon").GetComponent<AudioManagerCannon>();
    SetBaseSettings();
  }
  public void checkUpgradesForBombDamageEquipped() {
    if (UpgradesEquipped.EquippedUpgrades.Contains("BombDamage")) {
      BombButton.SetActive(true);
    }
  }
  void SetBaseSettings() {
    int lvl = UpgradesManager.returnDictionaryValue("BombDamage")[0];
    // int lvl = 1; //testing
    BaseBombCooldown = 20f - 1.5f * (float)lvl;
    BombDamage = 5f + 4.5f * (float)lvl; // max at 50 dmg
  }
  void Update() {
    if (BowManager.CoolDownRate != cooldownTimerChangeReceptor) {
      float oldBaseTime = BaseBombCooldown * cooldownTimerChangeReceptor;
      float newBaseTime = BaseBombCooldown * BowManager.CoolDownRate;
      float ratioRemaining = remainingTime / oldBaseTime;
      float newRemaining = ratioRemaining * newBaseTime;
      remainingTime = newRemaining;
      cooldownTimerChangeReceptor = BowManager.CoolDownRate;
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
    float BaseTime = BaseBombCooldown * cooldownTimerChangeReceptor;
    float ratioRemaining = remainingTime / BaseTime;
    cooldownCover.fillAmount = ratioRemaining;
  }
  //Bomb functionality
  public void UseBomb() {
    if (BowManager.UsingCooldown || remainingTime != 0f) {
      return;
    }
    BombButton.GetComponent<Button>().interactable = false;
    remainingTime = BaseBombCooldown * cooldownTimerChangeReceptor;

    clickPanel.SetActive(true);
    BombGuide.SetActive(true);
    BowManager.UsingCooldown = true;
    BowManager.GunsReady = false;
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
      timescale = 0.2f + 0.8f * timer / 2f;
      opacity = 0.4f - 0.4f * timer / 2f;
      clickPanel.GetComponent<Image>().color = new Color(0f, 0f, 0f, opacity);
      yield return null;
    }
    if (shot == false) {
      StartCoroutine("RandomPlacement");
    }
    clickPanel.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.4f);
    ResetValues();
  }
  void ResetValues() {
    clickPanel.SetActive(false);
    BombGuide.SetActive(false);
    shot = false;
    BowManager.UsingCooldown = false;
    Invoke("readyguns", 0.5f);
    Time.timeScale = 1f;
  }
  void readyguns() {
    BowManager.GunsReady = true;
  }
  IEnumerator BombPlacement() {
    while (clickPanel.activeSelf && shot == false) {
      if (Input.touchCount > 0 && remainingTime > 0f) {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began) {
          shot = true;
          Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
          Instantiate(BombEffect, new Vector3(touchPos.x, touchPos.y, 0f), Quaternion.identity);
          yield return new WaitForSeconds(0.3f);
          fireBomb(touchPos.x, touchPos.y);
          yield break;
        }
      }
      yield return null;
    }
  }
  IEnumerator RandomPlacement() {
    float x = Random.Range(-5.625f, 5.625f);
    float y = Random.Range(-7f, 10f);
    Instantiate(BombEffect, new Vector3(x, y, 0f), Quaternion.identity);
    yield return new WaitForSeconds(0.3f);
    fireBomb(x, y);
  }
  void fireBomb(float x, float y) {
    audioManager.PlayAudio("Bomb");
    Collider2D[] Objects = Physics2D.OverlapCircleAll(new Vector2(x, y), bombRadius);
    foreach (Collider2D coll in Objects) {
      if ((coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "TauntEnemy")) {
        coll.transform.root.gameObject.GetComponent<IDamageable>().takeTrueDamage(BombDamage);
      }
    }
  }
}
