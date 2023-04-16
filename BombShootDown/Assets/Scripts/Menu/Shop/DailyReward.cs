using UnityEngine;
using System;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour {
  [SerializeField] GameObject RewardButton, RemainingTimer;
  DateTime _LatestClaimDateTime;
  public DateTime LatestClaimDateTime {
    get { return _LatestClaimDateTime; }
    set {
      _LatestClaimDateTime = value;
      PlayerPrefs.SetString("LatestClaimTime", value.ToString());
    }
  }
  DateTime currentTime;
  bool rewardAvailable = false;
  void Awake() {
    checkForLatestClaimTime();
    checkNewRewardAvailable();
    currentTime = DateTime.Now;
  }
  void Update() {
    currentTime = DateTime.Now;
    checkNewRewardAvailable();
  }
  void checkForLatestClaimTime() {
    if (PlayerPrefs.HasKey("LatestClaimTime")) {
      LatestClaimDateTime = DateTime.Parse(PlayerPrefs.GetString("LatestClaimTime"));
    } else {
      rewardAvailable = true;
      LatestClaimDateTime = DateTime.Now;
      print("noClaims");
    }
  }
  void checkNewRewardAvailable() {
    TimeSpan timeElapsedSinceClaim = currentTime - LatestClaimDateTime;
    double totalHours = timeElapsedSinceClaim.TotalHours;
    if (totalHours > 24f || rewardAvailable) {
      rewardAvailable = true;
      RewardButton.SetActive(true);
      RemainingTimer.transform.parent.gameObject.SetActive(false);
    } else {
      RewardButton.SetActive(false);
      RemainingTimer.transform.parent.gameObject.SetActive(true);
      updateTimer(timeElapsedSinceClaim);
    }
  }
  void updateTimer(TimeSpan timeDiff) {
    Text textBox = RemainingTimer.GetComponent<Text>();
    double hours = timeDiff.Hours;
    double minutes = timeDiff.Minutes;
    textBox.text = $"{hours.ToString("00")}:{minutes.ToString("00")}";
  }
  public void claimRewards() {
    LatestClaimDateTime = DateTime.Now;
    rewardAvailable = false;
    MoneyManager.addMoney(3000);
    RewardButton.SetActive(false);
    RemainingTimer.transform.parent.gameObject.SetActive(true);
  }
}
