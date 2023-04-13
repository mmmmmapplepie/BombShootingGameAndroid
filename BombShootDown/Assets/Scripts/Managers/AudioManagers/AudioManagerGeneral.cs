using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

public class AudioManagerGeneral : MonoBehaviour {
  static GameObject instance;
  void Awake() {
    if (instance == null) {
      instance = gameObject;
      DontDestroyOnLoad(instance);
    } else {
      Destroy(gameObject);
    }
  }
  protected Sound FindSound(string soundname, List<Sound> SoundList) {
    Sound sound = SoundList.Find(s => s.name == soundname);
    if (sound == null) {
      Debug.Log("Sound " + soundname + " was not found!");
    }
    return sound;
  }
  protected void SetAudioSources(List<Sound> list, GameObject obj) {
    foreach (Sound s in list) {
      s.source = obj.AddComponent<AudioSource>();
      s.source.clip = s.clip;
      s.source.volume = s.volume;
      s.source.pitch = s.pitch;
      s.source.loop = s.loop;
      s.source.playOnAwake = s.playOnAwake;
      s.source.priority = s.priority;
    }
  }
}
