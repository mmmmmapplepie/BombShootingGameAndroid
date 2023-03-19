using UnityEngine;
using UnityEngine.Audio;
public class ChainExplosion : MonoBehaviour {
  [SerializeField]
  GameObject chainedAnimation;
  [HideInInspector]
  public bool Chained = false;
  bool animationAdd = false;
  RectTransform rect;
  AudioManagerEnemy audioManager;
  void Awake() {
    audioManager = transform.Find("AudioManagerEnemy").GetComponent<AudioManagerEnemy>();
    rect = GetComponent<RectTransform>();
  }
  void Update() {
    if (Chained && !animationAdd && gameObject.GetComponent<EnemyLife>().currentLife > 0f && Time.timeScale != 0f) {
      animationAdd = true;
      Instantiate(chainedAnimation, transform.Find("State").Find("Life").Find("Background"));
    }
  }
  public void Explode() {
    audioManager.PlayAudio("ChainExplosion");
    Collider2D[] Objects = Physics2D.OverlapCircleAll(transform.position, 1.5f);
    foreach (Collider2D coll in Objects) {
      if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "TauntEnemy") {
        if (coll.gameObject == gameObject) {
          continue;
        }
        coll.transform.root.GetComponent<ChainExplosion>().Chained = true;
        if (coll.transform.root.GetComponent<EnemyLife>().currentLife <= 0) {
          continue;
        }
        coll.transform.root.GetComponent<EnemyLife>().ChainExplosion();
      }
    }
  }
}
