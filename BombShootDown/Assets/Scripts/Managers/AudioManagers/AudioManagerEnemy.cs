using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioManagerEnemy : AudioManagerGeneral
{
  [SerializeField]
  List<Sound> SoundList;
  void Awake()
  {
    SetAudioSources(SoundList, gameObject);
  }
  public void PlayAudio(string soundname)
  {
    Sound sound = FindSound(soundname, SoundList);
    sound.source.Play();
    sound.source.volume = SettingsManager.volumeEnemy * sound.volume;
  }
}
