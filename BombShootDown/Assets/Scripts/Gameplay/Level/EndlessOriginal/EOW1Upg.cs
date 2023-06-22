using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EOW1Upg : MonoBehaviour {
  [SerializeField]
  List<UpgradePick> UpgTemplates;
  [SerializeField]
  GameObject Holder;
  [SerializeField]
  GameObject IconPrefab;
  string[] world1Upg = new string[9] { "MaximumLife", "LifeRecovery", "AmmunitionMax", "AmmunitionRate", "Damage", "Helpers", "BulletSpeed", "ReloadTime", "BombDamage" };
  int tempupgnum = 0;
  void Update() {
    if (UpgradesEquipped.tempUpgHolder.Count != tempupgnum) {
      Render();
      tempupgnum = UpgradesEquipped.tempUpgHolder.Count;
    }
  }
  void OnEnable() {
    Render();
  }
  void Render() {
    EmptyHolder();
    List<string> AvailableUpg = new List<string>();
    foreach (string name in world1Upg) {
      if (!UpgradesEquipped.EquippedUpgrades.Contains(name)) {
        AvailableUpg.Add(name);
      }
    }
    foreach (string upg in AvailableUpg) {
      CreateUpgradeOption(upg);
    }
  }
  void CreateUpgradeOption(string name) {
    RenderOption(name);
    GameObject icon = Instantiate(IconPrefab, Holder.GetComponent<Transform>());
    icon.GetComponent<RenderUpgradeIcon>().RenderUpg();
  }
  void RenderOption(string name) {
    RenderUpgradeIcon script = IconPrefab.GetComponent<RenderUpgradeIcon>();
    script.pick = FindTemplate(name);
    if (UpgradesEquipped.tempUpgHolder.Contains(script.pick.name) || SettingsManager.world[0] < 1) {
      IconPrefab.GetComponent<Button>().interactable = false;
    } else {
      IconPrefab.GetComponent<Button>().interactable = true;
    }
  }
  UpgradePick FindTemplate(string name) {
    foreach (UpgradePick options in UpgTemplates) {
      if (options.name == name) {
        return options;
      }
    }
    return null;
  }
  void EmptyHolder() {
    foreach (Transform child in Holder.GetComponent<Transform>()) {
      Destroy(child.gameObject);
    }
  }
}
