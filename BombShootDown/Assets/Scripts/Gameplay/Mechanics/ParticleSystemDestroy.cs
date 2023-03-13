using UnityEngine;

public class ParticleSystemDestroy : MonoBehaviour
{
  [SerializeField]
  public float timer;
  float startTime;
  // Start is called before the first frame update
  void Start()
  {
    startTime = Time.time;
  }
  void Update()
  {
    if ((Time.time - startTime) > timer)
    {
      Destroy(gameObject);
    }
  }
}
