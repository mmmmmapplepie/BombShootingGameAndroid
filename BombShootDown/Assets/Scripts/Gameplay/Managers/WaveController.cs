using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour {
  [SerializeField]
  Text WaveStartDisplay, waveDisplay, waveShadowDisplay;
  [SerializeField]
  GameObject WaveStartPanel, UpgradesPanel;
  [HideInInspector]
  public Level thisLevelData;
  public static bool LevelCleared = false;
  public static int CurrentWave = 0;//wave should start by incrementing this.
  public static int WavesCleared = 0;
  public static bool startWave = false;
  bool inCue = false;
  void Awake() {
    thisLevelData = GameObject.Find("LevelController").GetComponent<IGetLevelDataInterface>().GetLevelData();
    Time.timeScale = 0f;
  }
  void Update() {
    if (WavesCleared == CurrentWave && LevelCleared == false && inCue == false) {
      inCue = true;
      UpgradesEquipped.LevelSlots = thisLevelData.upgradesPerWave[WavesCleared];
      startWave = false;
      StartCoroutine("UpgradesDelayUnscaled", 1f);
    }
  }
  IEnumerator UpgradesDelayUnscaled(float sec) {
    yield return new WaitForSecondsRealtime(sec);
    while (GamePauseBehaviour.gamePaused == true) {
      yield return null;
    }
    GamePauseBehaviour.Pausable = false;
    CueUpgrades();
  }
  void CueUpgrades() {
    UpgradesPanel.SetActive(true);
  }
  public void CueNextWave() {
    WaveStartPanel.SetActive(true);
    string waveNumStr = (CurrentWave + 1).ToString();
    WaveStartDisplay.text = "Wave " + waveNumStr;
    waveDisplay.text = waveShadowDisplay.text = "Wave : " + waveNumStr;
    StartCoroutine("MoveWaveScreen");
  }
  void makeGunsReady() {
    BowManager.GunsReady = true;
  }
  IEnumerator MoveWaveScreen() {
    Invoke("makeGunsReady", 0.05f);
    CurrentWave++;
    inCue = false;
    Vector2 pos = new Vector2(0f, 0f);
    float starttime = Time.unscaledTime;
    while (Time.unscaledTime - starttime < 2f) {
      float r = WaveStartPanel.GetComponent<Image>().color.r;
      float b = WaveStartPanel.GetComponent<Image>().color.b;
      float g = WaveStartPanel.GetComponent<Image>().color.g;
      WaveStartPanel.GetComponent<Image>().color = new Color(r, g, b, 2f * (2f - (Time.unscaledTime - starttime)) / 6f);
      yield return null;
    }
    float r_ = WaveStartPanel.GetComponent<Image>().color.r;
    float b_ = WaveStartPanel.GetComponent<Image>().color.b;
    float g_ = WaveStartPanel.GetComponent<Image>().color.g;
    WaveStartPanel.GetComponent<Image>().color = new Color(r_, g_, b_, 2f / 3f);
    WaveStartPanel.SetActive(false);
    startWave = true;
    GamePauseBehaviour.Pausable = true;
  }
}
