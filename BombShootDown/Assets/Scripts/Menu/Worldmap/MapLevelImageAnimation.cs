using UnityEngine.UI;
using UnityEngine;
public class MapLevelImageAnimation : MonoBehaviour {
  RectTransform rectT;
  Rect rectIni;
  void Start() {
    rectT = GetComponent<RectTransform>();
    rectIni = rectT.rect;
  }
  void Update() {
    Vector2 updatepos = new Vector2(0, Mathf.PingPong(20 * Time.time, 15f) - 57f);
    rectT.anchoredPosition = updatepos;
  }
}
