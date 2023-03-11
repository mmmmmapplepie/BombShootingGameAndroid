using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
  public new string name;
  public List<int> upgradesPerWave;
  public int firstClearRewards;
  public int clearRewards;
  public List<Enemy> Enemies;
  public int[] stageInWorld;
}
