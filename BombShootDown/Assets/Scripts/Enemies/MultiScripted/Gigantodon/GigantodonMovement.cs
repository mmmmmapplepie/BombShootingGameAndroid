using UnityEngine;

public class GigantodonMovement : MonoBehaviour {
  Transform EnemyBase;
  float speed;
  float startTime;
  float xPos = 3f;
  void Start() {
    speed = transform.parent.gameObject.GetComponent<IDamageable>().data.Speed;
    EnemyBase = transform.root;
    startTime = Time.time;
    move();
  }
  void Update() {
    move();
  }
  void move() {
    EnemyBase.position = new Vector3(EnemyBase.position.x, EnemyBase.position.y - speed * BowManager.EnemySpeed * Time.deltaTime, 0f);
    sidewardsTeleport();
  }
  void sidewardsTeleport() {
    if (Time.time > startTime) {
      Teleport();
    };
  }
  void Teleport() {
    startTime = Time.time + Random.Range(10f, 15f);
    EnemyBase.position = new Vector3(xPos, EnemyBase.position.y, 0f);
    xPos = -xPos;
  }
}
