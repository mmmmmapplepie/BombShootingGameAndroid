using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour {
  Level data;
  int prize;
  int firstTimePrize;
  int[] thisLevel;
  [SerializeField] GameObject winParticleEffect;
  [SerializeField] Button nextLevelBtn;
  [SerializeField] Text clearRewards, levelNameTxt;
  new AudioManagerUI audio;
  void OnEnable() {
    data = GameObject.FindObjectOfType<LevelSpawner>().level;
    audio = GameObject.FindObjectOfType<AudioManagerUI>();
    Time.timeScale = 0f;
    Instantiate(winParticleEffect, new Vector3(0f, -11f, 0f), Quaternion.identity);

    // audio.PlayAudio("Win");
    thisLevel = data.stageInWorld;
    levelNameTxt.text = data.name;
    nextLevelAvailableCheck();
    prize = data.clearRewards;
    firstTimePrize = data.firstClearRewards;
    clearRewards.text = data.clearRewards.ToString();
    if (SettingsManager.world[0] < thisLevel[0]) {
      newClearLevel();
    } else if (SettingsManager.world[0] == thisLevel[0] && SettingsManager.world[1] < thisLevel[1]) {
      newClearLevel();
    }
    MoneyManager.addMoney(prize);
  }
  void nextLevelAvailableCheck() {
    if (thisLevel[0] == 1 && thisLevel[1] == 25) {
      nextLevelBtn.interactable = false;
    }
    if (thisLevel[0] == 2 && thisLevel[1] == 30) {
      nextLevelBtn.interactable = false;
    }
    if (thisLevel[0] == 3 && thisLevel[1] == 51) {
      nextLevelBtn.interactable = false;
    }
  }
  void newClearLevel() {
    clearRewards.text = clearRewards.text + $"\n{data.firstClearRewards.ToString()}" + " (FirstClear)";
    MoneyManager.addMoney(firstTimePrize);
    SettingsManager.clearStage(thisLevel[0], thisLevel[1]);
  }
  public void NextLevel() {
    Time.timeScale = 1f;
    if (thisLevel[0] == 1 && (thisLevel[1] == 1 || thisLevel[1] == 2)) {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    } else {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
      SceneManager.LoadScene("LevelBase", LoadSceneMode.Additive);
    }

  }
  public void WorldMap() {
    Time.timeScale = 1f;
    SceneManager.LoadScene("Worlds");
  }
  public void MainMenu() {
    Time.timeScale = 1f;
    SceneManager.LoadScene("MainMenu");
  }
}
