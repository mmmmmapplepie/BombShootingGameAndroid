using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCondition : MonoBehaviour {
  Enemy data;
  [SerializeField]
  GameObject lifeBar;
  [SerializeField]
  GameObject shieldBar;
  [SerializeField]
  GameObject shielded;
  [SerializeField]
  GameObject bossSprite;
  [SerializeField]
  List<Sprite> bossSpritesList;
  [SerializeField]
  GameObject ArmorSprite;
  [SerializeField]
  Text ArmorNumber;
  GameObject statusCanvas;

  float lifeBarScale;
  int bossType;
  float maxlife;
  float maxShield;

  void Awake() {
    statusCanvas = lifeBar.GetComponent<Transform>().parent.parent.gameObject;
    data = gameObject.GetComponent<IDamageable>().data;
    maxlife = data.Life;
    maxShield = data.MaxShield;
    bossType = data.Boss;
    lifeBarScale = lifeBar.transform.localScale.x;
    showBoss();
  }
  void LateUpdate() {
    if (!gameObject.GetComponent<IDamageable>().dead && Time.timeScale != 0f && statusCanvas != null) {
      showArmor();
      showLife();
      showShields();
    }
  }
  void showBoss() {
    if (bossType == 0) {
      bossSprite.gameObject.SetActive(false);
      return;
    } else {
      bossSprite.gameObject.SetActive(true);
      bossSprite.GetComponent<Image>().sprite = bossSpritesList[bossType - 1];
    }
  }
  void showArmor() {
    if (gameObject.GetComponent<IDamageable>().Armor > 0) {
      ArmorSprite.SetActive(true);
      ArmorNumber.gameObject.SetActive(true);
      ArmorNumber.text = gameObject.GetComponent<IDamageable>().Armor.ToString();
    }
    if (gameObject.GetComponent<IDamageable>().Armor <= 0) {
      ArmorNumber.gameObject.SetActive(false);
      ArmorSprite.SetActive(false);
    }
  }
  void showShields() {
    float ratio = (float)gameObject.GetComponent<IDamageable>().Shield / (float)maxShield;
    shieldBar.GetComponent<Slider>().value = ratio;
    if (gameObject.GetComponent<IDamageable>().Shield > 0) {
      shielded.SetActive(true);
    } else {
      shielded.SetActive(false);
    }
  }
  void showLife() {
    float ratio = gameObject.GetComponent<IDamageable>().currentLife / maxlife * lifeBarScale;
    lifeBar.GetComponent<Slider>().value = ratio;
  }
}
