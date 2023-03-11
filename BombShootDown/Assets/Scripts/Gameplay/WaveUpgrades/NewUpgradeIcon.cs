using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewUpgradeIcon : MonoBehaviour
{
  public UpgradePick pick;
  public void RenderUpg() {
    if (pick != null) {
      Image img = GetComponent<Image>();
      img.sprite = pick.sprite;
    }
  }
  public void UnSelect() {
    UpgradesEquipped.tempUpgHolder.Remove(pick.name);
  }
}
