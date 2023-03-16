using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
  public string name;
  public AudioClip clip;
  [Range(0f, 1f)]
  public float volume = 1f;
  [Range(-3f, 3f)]
  public float pitch = 1f;
  public bool loop = false;
  public bool playOnAwake = true;
  [Range(0, 256)]
  public int priority = 0;
  [HideInInspector]
  public AudioSource source;
}
