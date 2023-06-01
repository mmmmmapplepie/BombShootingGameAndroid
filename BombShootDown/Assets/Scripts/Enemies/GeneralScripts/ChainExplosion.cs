using UnityEngine;
using UnityEngine.Audio;
public class ChainExplosion : MonoBehaviour {
  [SerializeField]
  GameObject chainedAnimation;
  [HideInInspector]
  public bool Chained = false;
  public bool animationAdded = false;
  RectTransform rect;
  AudioManagerEnemy audioManager;
  void Awake() {
    audioManager = transform.Find("AudioManagerEnemy").GetComponent<AudioManagerEnemy>();
    rect = GetComponent<RectTransform>();
  }
  void Update() {
    if (Chained && !animationAdded && gameObject.GetComponent<EnemyLife>().currentLife > 0f) {
      animationAdded = true;
      Transform anchorParent = transform.Find("State").Find("Life").Find("Background");
      Instantiate(chainedAnimation, anchorParent.position, Quaternion.identity, anchorParent);
    }
  }
  public void Explode() {
    audioManager.PlayAudio("ChainExplosion");
    Collider2D[] Objects = Physics2D.OverlapCircleAll(transform.position, 1.5f);
    foreach (Collider2D coll in Objects) {
      if ((coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "TauntEnemy") && coll.gameObject.GetComponent<IDamageable>() != null) {
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
