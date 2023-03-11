using UnityEngine;
using UnityEngine.UI;

public class moneyUI : MonoBehaviour
{
  public Text moneytxt;
  void Start()
  {
    if (PlayerPrefs.HasKey("money")) {
      MoneyManager.money = PlayerPrefs.GetInt("money");
    }
    changeCurrencyUI();
  }
  public void changeCurrencyUI() {
    float value = MoneyManager.money;
    if (value < 1000001 ) {
      moneytxt.text = value.ToString("F0");
    } else {
      float logval = Mathf.Floor(Mathf.Log10(value));
      float temp = value/(Mathf.Pow(10f, logval));
      string txttemp = temp.ToString("F2") + "*e" + logval;
      moneytxt.text = txttemp;
    }
  }

}
