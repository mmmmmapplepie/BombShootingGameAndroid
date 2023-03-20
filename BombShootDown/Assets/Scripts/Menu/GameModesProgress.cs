using UnityEngine;
using UnityEngine.UI;

public class GameModesProgress : MonoBehaviour {
  public GameObject[] worlds;
  public GameObject secretWorld;
  public Text originalHS;
  public Text upgradedHS;
  void Start() {
    for (int i = 0; i < SettingsManager.world[0]; i++) {
      worlds[i].SetActive(true);
    }
    int[] last = new int[2] { 3, 51 };
    // you have to get high scores for the two modes.
    if (SettingsManager.world == last && SettingsManager.endlessOriginalHS > 300f && SettingsManager.endlessUpgradedHS > 300f) {
      secretWorld.SetActive(true);
    }
    originalHS.text = SettingsManager.endlessOriginalHS.ToString();
    upgradedHS.text = SettingsManager.endlessUpgradedHS.ToString();
  }
}
