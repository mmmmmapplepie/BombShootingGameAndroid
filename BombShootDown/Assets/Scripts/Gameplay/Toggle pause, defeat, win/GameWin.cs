using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class GameWin : MonoBehaviour {
  Level data;
  int prize;
  int firstTimePrize;
  int[] thisLevel;
  [SerializeField] GameObject winParticleEffect;
  [SerializeField] Button nextLevelBtn;
  [SerializeField] Text clearRewards, levelNameTxt;
  [SerializeField] List<string> tipsList;
  [SerializeField] Text tipsText, loadPercent;
  [SerializeField] GameObject loadPanel;
  new AudioManagerUI audio;
  GameObject instantiatedEffect;
  void Awake() {
    audio = GameObject.FindObjectOfType<AudioManagerUI>();
  }
  void OnEnable() {
    data = GameObject.FindObjectOfType<LevelSpawner>().level;
    Time.timeScale = 0f;
    StartCoroutine(winAudio());
    instantiatedEffect = Instantiate(winParticleEffect, new Vector3(0f, -11f, 0f), Quaternion.identity);
    thisLevel = data.stageInWorld;
    levelNameTxt.text = data.name;
    nextLevelAvailableCheck();
    prize = data.clearRewards;
    firstTimePrize = data.firstClearRewards;
    clearRewards.text = data.clearRewards.ToString();
    if (SettingsManager.world[0] < thisLevel[0]) {
      newClearLevel();
    } else if (SettingsManager.world[0] == thisLevel[0] && SettingsManager.world[1] <= thisLevel[1]) {
      newClearLevel();
    }
    MoneyManager.addMoney(prize);
  }
  IEnumerator winAudio() {
    yield return new WaitForSecondsRealtime(0.2f);
    audio.PlayAudio("Victory");
  }
  void nextLevelAvailableCheck() {
    nextLevelBtn.interactable = false;
    if (thisLevel[0] == 1 && thisLevel[1] < 25) {
      nextLevelBtn.interactable = true;
    }
    if (thisLevel[0] == 2 && thisLevel[1] < 30) {
      nextLevelBtn.interactable = true;
    }
    if (thisLevel[0] == 3 && thisLevel[1] < 51) {
      nextLevelBtn.interactable = true;
    }
  }
  void newClearLevel() {
    clearRewards.text = clearRewards.text + $"\n{data.firstClearRewards.ToString()}" + " (FirstClear)";
    MoneyManager.addMoney(firstTimePrize);
    SettingsManager.clearStage(thisLevel[0], thisLevel[1]);
  }
  public void NextLevel() {
    Time.timeScale = 1f;
    audio.PlayAudio("Click");
    if (thisLevel[0] == 1 && (thisLevel[1] == 1)) {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    } else {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
      SceneManager.LoadScene("LevelBase", LoadSceneMode.Additive);
    }

  }
  public void WorldMap() {
    audio.PlayAudio("Click");
    StartCoroutine(loadSceneAsync("Worlds"));
    Destroy(instantiatedEffect);
  }
  public void MainMenu() {
    audio.PlayAudio("Click");
    Time.timeScale = 1f;
    SceneManager.LoadScene("MainMenu");
  }
  IEnumerator loadSceneAsync(string sceneName) {
    loadPanel.SetActive(true);
    int totalTips = tipsList.Count;
    int tipIndex = Random.Range(0, totalTips);
    tipsText.text = tipsList[tipIndex];
    AsyncOperation asyncScene = SceneManager.LoadSceneAsync(sceneName);
    asyncScene.allowSceneActivation = false;
    float loadedAmount = 0f;
    while (!asyncScene.isDone) {
      float percent = asyncScene.progress * 100f;
      if (loadedAmount < 100f && percent >= 90f) {
        loadedAmount += 10f;
        loadPercent.text = loadedAmount.ToString() + "%";
        yield return null;
      }
      if (loadedAmount >= 99f && percent >= 90f) {
        asyncScene.allowSceneActivation = true;
        loadPercent.text = "100%";
        yield return null;
      }
    }
  }
}
