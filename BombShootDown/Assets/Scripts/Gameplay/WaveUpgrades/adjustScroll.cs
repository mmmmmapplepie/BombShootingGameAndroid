using UnityEngine;
using UnityEngine.UI;

public class adjustScroll : MonoBehaviour
{
  RectTransform rect;
  void Awake() {
    rect = GetComponent<RectTransform>();
  }
  void OnEnable() {
    Invoke("SetPosition", 0.001f);
  }
  void SetPosition() {
    rect.localPosition = new Vector3 (-300f, 0f, 0f);
  }
}
