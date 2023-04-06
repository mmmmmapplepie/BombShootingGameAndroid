using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VesselContents : MonoBehaviour, IdestroyFunction {
  [SerializeField] List<Enemy> availableEnemies;
  [SerializeField] int heldEnemiesNumber;
  [SerializeField] SpriteRenderer Holder1, Holder2, Holder3, Holder4;
  List<Enemy> heldEnemies = new List<Enemy>();
  void Start() {
    ChooseHeldEnemies();
    ShiftHolders();
  }
  void ShiftHolders() {
    if (heldEnemiesNumber == 3) {
      Holder4.gameObject.SetActive(false);
      Holder1.transform.localPosition = new Vector3(-0.26f, -0.125f, 0f);
      Holder2.transform.localPosition = new Vector3(0f, -0.125f, 0f);
      Holder3.transform.localPosition = new Vector3(0.26f, -0.125f, 0f);
    }
  }
  void ChooseHeldEnemies() {
    int length = availableEnemies.Count;
    for (int i = 0; i < heldEnemiesNumber; i++) {
      int index = Random.Range(0, length);
      heldEnemies.Add(availableEnemies[index]);
    }
    changeHolderSprites();
    changeDamage();
  }
  void changeHolderSprites() {
    Holder1.sprite = heldEnemies[0].sprite;
    Holder2.sprite = heldEnemies[1].sprite;
    Holder3.sprite = heldEnemies[2].sprite;
    if (heldEnemiesNumber == 4) {
      Holder4.sprite = heldEnemies[3].sprite;
    }
  }
  void changeDamage() {
    EnemyDamage script = transform.root.GetComponent<EnemyDamage>();
    script.Damage = 0f;
    foreach (Enemy ene in heldEnemies) {
      script.Damage += ene.Damage;
    }
  }
  public void DestroyFunction() {
    foreach (Enemy ene in heldEnemies) {
      float xPos = Random.Range(-1f, 1f);
      Instantiate(ene.enemyPrefab, new Vector3(transform.root.position.x + xPos, transform.root.position.y, 0f), Quaternion.identity);
    }
  }
}
