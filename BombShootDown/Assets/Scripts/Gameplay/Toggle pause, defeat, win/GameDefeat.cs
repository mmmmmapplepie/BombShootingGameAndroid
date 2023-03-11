using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameDefeat : MonoBehaviour
{
  [SerializeField]
  GameObject AdButton;

  void OnEnable() {
    Time.timeScale = 0f;
  }
  public void Restart() {
    Time.timeScale = 1f;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
  public void WorldMap() {
    Time.timeScale = 1f;
    SceneManager.LoadScene("Worlds");
  }
  public void ContinueAfterAd() {
    AdButton.GetComponent<Button>().interactable = false;


    LifeManager.CurrentLife = BowManager.MaxLife;
    Time.timeScale = 1f;
    gameObject.SetActive(false);
  }
}
