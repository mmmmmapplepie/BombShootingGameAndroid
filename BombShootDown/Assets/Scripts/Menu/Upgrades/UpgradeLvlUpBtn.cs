using UnityEngine.UI;
using UnityEngine;

public class UpgradeLvlUpBtn : MonoBehaviour
{
  public UpgradesUI.Upgrades upg;
  public GameObject boss;
  private UpgradesUI script;
    void Start()
    {
      script = boss.GetComponent<UpgradesUI>();
      Button button = gameObject.GetComponent<Button>();
      button.onClick.AddListener(raisegrade);
    }
    private void raisegrade() {
      //click noise
      script.raiseUpgrade(upg);
    }
}
