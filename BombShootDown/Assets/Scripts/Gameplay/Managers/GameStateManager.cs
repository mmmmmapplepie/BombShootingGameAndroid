using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
  [SerializeField]
  GameObject GameEndScreen;
  [SerializeField]
  GameObject WinScreen;
  [SerializeField]
  GameObject revivePanel;
  void Update()
  {
    if (LifeManager.CurrentLife <= 0f)
    {
      if (BowManager.ReviveUsable == true && LifeManager.ReviveUsed == false)
      {
        LifeManager.ReviveUsed = true;
        Revive();
      }
      else
      {
        GameEndScreen.SetActive(true);
      }
    }
    if (WaveController.LevelCleared == true && LifeManager.CurrentLife > 0f)
    {
      WinScreen.SetActive(true);
    }
  }
  void Revive()
  {
    revivePanel.SetActive(true);
    StartCoroutine("Reviving");
    Invoke("deactivatePanel", 2f);
  }
  void deactivatePanel()
  {
    revivePanel.SetActive(false);
  }
  IEnumerator Reviving()
  {
    float time = Time.time;
    while (revivePanel.activeSelf == true)
    {
      float ratio = 1 - 0.5f * (Time.time - time);
      LifeManager.CurrentLife = BowManager.MaxLife * BowManager.Revive;
      revivePanel.GetComponent<Image>().color = new Color(1f, 1f, 1f, ratio);
      yield return null;
    }
    revivePanel.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
  }
}
