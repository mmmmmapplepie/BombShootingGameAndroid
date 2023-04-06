using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewUpgradeIcon : MonoBehaviour {
  [SerializeField] Text text;
  public UpgradePick pick;
  public void RenderUpg() {
    if (pick != null) {
      Image img = GetComponent<Image>();
      img.sprite = pick.sprite;
      text.text = pick.upgradeSlots.ToString();
    }
  }
  public void UnSelect() {
    UpgradesEquipped.tempUpgHolder.Remove(pick.name);
  }
}
