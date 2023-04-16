using UnityEngine;
using UnityEngine.UI;

public class RenderUpgradeIcon : MonoBehaviour {
  [SerializeField] Text text;
  public UpgradePick pick;
  new AudioManagerUI audio;
  void Awake() {
    audio = GameObject.FindObjectOfType<AudioManagerUI>();
  }
  public void RenderUpg() {
    if (pick != null) {
      Image img = GetComponent<Image>();
      img.sprite = pick.sprite;
      text.text = pick.upgradeSlots.ToString();
    }
  }
  public void AddUpgToDisplay() {
    audio.PlayAudio("UpLevel");
    if (pick.upgradeSlots <= UpgradesEquipped.AvailableSlots) {
      UpgradesEquipped.tempUpgHolder.Add(pick.name);
    }
  }
}
