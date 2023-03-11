using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameModeCannon : MonoBehaviour
{
  public GameObject MenuAimLine;
  string currentClicked1;
  string newscene;
  public void checkClicked(Button button) {
    string btn = button.name;
    if (btn == currentClicked1) {
      moveCannonPointer(btn);
      //movescene sound
      moveScene(btn);
    } else {
      //click sound
      moveCannonPointer(btn);
      currentClicked1 = btn;
    }
  }

  void moveCannonPointer(string clickedbutton) {
    LineRenderer LR = MenuAimLine.GetComponent<LineRenderer>();
    Transform transform = gameObject.GetComponent<Transform>();
    if (clickedbutton == "StoryBtn") {
      LR.SetPosition(1, new Vector3(-4.55f, 3f, 0f));
      transform.rotation = Quaternion.Euler(0, 0, -1.9f);
    }
    if (clickedbutton == "EndlessUpgraded") {
      LR.SetPosition(1, new Vector3(-2f, 0f, 0f));
      transform.rotation = Quaternion.Euler(0, 0, -21.80141f);
    }
    if (clickedbutton == "EndlessOriginal") {
      LR.SetPosition(1, new Vector3(3f, -4f, 0f));
      transform.rotation = Quaternion.Euler(0, 0, -51.84277f);
    }
  }


  void moveScene(string btn) {
    if (btn == "StoryBtn") {
      SceneManager.LoadScene("Worlds");
    }
    if (btn == "EndlessUpgraded") {
      SceneManager.LoadScene("EndlessUpgraded");
    }
    if (btn == "EndlessOriginal") {
      SceneManager.LoadScene("EndlessOriginal");
    }
  }
}

