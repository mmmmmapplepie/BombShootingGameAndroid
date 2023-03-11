using UnityEngine.UI;
using UnityEngine;

public class UpgradeLvlDownBtn : MonoBehaviour
{
  public UpgradesUI.Upgrades upg;
  public GameObject boss;
  private UpgradesUI script;
    void Start()
    {
      script = boss.GetComponent<UpgradesUI>();
      Button button = gameObject.GetComponent<Button>();
      button.onClick.AddListener(donwgrade);
    }
    private void donwgrade() {
      //click sound
      script.lowerUpgrade(upg);
    }
}
