using UnityEngine.UI;
using UnityEngine;

public class CameraAspect : MonoBehaviour {
  void Awake() {
    CamAspect();
  }
  public void CamAspect() {
    float targetaspect = 9f / 16f;
    float windowaspect = (float)Screen.width / (float)Screen.height;
    float scaleheight = windowaspect / targetaspect;
    Camera camera = GetComponent<Camera>();
    if (scaleheight < 1f) {
      Rect rect = camera.rect;
      rect.width = 1f;
      rect.height = scaleheight;
      rect.x = 0;
      rect.y = (1f - scaleheight) / 2f;
      camera.rect = rect;
    } else {
      float scalewidth = 1f / scaleheight;
      Rect rect = camera.rect;
      rect.width = scalewidth;
      rect.height = 1f;
      rect.x = (1f - scalewidth) / 2f;
      rect.y = 0;
      camera.rect = rect;
    }
  }
}
