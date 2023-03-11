using UnityEngine;
using UnityEngine.UI;
public class adjustScrollDisplay : MonoBehaviour
{
  RectTransform rect;
  void Awake() {
    rect = GetComponent<RectTransform>();
  }
  void OnEnable() {
    Invoke("SetPosition", 0.001f);
  }
  void SetPosition() {
    float height = rect.rect.height;
    float begh = 165f - height;
    rect.localPosition = new Vector3 (0f, begh, 0f);
  }
}
