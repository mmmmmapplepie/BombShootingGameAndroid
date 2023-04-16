using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewUpgradeIcon : MonoBehaviour {
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
  public void UnSelect() {
    audio.PlayAudio("DownLevel");
    UpgradesEquipped.tempUpgHolder.Remove(pick.name);
  }
}
