using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GamePauseBehaviour : MonoBehaviour {
  [SerializeField]
  GameObject PauseToggle;
  [SerializeField] Text tipsText, loadPercent;
  [SerializeField] GameObject loadPanel;
  [SerializeField] List<string> tipsList;
  Button button;
  public static bool gamePaused = false;
  public static bool Pausable = true;
  bool pausableCheck = true;
  new AudioManagerUI audio;
  void Awake() {
    audio = GameObject.FindObjectOfType<AudioManagerUI>();
    Pausable = true;
    gamePaused = false;
  }
  void Start() {
    button = gameObject.GetComponent<Button>();
    button.onClick.AddListener(Pause);
  }
  void Update() {
    if (pausableCheck != Pausable) {
      pausableCheck = Pausable;
      if (pausableCheck) {
        GetComponent<Button>().interactable = true;
      } else {
        GetComponent<Button>().interactable = false;
      }
    }
  }

  // Update is called once per frame
  void Pause() {
    audio.PlayAudio("Click");
    if (BowManager.UsingCooldown == true || Pausable == false) {
      return;
    }
    Time.timeScale = 0f;
    gamePaused = true;
    PauseToggle.SetActive(true);
  }
  public void Restart() {
    audio.PlayAudio("Click");
    gamePaused = false;
    Time.timeScale = 1f;
    PauseToggle.SetActive(false);
    bool levelBaseUsed = false;
    if (SceneManager.GetSceneByName("LevelBase").isLoaded) {
      levelBaseUsed = true;
    }
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    if (levelBaseUsed) {
      SceneManager.LoadScene("LevelBase", LoadSceneMode.Additive);
    }
  }
  public void Continue() {
    audio.PlayAudio("Click");
    gamePaused = false;
    Time.timeScale = 1f;
    PauseToggle.SetActive(false);
  }
  public void WorldMap() {
    audio.PlayAudio("Click");
    gamePaused = false;
    StartCoroutine(loadSceneAsync("Worlds"));
  }
  IEnumerator loadSceneAsync(string sceneName) {
    loadPanel.SetActive(true);
    int totalTips = tipsList.Count;
    int tipIndex = Random.Range(0, totalTips);
    tipsText.text = tipsList[tipIndex];
    AsyncOperation asyncScene = SceneManager.LoadSceneAsync(sceneName);
    asyncScene.allowSceneActivation = false;
    float loadedAmount = 0f;
    StartCoroutine(worldTimeReturn(asyncScene));
    while (!asyncScene.isDone) {
      float percent = asyncScene.progress * 100f;
      if (loadedAmount < 100f && percent >= 90f) {
        loadedAmount += 10f;
        loadPercent.text = loadedAmount.ToString() + "%";
        yield return null;
      }
      if (loadedAmount >= 99f && percent >= 90f) {
        asyncScene.allowSceneActivation = true;
        print(asyncScene.progress);
        loadPercent.text = "100%";
        yield return null;
      }
    }

    Time.timeScale = 1f;
  }
  IEnumerator worldTimeReturn(AsyncOperation sceneprog) {
    while (false) {
      if (sceneprog.isDone) {
        print("AsyncDone");
        yield break;
      }
    }
  }
}
