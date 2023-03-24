using UnityEngine;
using UnityEngine.UI;

public class adjustScroll : MonoBehaviour {
  RectTransform rect;
  void Awake() {
    rect = GetComponent<RectTransform>();
  }
  void OnEnable() {
    Invoke("SetPosition", 0.001f);
  }
  void SetPosition() {
    float width = rect.rect.width;
    rect.localPosition = new Vector3(width, 0f, 0f);
  }
}
