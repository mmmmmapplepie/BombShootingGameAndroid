using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuCannonMove : MonoBehaviour
{
  public GameObject MenuAimLine;
  string currentClicked1;
  string newscene;
  AudioManagerUI UIaudio;
  AudioManagerBGM BGM;
  void Start()
  {
    UIaudio = GameObject.Find("AudioManagerUI").GetComponent<AudioManagerUI>();
    BGM = GameObject.Find("AudioManagerBGM").GetComponent<AudioManagerBGM>();
    if (BGM.currentBGM.name != "MenuTheme")
    {
      BGM.ChangeBGM("MenuTheme");
    }
  }
  public void checkClicked(Button button)
  {
    string btn = button.name;
    if (btn == currentClicked1)
    {
      moveCannonPointer(btn);
      UIaudio.PlayAudio("Click");
      moveScene(btn);
    }
    else
    {
      UIaudio.PlayAudio("Click");
      moveCannonPointer(btn);
      currentClicked1 = btn;
    }
  }

  void moveCannonPointer(string clickedbutton)
  {
    LineRenderer LR = MenuAimLine.GetComponent<LineRenderer>();
    Transform transform = gameObject.GetComponent<Transform>();
    if (clickedbutton == "PlayBtn")
    {
      LR.SetPosition(1, new Vector3(-4.17202f, 0f, 0f));
      transform.rotation = Quaternion.Euler(0, 0, -5f);
    }
    if (clickedbutton == "UpgradesBtn")
    {
      LR.SetPosition(1, new Vector3(-1.8f, -3f, 0f));
      transform.rotation = Quaternion.Euler(0, 0, -26.21138f);
    }
    if (clickedbutton == "ShopBtn")
    {
      LR.SetPosition(1, new Vector3(0f, -6f, 0f));
      transform.rotation = Quaternion.Euler(0, 0, -55.00798f);
    }
  }


  void moveScene(string btn)
  {
    if (btn == "PlayBtn")
    {
      SceneManager.LoadScene("GameMode");
    }
    if (btn == "UpgradesBtn")
    {
      SceneManager.LoadScene("Upgrades");
    }
    if (btn == "ShopBtn")
    {
      SceneManager.LoadScene("Shop");
    }
  }
}
