using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GamePauseBehaviour : MonoBehaviour
{
  [SerializeField]
  GameObject PauseToggle;
  Button button;
  void Start()
  {
    button = gameObject.GetComponent<Button>();
    button.onClick.AddListener(Pause);
  }

  // Update is called once per frame
  void Pause()
  {
    if (BowManager.UsingCooldown == true) {
      return;
    }
    Time.timeScale = 0f;
    PauseToggle.SetActive(true);
  }
  public void Restart() {
    Time.timeScale = 1f;
    PauseToggle.SetActive(false);
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
  public void Continue() {
    Time.timeScale = 1f;
    PauseToggle.SetActive(false);
  }
  public void WorldMap() {
    Time.timeScale = 1f;
    SceneManager.LoadScene("Worlds");
  }



}
