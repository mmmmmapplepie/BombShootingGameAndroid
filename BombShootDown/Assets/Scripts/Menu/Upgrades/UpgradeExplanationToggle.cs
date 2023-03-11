using UnityEngine;
using UnityEngine.UI;

public class UpgradeExplanationToggle : MonoBehaviour
{
  [SerializeField]
  GameObject Explanation;
  public void close() {
    Explanation.SetActive(false);
  }
  public void open() {
    Explanation.SetActive(true);
  }
}
