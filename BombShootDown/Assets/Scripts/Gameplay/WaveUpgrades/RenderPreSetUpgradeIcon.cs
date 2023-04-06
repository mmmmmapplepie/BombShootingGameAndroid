using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderPreSetUpgradeIcon : MonoBehaviour {
  [SerializeField] Text text;
  public UpgradePick pick;
  public void RenderUpg() {
    if (pick != null) {
      Image img = GetComponent<Image>();
      img.sprite = pick.sprite;
      text.text = pick.upgradeSlots.ToString();
    }
  }
}
