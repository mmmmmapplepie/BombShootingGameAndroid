using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedGameTutorial : MonoBehaviour {
  [SerializeField]
  List<GameObject> InfoList;
  int currIndex = 0;
  void Start() {
    StartCoroutine("OpenInfoFirst");
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
    Time.timeScale = 1f;
  }
  void OpenInfo(int index) {
    currIndex++;
    InfoList[index].SetActive(true);
    Time.timeScale = 0f;
  }
  IEnumerator OpenInfoFirst() {
    yield return new WaitForSecondsRealtime(3f);
    OpenInfo(0);
  }
}

