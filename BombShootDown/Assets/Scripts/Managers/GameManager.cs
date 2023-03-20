using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameManager : MonoBehaviour {
  public static GameObject gameManager;
  void Awake() {
    if (gameManager == null) {
      gameManager = gameObject;
      UpgradesManager.loadAllData();
      DontDestroyOnLoad(gameManager);
    } else {
      Destroy(gameObject);
    }
  }
}
