using UnityEngine;

public class ShopTutorial : MonoBehaviour {
  [SerializeField] GameObject tutorialPanel;
  public void OpenTutorial() {
    tutorialPanel.SetActive(true);
  }
  public void CloseTutorial() {
    tutorialPanel.SetActive(false);
  }
}
