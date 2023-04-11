using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameModeCannon : MonoBehaviour {
  [SerializeField] GameObject loadPanel;
  [SerializeField] Text loadPercent, tipsText;
  [SerializeField] List<string> tipsList;
  public GameObject MenuAimLine;
  string currentClicked1;
  string newscene;
  AudioManagerUI UIaudio;
  int totalTips;
  void Awake() {
    totalTips = tipsList.Count;
    UIaudio = GameObject.Find("AudioManagerUI").GetComponent<AudioManagerUI>();
    if (GameObject.Find("AudioManagerBGM").GetComponent<AudioManagerBGM>().currentBGM.name != "MenuTheme") {
      GameObject.Find("AudioManagerBGM").GetComponent<AudioManagerBGM>().ChangeBGM("MenuTheme");
    }
  }
  public void checkClicked(Button button) {
    string btn = button.name;
    UIaudio.PlayAudio("Click");
    if (btn == currentClicked1) {
      moveCannonPointer(btn);
      moveScene(btn);
    } else {
      moveCannonPointer(btn);
      currentClicked1 = btn;
    }
  }

  void moveCannonPointer(string clickedbutton) {
    LineRenderer LR = MenuAimLine.GetComponent<LineRenderer>();
    Transform transform = gameObject.GetComponent<Transform>();
    if (clickedbutton == "StoryBtn") {
      LR.SetPosition(1, new Vector3(-4.55f, 3f, 0f));
      transform.rotation = Quaternion.Euler(0, 0, -1.9f);
    }
    if (clickedbutton == "EndlessUpgraded") {
      LR.SetPosition(1, new Vector3(-2f, 0f, 0f));
      transform.rotation = Quaternion.Euler(0, 0, -21.80141f);
    }
    if (clickedbutton == "EndlessOriginal") {
      LR.SetPosition(1, new Vector3(3f, -4f, 0f));
      transform.rotation = Quaternion.Euler(0, 0, -51.84277f);
    }
  }
  void moveScene(string btn) {
    if (btn == "StoryBtn") {
      StartCoroutine(loadSceneAsync("Worlds"));
    }
    if (btn == "EndlessUpgraded") {
      StartCoroutine(loadSceneAsync("EndlessUpgraded"));
    }
    if (btn == "EndlessOriginal") {
      StartCoroutine(loadSceneAsync("EndlessOriginal"));
    }
  }
  IEnumerator loadSceneAsync(string sceneName) {
    loadPanel.SetActive(true);
    int tipIndex = Random.Range(0, totalTips);
    tipsText.text = tipsList[tipIndex];
    AsyncOperation asyncScene = SceneManager.LoadSceneAsync(sceneName);
    asyncScene.allowSceneActivation = false;
    float loadedAmount = 0f;
    while (!asyncScene.isDone) {
      float percent = asyncScene.progress * 100f;
      if (loadedAmount < 100f && percent >= 90f) {
        loadedAmount += 1f;
        loadPercent.text = loadedAmount.ToString() + "%";
        yield return new WaitForSeconds(0.04f);
      }
      if (loadedAmount >= 99f && percent >= 90f) {
        asyncScene.allowSceneActivation = true;
        loadPercent.text = "100%";
        yield return null;
      }
    }
  }
}

