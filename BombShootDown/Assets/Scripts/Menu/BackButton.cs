using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
  //back to Main Menu
  public void BackMM() {
    //click sound
    SceneManager.LoadScene("MainMenu");
  }
  public void BackGM() {
    //click sound
    SceneManager.LoadScene("GameMode");
  }
  public void BackW(string world) {
    //click sound
    SceneManager.LoadScene(world);
  }
}
