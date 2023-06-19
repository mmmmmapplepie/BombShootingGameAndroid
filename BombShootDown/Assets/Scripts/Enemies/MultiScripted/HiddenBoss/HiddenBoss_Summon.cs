using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenBoss_Summon : MonoBehaviour {
  [SerializeField] GameObject BigSpawnEffect;
  [SerializeField] List<Enemy> enemies;
  Queue<Enemy> enemiesQueue = new Queue<Enemy>();
  Vector3 spawnPosition;

  [SerializeField] Enemy coupladFollower, coupladMaxFollower;
  void Awake() {
    randomizeList(enemies);
  }
  void randomizeList(List<Enemy> list) {
    int n = list.Count;
    while (n > 1) {
      n--;
      int k = Random.Range(0, n - 1);
      Enemy value = list[k];
      list[k] = list[n];
      list[n] = value;
    }
    foreach (Enemy enemy in list) {
      enemiesQueue.Enqueue(enemy);
    }
  }
  GameObject GetNextEnemyFromQueue(Queue<Enemy> queue) {
    Enemy enemy = queue.Dequeue();
    queue.Enqueue(enemy);
    return enemy.enemyPrefab;
  }
  void SpawnEnemy() {
    GameObject prefab = GetNextEnemyFromQueue(enemiesQueue);
    Instantiate(prefab, spawnPosition, Quaternion.identity);
    if (prefab.name == "CoupladSeeker") {
      Instantiate(coupladFollower.enemyPrefab, spawnPosition, Quaternion.identity);
    }
    if (prefab.name == "MaxCoupladSeeker") {
      Instantiate(coupladMaxFollower.enemyPrefab, spawnPosition, Quaternion.identity);
    }
  }
  void OnEnable() {
    spawnPosition = transform.root.position;
    Instantiate(BigSpawnEffect, spawnPosition, Quaternion.identity);
    Invoke("SpawnEnemy", 0.5f);
  }
}
