using UnityEngine;
using UnityEngine.UI;

public class DirectionCursor : MonoBehaviour
{
  RectTransform RT;
  Image img;
  Color color;
  GameObject clickedObject;
  Vector2 clickedObjectPosition;
  float Distance;
   //inside camera view is max/min heigth (10, -10)  width (5.625, -5.625)
  void Start() {
    RT = gameObject.GetComponent<RectTransform>();
    img = gameObject.GetComponent<Image>();
    color = img.color;
    hidePointer();
  }
  void Update()
  {
    clickedObject = FocusLevelUpdater.cameraFocusObject;
    if (clickedObject != null) {
      clickedObjectPosition = clickedObject.GetComponent<RectTransform>().position;
      Distance = clickedObjectPosition.magnitude;
      pointerSpin();
      CheckOutOfView();
    } else {
      return;
    }

    //change opacity by img.color.a
    //the aiming and rotating and how visible the arrow is wrt position
  }
  void CheckOutOfView() {
    if ((clickedObjectPosition.x < -6.1f || clickedObjectPosition.x > 6.1f) || (clickedObjectPosition.y < -10.5f || clickedObjectPosition.y > 10.5f)) {
      renderPointer();
    } else {
      hidePointer();
    }
    img.color = color;
  }
  void hidePointer() {
    color.a = 0f;
    img.color = color;
  }
  void pointerSpin() {
    if (clickedObjectPosition.x <= 0) {
      float angle = Mathf.Atan(clickedObjectPosition.y/clickedObjectPosition.x)*180/Mathf.PI;
      RT.transform.rotation = Quaternion.Euler(0, 0, angle);
    } else {
      if (clickedObjectPosition.y >= 0) {
        float angle = -180+Mathf.Atan(clickedObjectPosition.y/clickedObjectPosition.x)*180/Mathf.PI ;
        RT.transform.rotation = Quaternion.Euler(0, 0, angle);
      } else {
        float angle = 180+Mathf.Atan(clickedObjectPosition.y/clickedObjectPosition.x)*180/Mathf.PI;
        RT.transform.rotation = Quaternion.Euler(0, 0, angle);
      }
    }
  }
  void renderPointer() {
    if (Distance > 20f) {
      color.a = 1f;
    } else {
      float value = 0.2f + (Distance - 6f)/20f;
      color.a = value;
    }
  }
}
