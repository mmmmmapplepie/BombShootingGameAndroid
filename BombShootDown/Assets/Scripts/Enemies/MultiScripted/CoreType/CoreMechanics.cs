using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreMechanics : MonoBehaviour {
  [SerializeField] Image TimerBar;
  [SerializeField] Slider TimerSlider;
  [SerializeField] int shieldRecovered, superTimeOutRate;
  [SerializeField] float damage, timerInterval = 30f;
  [SerializeField] ParticleSystem burstEffect;
  int timeOutCount = 1;
  bool superCore = false;
  bool superCorePreheat = false;
  float StartTimer;
  Color originalColor;
  void Awake() {
    StartTimer = Time.time;
    originalColor = TimerBar.color;
  }
  void Update() {
    if (Time.time > timerInterval + StartTimer) {
      StartTimer = Time.time;
      burstEffect.Clear();
      burstEffect.Play();
      setSuperCoreState();
      changeTimerColor();
      Invoke("TimeOut", 0.4f);
      TimerSlider.value = 1f;
      timeOutCount++;
    } else {
      TimerSlider.value = (timerInterval - (Time.time - StartTimer)) / timerInterval;
    }
  }
  void setSuperCoreState() {
    if ((timeOutCount + 1) % superTimeOutRate == 0) {
      superCorePreheat = true;
    } else {
      superCorePreheat = false;
    }
    if (timeOutCount % superTimeOutRate == 0) {
      superCore = true;
    } else {
      superCore = false;
    }
  }
  void TimeOut() {
    int super = 1;
    if (superCore) {
      super = 2;
    }
    replenishShieldsAndUpgradeArmor(super);
    dealDamage((float)super);
    superCore = false;
  }
  void changeTimerColor() {
    if (superCorePreheat) {
      TimerBar.color = new Color(0f, 1f, 1f, 1f);
    } else {
      TimerBar.color = originalColor;
    }
  }
  void replenishShieldsAndUpgradeArmor(int superMultiplier) {
    List<GameObject> enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    enemies.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("TauntEnemy")));
    foreach (GameObject enemy in enemies) {
      EnemyLife script = enemy.transform.root.gameObject.GetComponent<EnemyLife>();
      if (script == null) return;
      script.Shield = (script.Shield + superMultiplier * shieldRecovered) > script.MaxShield ? script.MaxShield : script.Shield + superMultiplier * shieldRecovered;
      script.Armor += superMultiplier;
    }
  }
  void addShield(GameObject enemy, int superMultiplier) {

  }
  void addArmor(GameObject enemy, int superMultiplier) {

  }
  void dealDamage(float super) {
    LifeManager.CurrentLife -= super * damage;
  }



  //timer mechanic
  //
}
