using UnityEngine;
using UnityEngine.UI;

public class MapLevelFocus : MonoBehaviour {
  GameObject panelHolder;
  [SerializeField]
  public Level lvl;
  [SerializeField]
  GameObject Fog;
  RectTransform thisRect;
  Button button;
  void Start() {
    panelHolder = GameObject.FindWithTag("MapInfoPanel");
    button = gameObject.GetComponent<Button>();
    thisRect = gameObject.GetComponent<RectTransform>();
    button.onClick.AddListener(SetFocus);
    button.interactable = false;
    ClearFog();
  }
  void ClearFog() {
    int tempclearworld = SettingsManager.world[0];
    int tempclearlvl = SettingsManager.world[1];
    if (tempclearworld > lvl.stageInWorld[0]) {
      Fog.SetActive(false);
      button.interactable = true;
    } else if (tempclearworld >= lvl.stageInWorld[0] && tempclearlvl >= lvl.stageInWorld[1]) {
      Fog.SetActive(false);
      button.interactable = true;
    }
  }
  void SetFocus() {
    GameObject.FindObjectOfType<AudioManagerUI>().PlayAudio("Click");
    FocusLevelUpdater.cameraFocusObject = gameObject;
    SettingsManager.currentFocusLevelTransform[0] = thisRect.anchoredPosition.x;
    SettingsManager.currentFocusLevelTransform[1] = thisRect.anchoredPosition.y;
    FocusLevelUpdater.focusLevel = lvl;
    SaveSystem.saveSettings();
    panelHolder.GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);
  }
}
