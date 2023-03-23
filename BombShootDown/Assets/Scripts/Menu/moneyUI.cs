using UnityEngine;
using UnityEngine.UI;

public class moneyUI : MonoBehaviour {
  public Text moneytxt;
  void Start() {
    if (PlayerPrefs.HasKey("money")) {
      MoneyManager.money = PlayerPrefs.GetInt("money");
    }
    changeCurrencyUI();
  }
  public void changeCurrencyUI() {
    float value = MoneyManager.money;
    moneytxt.text = value.ToString();
  }
}
