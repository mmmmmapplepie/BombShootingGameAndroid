using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicGameTutorial : MonoBehaviour {
  [SerializeField]
  List<GameObject> InfoList;
  int currIndex = 0;
  void Start() {
    Invoke("OpenInfoFirst", 3.7f);
  }
  public void runCoroutineForNext(GameObject closeGO) {
    StartCoroutine(closeThenOpenDelay(currIndex, closeGO));
  }
  public IEnumerator closeThenOpenDelay(int index, GameObject closeGO) {
    CloseInfo(closeGO);
    OpenInfo(index);
    yield return null;
  }
  public void CloseInfo(GameObject go) {
    go.SetActive(false);
    BowManager.GunsReady = true;
    Time.timeScale = 1f;
  }
  void OpenInfo(int index) {
    currIndex++;
    InfoList[index].SetActive(true);
    BowManager.GunsReady = false;
    Time.timeScale = 0f;
  }
  void OpenInfoFirst() {
    OpenInfo(0);
  }
}
