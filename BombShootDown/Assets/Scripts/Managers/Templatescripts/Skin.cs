using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Skin", menuName = "Skin")]
public class Skin : ScriptableObject {
  public new string name;
  public GameObject particleEffect = null;
  public Sprite mainBody;
  public Sprite LeftString = null;
  public Sprite RightString = null;
  public Sprite LeftBolt = null;
  public Sprite RightBolt = null;
}
