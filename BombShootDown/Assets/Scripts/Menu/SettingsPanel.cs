using UnityEngine;
using UnityEngine.UI;
public class SettingsPanel : MonoBehaviour
{
  public GameObject panel;
  public Slider volThemeSlider;
  public Slider volEnemySlider;
  public Slider volCannonSlider;
  GameObject Camera;
  void Start()
  {
    Camera = GameObject.FindWithTag("MainCamera");
    if (PlayerPrefs.HasKey("volumeTheme"))
    {
      SettingsManager.volumeTheme = PlayerPrefs.GetFloat("volumeTheme");
      volThemeSlider.value = SettingsManager.volumeTheme;
    }
    if (PlayerPrefs.HasKey("volumeEnemy"))
    {
      SettingsManager.volumeEnemy = PlayerPrefs.GetFloat("volumeEnemy");
      volEnemySlider.value = SettingsManager.volumeEnemy;
    }
    if (PlayerPrefs.HasKey("volumeCannon"))
    {
      SettingsManager.volumeCannon = PlayerPrefs.GetFloat("volumeCannon");
      volCannonSlider.value = SettingsManager.volumeCannon;
    }
    Camera.GetComponent<CameraAspect>().CamAspect();
  }
  public void setSetting(string setting)
  {
    if (setting == "volumeTheme")
    {
      float value = volThemeSlider.value;
      SettingsManager.volumeTheme = value;
      PlayerPrefs.SetFloat("volumeTheme", value);
    }
    if (setting == "volumeEnemy")
    {
      float value = volEnemySlider.value;
      SettingsManager.volumeEnemy = value;
      PlayerPrefs.SetFloat("volumeEnemy", value);
    }
    if (setting == "volumeCannon")
    {
      float value = volCannonSlider.value;
      SettingsManager.volumeCannon = value;
      PlayerPrefs.SetFloat("volumeCannon", value);
    }
  }
  public void MenuOpenSettings()
  {
    panel.SetActive(true);
    GameObject.Find("AudioManagerUI").GetComponent<AudioManagerUI>().PlayAudio("Click");
  }
  public void MenuCloseSettings()
  {
    panel.SetActive(false);
    GameObject.Find("AudioManagerUI").GetComponent<AudioManagerUI>().PlayAudio("Click");
  }
}
