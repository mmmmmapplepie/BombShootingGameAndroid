using UnityEngine;
using UnityEngine.UI;

public class WorldCanvasCameraSetter : MonoBehaviour
{
  Canvas canvas;
  void Awake()
  {
    canvas = gameObject.GetComponent<Canvas>();
    canvas.worldCamera = Camera.main;
  }
}
