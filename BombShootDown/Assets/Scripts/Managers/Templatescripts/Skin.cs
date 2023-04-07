using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Skin", menuName = "Skin")]
public class Skin : ScriptableObject {
  public new string name;
  public GameObject particleEffect = null;
  public int price = 0;
  public enum skinType { Bow, Fortress, Bullet };
  [SerializeField]
  public skinType type;
  public Sprite mainBody;
  public Sprite LeftString = null;
  public Sprite RightString = null;
  public Sprite LeftBolt = null;
  public Sprite RightBolt = null;
  public float PS_Scale = 1f;
  public Vector3 PS_Position = Vector3.zero;
}
