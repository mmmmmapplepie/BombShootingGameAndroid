using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShiftMapToFocus : MonoBehaviour
{
  RectTransform RT;
  float[] lastLevelT;
  float screenWidth;
  float screenHeight;
  // map is 4096 wide by 3072 tall
  float shiftx;
  float shifty;
  float mapwidth;
  float mapheight;
  Camera camera;
  void Start()
  {
    camera = Camera.main;
    UpgradesManager.loadAllData();
    lastLevelT = SettingsManager.currentFocusLevelTransform;
    if (lastLevelT != null)
    {
      Transform();
    }
    else
    {
      screenHeight = (float)camera.pixelHeight;
      screenWidth = (float)camera.pixelWidth;
      RT.localPosition = new Vector2(-screenWidth * 0.5f, -screenHeight * 0.5f);
    }
  }
  void Transform()
  {
    RT = GetComponent<RectTransform>();
    mapwidth = RT.rect.width;
    mapheight = RT.rect.height;
    screenHeight = (float)camera.pixelHeight;
    screenWidth = (float)camera.pixelWidth;
    shiftx = lastLevelT[0];
    shifty = lastLevelT[1];
    if ((mapwidth - lastLevelT[0]) < (screenWidth * 0.5f))
    {
      shiftx = mapwidth - screenWidth * 0.5f;
    }
    if (lastLevelT[0] < (screenWidth * 0.5f))
    {
      shiftx = screenWidth * 0.5f;
    }
    if ((mapheight - lastLevelT[1]) < (screenHeight * 0.5f))
    {
      shifty = mapheight - screenHeight * 0.5f;
    }
    if (lastLevelT[1] < (screenHeight * 0.5f))
    {
      shifty = screenHeight * 0.5f;
    }
    RT.localPosition = new Vector2(-shiftx, -shifty);
  }
}
