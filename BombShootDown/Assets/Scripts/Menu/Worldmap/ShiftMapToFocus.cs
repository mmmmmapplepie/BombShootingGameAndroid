using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShiftMapToFocus : MonoBehaviour {
  RectTransform RT;
  float[] lastLevelT;
  float cameraScreenWidth;
  float cameraScreenHeight;
  // map is 4096 wide by 3072 tall
  float shiftx;
  float shifty;
  float mapwidth;
  float mapheight;
  new Camera camera;
  void Start() {
    RT = GetComponent<RectTransform>();
    camera = Camera.main;
    cameraScreenHeight = (float)camera.pixelHeight > 1600f ? 1600f : (float)camera.pixelHeight;
    cameraScreenWidth = (float)camera.pixelWidth > 900f ? 900f : (float)camera.pixelWidth;
    lastLevelT = SettingsManager.currentFocusLevelTransform;
    if (lastLevelT[0] == 0 && lastLevelT[1] == 0) {

      RT.localPosition = new Vector2(-cameraScreenWidth * 0.5f, -cameraScreenHeight * 0.5f);
    } else {
      Transform();
    }
  }
  void Transform() {
    mapwidth = RT.rect.width;
    mapheight = RT.rect.height;
    shiftx = lastLevelT[0];
    shifty = lastLevelT[1];
    if ((mapwidth - lastLevelT[0]) < (cameraScreenWidth * 0.5f)) {
      shiftx = mapwidth - cameraScreenWidth * 0.5f;
    }
    if (lastLevelT[0] < (cameraScreenWidth * 0.5f)) {
      shiftx = cameraScreenWidth * 0.5f;
    }
    if ((mapheight - lastLevelT[1]) < (cameraScreenHeight * 0.5f)) {
      shifty = mapheight - cameraScreenHeight * 0.5f;
    }
    if (lastLevelT[1] < (cameraScreenHeight * 0.5f)) {
      shifty = cameraScreenHeight * 0.5f;
    }
    RT.localPosition = new Vector2(-shiftx, -shifty);
  }
}
