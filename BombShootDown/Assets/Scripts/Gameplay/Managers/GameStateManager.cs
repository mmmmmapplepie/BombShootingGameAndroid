using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    while (revivePanel.activeSelf == true)
    {
      LifeManager.CurrentLife = BowManager.MaxLife * BowManager.Revive;
      yield return null;
    }
  }
}
