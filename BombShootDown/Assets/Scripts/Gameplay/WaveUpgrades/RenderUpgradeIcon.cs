using UnityEngine;
using UnityEngine.UI;

public class RenderUpgradeIcon : MonoBehaviour {
  [SerializeField] Text text;
  public UpgradePick pick;
  public void RenderUpg() {
    if (pick != null) {
      Image img = GetComponent<Image>();
      img.sprite = pick.sprite;
      text.text = pick.upgradeSlots.ToString();
    }
  }
  public void AddUpgToDisplay() {
    if (pick.upgradeSlots <= UpgradesEquipped.AvailableSlots) {
      UpgradesEquipped.tempUpgHolder.Add(pick.name);
    }
  }
}
