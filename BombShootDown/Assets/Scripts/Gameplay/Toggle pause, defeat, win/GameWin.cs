using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour {
  Level data;
  int prize;
  int firstTimePrize;
  int[] thisLevel;
  void Awake() {
    data = GameObject.FindObjectOfType<LevelSpawner>().level;
  }

  void OnEnable() {
    Time.timeScale = 0f;
    thisLevel = data.stageInWorld;
    prize = data.clearRewards;
    firstTimePrize = data.firstClearRewards;
    if (SettingsManager.world[0] < thisLevel[0]) {
      MoneyManager.addMoney(firstTimePrize);
    } else if (SettingsManager.world[0] == thisLevel[0] && SettingsManager.world[1] < thisLevel[1]) {
      MoneyManager.addMoney(firstTimePrize);
    }
    MoneyManager.addMoney(prize);
    SettingsManager.clearStage(thisLevel[0], thisLevel[1]);
  }
  public void NextLevel() {
    Time.timeScale = 1f;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
