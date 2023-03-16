using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioManagerUI : AudioManagerGeneral
{
  [SerializeField]
  List<Sound> SoundList;
  void Awake()
  {
    SetAudioSources(SoundList, gameObject);
    foreach (Sound s in SoundList)
    {
      s.source.ignoreListenerPause = true;
    }
  }
  public void PlayAudio(string soundname)
  {
    Sound sound = FindSound(soundname, SoundList);
    sound.source.Play();
    sound.source.volume = sound.volume;
  }
}
