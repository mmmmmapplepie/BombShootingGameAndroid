using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
public class EndlessGameDefeat : MonoBehaviour {
  [SerializeField]
  GameObject AdButton;
  [SerializeField] List<string> tipsList;
  [SerializeField] Text tipsText, loadPercent, RewardsAndPoints;
  [SerializeField] GameObject loadPanel;
  new AudioManagerUI audio;

  [HideInInspector]
  public float StartTime;
  int Reward;
  string EndlessType;
  void Awake() {
    audio = GameObject.FindObjectOfType<AudioManagerUI>();
  }
  void OnEnable() {
    StartCoroutine(loseAudio());
    Time.timeScale = 0f;
    showRewards();
    EndlessType = SceneManager.GetActiveScene().name;
    BowManager.GunsReady = false;
  }
  void showRewards() {
    float timeElapsed = Time.time - StartTime;
    float multiplier = getMultiplier(timeElapsed);
    float Reward = timeElapsed * multiplier;
    string rewardString = "Your Current rewards are:" + $"\n" + "Bombs: " + Mathf.Round(Reward).ToString()
    + $"\n" + "Score: " + Mathf.Round(Reward * 1.5f).ToString();
    RewardsAndPoints.text = rewardString;
  }
  float getMultiplier(float time) {
    float multiplier;
    if (time < 600f) {
      multiplier = (time * 5f) / 600f;
    } else {
      multiplier = 10f;
    }
    return multiplier;
  }
  IEnumerator loseAudio() {
    yield return new WaitForSecondsRealtime(0.2f);
    audio.PlayAudio("Defeat");
  }
  public void Restart() {
    audio.PlayAudio("Click");
    Time.timeScale = 1f;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
  }
  public void GameModes() {
    audio.PlayAudio("Click");
    SceneManager.LoadScene("GameMode");
  }
  public void ContinueAfterAd() {
    audio.PlayAudio("Click");
    AdButton.GetComponent<Button>().gameObject.SetActive(false);
    LifeManager.CurrentLife = BowManager.MaxLife;
    Time.timeScale = 1f;
    gameObject.SetActive(false);
  }
  void OnDisable() {
    BowManager.GunsReady = true;
  }
  void OnDestroy() {
    MoneyManager.addMoney(Reward);
    if (EndlessType == "EndlessOriginal") {
      SettingsManager.endlessOriginalHS = Mathf.Round(Reward * 1.5f);
    } else {
      SettingsManager.endlessUpgradedHS = Mathf.Round(Reward * 1.5f);
    }
  }
}
