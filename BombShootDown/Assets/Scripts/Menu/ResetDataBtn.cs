using UnityEngine;
using UnityEngine.UI;

public class ResetDataBtn : MonoBehaviour {
  public GameObject confirmationPanel;
  public Button confirm;
  public Button cancel;
  void Start() {
    Button button = gameObject.GetComponent<Button>();
    button.onClick.AddListener(deleteProcedure);
  }
  void deleteProcedure() {
    GameObject.Find("AudioManagerUI").GetComponent<AudioManagerUI>().PlayAudio("Click");
    confirmationPanel.SetActive(true);
  }
  public void confirmDataDelete() {
    GameObject.Find("AudioManagerUI").GetComponent<AudioManagerUI>().PlayAudio("Click");
    UpgradesManager.UpgradeOptions.Clear();
    SettingsManager.unlockedFortressSkin.Clear();
    SettingsManager.unlockedBowSkin.Clear();
    SettingsManager.unlockedBulletSkin.Clear();
    PlayerPrefs.DeleteKey("LatestClaimTime");
    SaveSystem.resetSaveData();
    confirmationPanel.SetActive(false);
    UpgradesManager.loadAllData();
  }
  public void cancelDataDelete() {
    GameObject.Find("AudioManagerUI").GetComponent<AudioManagerUI>().PlayAudio("Back");
    confirmationPanel.SetActive(false);
  }
}
