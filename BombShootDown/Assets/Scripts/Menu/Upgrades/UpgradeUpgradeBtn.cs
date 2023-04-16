using UnityEngine.UI;
using UnityEngine;

public class UpgradeUpgradeBtn : MonoBehaviour {
  public Button btn;
  public UpgradesUI.Upgrades upg;
  public GameObject boss;
  private UpgradesUI script;
  void Start() {
    script = boss.GetComponent<UpgradesUI>();
    Button button = gameObject.GetComponent<Button>();
    button.onClick.AddListener(upgrade);
    checkDisabled();
  }
  private void checkDisabled() {
    //world block
    if ((upg == UpgradesUI.Upgrades.Revive || upg == UpgradesUI.Upgrades.ArmorPierce || upg == UpgradesUI.Upgrades.HitsPerHit || upg == UpgradesUI.Upgrades.Pierce || upg == UpgradesUI.Upgrades.AoeHit || upg == UpgradesUI.Upgrades.Laser) && SettingsManager.world[0] < 2) {
      btn.interactable = false;
    }
    if ((upg == UpgradesUI.Upgrades.Nuke || upg == UpgradesUI.Upgrades.ChainExplosion || upg == UpgradesUI.Upgrades.PullEnemies || upg == UpgradesUI.Upgrades.DoubleGun) && SettingsManager.world[0] < 3) {
      btn.interactable = false;
    }
    //max upgrade
    string upgstring = upg.ToString();
    int[] upgrade = UpgradesManager.returnDictionaryValue(upgstring);
    if (upg == UpgradesUI.Upgrades.DoubleGun) {
      if (upgrade[1] == 1) {
        btn.interactable = false;
      }
    } else {
      if (upgrade[1] == 10) {
        btn.interactable = false;
      }
    }
  }
  private void upgrade() {
    script.upgradeUpgrade(upg);
    checkDisabled();
  }
}
