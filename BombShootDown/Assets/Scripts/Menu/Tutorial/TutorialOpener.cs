using UnityEngine;
using UnityEngine.UI;

public class TutorialOpener : MonoBehaviour {
  [SerializeField]
  bool ProgressTrigger;
  [SerializeField]
  GameObject guidePanel;
  // Start is called before the first frame update
  void Start() {
    if (ProgressTrigger == true) {
      if (SettingsManager.world[0] == 1 && SettingsManager.world[1] == 1) {
        ActivateGuide();
      }
    }
  }
  public void ActivateGuide() {
    guidePanel.SetActive(true);
  }
  public void DeactivateGuide() {
    guidePanel.SetActive(false);
  }
}
