using UnityEngine;
using UnityEngine.UI;

public class ResetDataBtn : MonoBehaviour
{
  public GameObject confirmationPanel;
  public Button confirm;
  public Button cancel;
    // Start is called before the first frame update
    void Start()
    {
      Button button = gameObject.GetComponent<Button>();
      button.onClick.AddListener(deleteProcedure);
    }
    void deleteProcedure() {
      confirmationPanel.SetActive(true);
    }
    public void confirmDataDelete() {
      UpgradesManager.UpgradeOptions.Clear();
      SaveSystem.resetSaveData();
      confirmationPanel.SetActive(false);
      // UpgradesManager UM = new UpgradesManager();
      // UM.loadAllData();
      UpgradesManager.loadAllData();
    }
    public void cancelDataDelete() {
      confirmationPanel.SetActive(false);
    }
}
