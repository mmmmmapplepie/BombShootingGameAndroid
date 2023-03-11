using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
  public static GameObject gameManager;
  //should not be destroyed on scene changes
  void Awake() {
    #region singleton
    if (gameManager == null) {
      gameManager = gameObject;
      DontDestroyOnLoad(gameManager);
    } else {
      Destroy(gameObject);
    }
    #endregion

  }
  void Start() {
    UpgradesManager.loadAllData();
  }



  void addClearedStage() {

  }
}
