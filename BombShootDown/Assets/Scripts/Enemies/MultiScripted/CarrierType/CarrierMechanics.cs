using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierMechanics : MonoBehaviour {
  [SerializeField] Animator animator;
  [SerializeField] GameObject smallSpawnEffect, bigSpawnEffect;
  [SerializeField] List<Enemy> smallWeak, smallStrong, bigWeak, bigStrong;
  [SerializeField] float shieldrate;
  [SerializeField] float regen, DOT;
  [Range(0, 60)]
  [SerializeField] int BigStrongRawPercent, BigWeakRawPercent, SmallStrongRawPercent;
  EnemyLife lifeScript;
  Queue<Enemy> qsmallw = new Queue<Enemy>();
  Queue<Enemy> qsmalls = new Queue<Enemy>();
  Queue<Enemy> qbigw = new Queue<Enemy>();
  Queue<Enemy> qbigs = new Queue<Enemy>();
  bool Summoning = false;
  float shieldRefreshTime;
  Vector3 pos;
  void OnValidate() {
    if (BigStrongRawPercent + BigWeakRawPercent + SmallStrongRawPercent > 100) {
      BigStrongRawPercent = 5;
      BigWeakRawPercent = 10;
      SmallStrongRawPercent = 25;
    }
  }
  void Awake() {
    lifeScript = transform.root.GetComponent<EnemyLife>();
    SetQueue(smallWeak, qsmallw);
    SetQueue(smallStrong, qsmalls);
    SetQueue(bigWeak, qbigw);
    SetQueue(bigStrong, qbigs);
    shieldRefreshTime = Time.time;
    StartCoroutine(Summon());
  }
  void Update() {
    healUp();
    shieldUp();
    dealDmg();
  }
  void healUp() {
    lifeScript.currentLife = lifeScript.currentLife + regen * Time.deltaTime > lifeScript.maxLife ? lifeScript.maxLife : lifeScript.currentLife + regen * Time.deltaTime;
  }
  void shieldUp() {
    if (Time.time > shieldRefreshTime + shieldrate) {
      lifeScript.Shield = lifeScript.Shield + 1 > lifeScript.MaxShield ? lifeScript.MaxShield : lifeScript.Shield + 1;
    }
    shieldRefreshTime = Time.time;
  }
  void dealDmg() {
    LifeManager.CurrentLife -= DOT * Time.deltaTime;
  }
  void SetQueue(List<Enemy> enemyList, Queue<Enemy> queue) {
    randomizeList(enemyList);
    foreach (Enemy en in enemyList) {
      queue.Enqueue(en);
    }
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
  }
  GameObject GetNextEnemyFromQueue(Queue<Enemy> queue) {
    Enemy enemy = queue.Dequeue();
    queue.Enqueue(enemy);
    return enemy.enemyPrefab;
  }
  IEnumerator Summon() {
    while (true) {
      Summoning = true;
      float wait = Random.Range(1f, 2f);
      yield return new WaitForSeconds(wait);
      float difficulty = Random.Range(0, 100);
      if (difficulty <= BigStrongRawPercent) {
        GameObject enemy = GetNextEnemyFromQueue(qbigs);
        StartCoroutine(SummonRoutine(true, enemy));
      }
      if (difficulty > BigStrongRawPercent && difficulty <= (BigStrongRawPercent + BigWeakRawPercent)) {
        GameObject enemy = GetNextEnemyFromQueue(qbigw);
        StartCoroutine(SummonRoutine(true, enemy));
      }
      if (difficulty > (BigStrongRawPercent + BigWeakRawPercent) && difficulty <= (SmallStrongRawPercent + BigWeakRawPercent)) {
        GameObject enemy = GetNextEnemyFromQueue(qsmalls);
        StartCoroutine(SummonRoutine(false, enemy));
      }
      if (difficulty > (SmallStrongRawPercent + BigWeakRawPercent)) {
        GameObject enemy = GetNextEnemyFromQueue(qsmallw);
        StartCoroutine(SummonRoutine(false, enemy));
      }
      while (Summoning) {
        yield return null;
      }
    }
  }
  IEnumerator SummonRoutine(bool Big, GameObject prefab) {
    int i = 0;
    while (i < 20) {
      i++;
      animator.speed = ((float)i + 1f) * 0.5f;
      yield return new WaitForSeconds(1f / (20f * BowManager.EnemySpeed));
    }
    animator.speed = 10f;
    pos = transform.root.position;
    if (Big) {
      GameObject spawnEffect = Instantiate(bigSpawnEffect, pos, Quaternion.identity);
      ParticleSystem.MainModule ps = spawnEffect.GetComponent<ParticleSystem>().main;
      ps.simulationSpeed = BowManager.EnemySpeed;
    } else {
      GameObject spawnEffect = Instantiate(smallSpawnEffect, pos, Quaternion.identity);
      ParticleSystem.MainModule ps = spawnEffect.GetComponent<ParticleSystem>().main;
      ps.simulationSpeed = BowManager.EnemySpeed;
    }
    StartCoroutine("Spawn", prefab);
    i = 39;
    while (i > 0) {
      i--;
      animator.speed = ((float)i + 1f) * 0.5f;
      yield return new WaitForSeconds(1f / (20f * BowManager.EnemySpeed));
    }
    animator.speed = 1f;
    Summoning = false;
  }
  IEnumerator Spawn(GameObject prefab) {
    yield return new WaitForSeconds(0.5f / BowManager.EnemySpeed);
    Instantiate(prefab, pos, Quaternion.identity);
  }
}
