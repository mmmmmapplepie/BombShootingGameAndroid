using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour {
  //back to Main Menu
  public void BackMM() {
    GameObject.Find("AudioManagerUI").GetComponent<AudioManagerUI>().PlayAudio("Back");
    SceneManager.LoadScene("MainMenu");
  }
  public void BackGM() {
    GameObject.Find("AudioManagerUI").GetComponent<AudioManagerUI>().PlayAudio("Back");
    SceneManager.LoadScene("GameMode");
  }
  public void BackW(string world) {
    GameObject.Find("AudioManagerUI").GetComponent<AudioManagerUI>().PlayAudio("Back");
    SceneManager.LoadScene(world);
  }
}
