using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErnestoMovement : MonoBehaviour {
  Transform root;
  float XPos;
  float YPos;
  void Start() {
    root = transform.root;
    pickNewPosition();
    root.position = new Vector3(XPos, root.position.y, 0f);
    StartCoroutine(movePosition());
  }
  void Update() {
    BobMovement();
  }
  void pickNewPosition() {
    XPos = Random.Range(3f, 4f);
    YPos = root.position.y;
    int side = Random.Range(-1, 2);
    while (side == 0) {
      side = Random.Range(-1, 2);
    }
    XPos = (float)side * XPos;
  }
  void BobMovement() {
    root.position = new Vector3(root.position.x, YPos + 0.5f * Mathf.Sin(0.5f * Time.time), 0f);
  }
  IEnumerator movePosition() {
    while (true) {
      yield return new WaitForSeconds(Random.Range(20f, 25f));
      root.position = new Vector3(XPos, root.position.y - 1f, 0f);
      YPos = root.position.y;
      pickNewPosition();
    }
  }
}
