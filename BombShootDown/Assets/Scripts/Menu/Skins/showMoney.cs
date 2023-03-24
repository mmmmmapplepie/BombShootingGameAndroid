using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showMoney : MonoBehaviour {
  [SerializeField]
  Text text;
  void Update() {
    text.text = MoneyManager.money.ToString();
  }
}
