using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDataDisplay : MonoBehaviour
{
  Level thislevel;
  public Text LevelNameTxt;
  public Text WaveNumberTxt;
  public Transform EnemiesDescriptionBoxT;
  public GameObject enemyBoxPrefab;
  void OnEnable() {
    thislevel = FocusLevelUpdater.focusLevel;
    LevelNameTxt.text = thislevel.name;
    WaveNumberTxt.text = "Waves: " + thislevel.upgradesPerWave.Count.ToString();
    ClearBox();
    foreach (Enemy enemy in thislevel.Enemies) {
      CreateEnemyBox(enemy);
    }
  }
  void ClearBox() {
    foreach (Transform childT in EnemiesDescriptionBoxT) {
      Destroy(childT.gameObject);
    }
  }
  void BoxPosReset() {
    Vector2 updatepos = new Vector2(0f, 0f);
    EnemiesDescriptionBoxT.gameObject.GetComponent<RectTransform>().anchoredPosition = updatepos;
  }
  void CreateEnemyBox(Enemy enemy) {
    BoxPosReset();
    GameObject enemyBox = Instantiate(enemyBoxPrefab, EnemiesDescriptionBoxT);
    Transform enemyBoxT = enemyBox.GetComponent<Transform>();
    //Make sure the sprites are squares in dimension. As I have not got scaling here.
    enemyBoxT.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = enemy.sprite;
    Text enemyName = enemyBoxT.GetChild(1).gameObject.GetComponent<Text>();
    enemyName.text = enemy.name;

    Text enemyDes = enemyBoxT.GetChild(2).gameObject.GetComponent<Text>();
    enemyDes.text = enemy.enemyDescription;
  }
  #region BtnNavigation
  public void CloseLevelPanel() {
    gameObject.SetActive(false);
  }
  public void EnterStoryPlay() {
    SceneManager.LoadScene(thislevel.name);
  }
  #endregion
}
