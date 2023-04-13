using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ChosenUpgMaxed : MonoBehaviour {
  [SerializeField]
  List<UpgradePick> UpgTemplates;
  WaveController waveController;
  [SerializeField]
  Transform ChosenUpgContainer;
  [SerializeField]
  GameObject UpgIconPrefab;
  [SerializeField]
  GameObject NewUpgIconPrefab;
  [SerializeField]
  Text AvailableSlotText;
  [SerializeField]
  GameObject UpgradesPanel;
  [SerializeField]
  UpgradesMaxed UpgradesScript;
  [SerializeField]
  GameObject cooldowns;
  [SerializeField]
  GameObject AmmoLife;
  int ItemsInList;
  int tempupgnum = -1;
  void Awake() {
    waveController = GameObject.FindObjectOfType<WaveController>();
  }
  void Update() {
    if (UpgradesEquipped.tempUpgHolder.Count != tempupgnum) {
      RenderAll();
      tempupgnum = UpgradesEquipped.tempUpgHolder.Count;
    }
  }
  void OnEnable() {
    RenderAll();
    Time.timeScale = 0f;
    BowManager.GunsReady = false;
  }
  void RenderAll() {
    EmptyHolder();
    RenderPresetUpg();
    RenderAllOptions();
    StartCoroutine(changeSlots());
  }
  IEnumerator changeSlots() {
    yield return null;
    ChangeAvailableSlots();
  }
  void RenderPresetUpg() {
    foreach (string upg in UpgradesEquipped.EquippedUpgrades) {
      GameObject icon = Instantiate(UpgIconPrefab, ChosenUpgContainer);
      icon.GetComponent<RenderPreSetUpgradeIcon>().pick = FindTemplate(upg);
      icon.GetComponent<RenderPreSetUpgradeIcon>().RenderUpg();
    }
  }
  void AppendUpgradesToEquippedUpgrades() {
    foreach (string name in UpgradesEquipped.tempUpgHolder) {
      UpgradesEquipped.EquippedUpgrades.Add(name);
    }
    UpgradesEquipped.tempUpgHolder.Clear();
  }
  UpgradePick FindTemplate(string name) {
    foreach (UpgradePick options in UpgTemplates) {
      if (options.name == name) return options;
    }
    return null;
  }
  void RenderAllOptions() {
    foreach (string upg in UpgradesEquipped.tempUpgHolder) {
      CreateUpgradeOption(upg);
    }
  }
  void CreateUpgradeOption(string name) {
    GameObject icon = Instantiate(NewUpgIconPrefab, ChosenUpgContainer);
    icon.GetComponent<NewUpgradeIcon>().pick = FindTemplate(name);
    icon.GetComponent<NewUpgradeIcon>().RenderUpg();
  }
  void ChangeAvailableSlots() {
    UpgradesEquipped.AvailableSlots = Mathf.Min(UpgradesEquipped.LevelSlots, UpgradesEquipped.UpgradedSlots);
    foreach (Transform child in ChosenUpgContainer) {
      if (child.GetComponent<NewUpgradeIcon>() != null) {
        int weight = child.GetComponent<NewUpgradeIcon>().pick.upgradeSlots;
        UpgradesEquipped.AvailableSlots -= weight;
      }

      if (child.GetComponent<RenderPreSetUpgradeIcon>() != null) {
        int weight = child.GetComponent<RenderPreSetUpgradeIcon>().pick.upgradeSlots;
        UpgradesEquipped.AvailableSlots -= weight;
      }
    }
    AvailableSlotText.text = UpgradesEquipped.AvailableSlots.ToString();
  }
  void EmptyHolder() {
    for (int i = 0; i < ChosenUpgContainer.childCount; i++) {
      Destroy(ChosenUpgContainer.GetChild(i).gameObject);
    }
  }
  public void DisableUpgrades() {
    AppendUpgradesToEquippedUpgrades();
    UpgradesScript.setUpgrades();
    cooldowns.GetComponent<Bomb>().checkUpgradesForBombDamageEquipped();
    cooldowns.GetComponent<Laser>().checkUpgradesForLaserEquipped();
    cooldowns.GetComponent<Nuke>().checkUpgradesForNukeEquipped();
    AmmoLife.GetComponent<AmmoLifeReviveUI>().checkUpgradesForReviveEquipped();
    UpgradesScript.SpeedUpTimeAfterUpgrades();
    waveController.CueNextWave();
    UpgradesPanel.SetActive(false);
  }
}
