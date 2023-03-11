using UnityEngine;
using UnityEngine.UI;

public class GameModesProgress : MonoBehaviour
{
  public GameObject[] worlds;
  public GameObject secretWorld;
    void Start()
    {
      for (int i = 0; i < SettingsManager.world[0]; i++) {
        worlds[i].SetActive(true);
      }
      int[] last = new int[2] {3, 10};
      if (SettingsManager.world == last) {
        secretWorld.SetActive(true);
      }
    }
}
