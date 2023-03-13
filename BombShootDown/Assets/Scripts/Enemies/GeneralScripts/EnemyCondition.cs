using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCondition : MonoBehaviour
{
  Enemy data;

  [SerializeField]
  GameObject lifeBar;
  [SerializeField]
  GameObject shieldBar;
  [SerializeField]
  GameObject bossSprite;
  [SerializeField]
  List<Sprite> bossSpritesList;
  [SerializeField]
  GameObject ArmorSprite;
  [SerializeField]
  Text ArmorNumber;

  float lifeBarScale;
  int bossType;
  float maxlife;
  float maxShield;

  void Awake()
  {
    data = gameObject.GetComponent<EnemyLife>().data;
    maxlife = data.Life;
    maxShield = data.MaxShield;
    bossType = data.Boss;
    lifeBarScale = lifeBar.transform.localScale.x;
    showBoss();
  }
  void Update()
  {
    if (!gameObject.GetComponent<EnemyLife>().dead)
    {
      showShields();
      showLife();
      showArmor();
    }

  }
  void showBoss()
  {
    if (bossType == 0)
    {
      bossSprite.gameObject.SetActive(false);
      return;
    }
    else
    {
      bossSprite.gameObject.SetActive(true);
      bossSprite.GetComponent<SpriteRenderer>().sprite = bossSpritesList[bossType - 1];
    }
  }
  void showArmor()
  {
    if (gameObject.GetComponent<EnemyLife>().Armor > 0)
    {
      ArmorSprite.SetActive(true);
      ArmorNumber.gameObject.SetActive(true);
      ArmorNumber.text = gameObject.GetComponent<EnemyLife>().Armor.ToString();
    }
    if (gameObject.GetComponent<EnemyLife>().Armor <= 0)
    {
      ArmorNumber.gameObject.SetActive(false);
      ArmorSprite.SetActive(false);
    }
  }
  void showShields()
  {
    float ratio = (float)gameObject.GetComponent<EnemyLife>().Shield / (float)maxShield;
    shieldBar.GetComponent<Slider>().value = ratio;
  }
  void showLife()
  {
    float ratio = gameObject.GetComponent<EnemyLife>().currentLife / maxlife * lifeBarScale;
    lifeBar.GetComponent<Slider>().value = ratio;
  }
}
