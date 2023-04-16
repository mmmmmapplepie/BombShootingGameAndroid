using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skinSetup : MonoBehaviour {
  [SerializeField]
  Transform BowRect, BulletRect, FortressRect;
  [SerializeField]
  GameObject BowSkinPreviewPrefab, BulletSkinPreviewPrefab, FortressSkinPreviewPrefab;
  temporarySkinHolder temp;
  void Awake() {
    temp = gameObject.GetComponent<temporarySkinHolder>();
    InitializeSkins();
  }
  void InitializeSkins() {
    foreach (Skin skin in temp.allBowSkins) {
      createBowPreview(skin);
    }
    foreach (Skin skin in temp.allFortressSkins) {
      createFortressPreview(skin);
    }
    foreach (Skin skin in temp.allBulletSkins) {
      createBulletPreview(skin);
    }
  }
  void createBowPreview(Skin skin) {
    BowSkinPreviewPrefab.GetComponent<skinPrefabSetup>().skin = skin;
    GameObject pre = Instantiate(BowSkinPreviewPrefab, BowRect);
  }
  void createBulletPreview(Skin skin) {
    BulletSkinPreviewPrefab.GetComponent<skinPrefabSetup>().skin = skin;
    GameObject pre = Instantiate(BulletSkinPreviewPrefab, BulletRect);
  }
  void createFortressPreview(Skin skin) {
    FortressSkinPreviewPrefab.GetComponent<skinPrefabSetup>().skin = skin;
    GameObject pre = Instantiate(FortressSkinPreviewPrefab, FortressRect);
  }








}
