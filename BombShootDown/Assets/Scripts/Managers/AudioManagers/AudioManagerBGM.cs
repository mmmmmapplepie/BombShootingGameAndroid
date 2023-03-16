using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioManagerBGM : AudioManagerGeneral
{
  [SerializeField]
  List<Sound> SoundList;
  [HideInInspector]
  public Sound currentBGM;
  float volumeSettingStart;
  bool changingBGM = false;
  void Awake()
  {
    SetAudioSources(SoundList, gameObject);
    volumeSettingStart = SettingsManager.volumeTheme;
    PlayAudio("MenuTheme");
  }
  void Update()
  {
    if (volumeSettingStart != SettingsManager.volumeTheme)
    {
      currentBGM.source.volume = SettingsManager.volumeTheme;
    }
  }
  void PlayAudio(string soundname)
  {
    Sound sound = FindSound(soundname, SoundList);
    sound.source.Play();
    sound.source.volume = SettingsManager.volumeTheme * sound.volume;
    currentBGM = sound;
  }
  public void ChangeBGM(string newBGMname)
  {
    if (changingBGM)
    {
      StopCoroutine("BGMFadeOutIn");
      print("stopped");
    }
    StartCoroutine("BGMFadeOutIn", newBGMname);
  }
  IEnumerator BGMFadeOutIn(string newBGM)
  {
    changingBGM = true;
    float volumeLvl = SettingsManager.volumeTheme;
    float volumeLvlInitial = currentBGM.source.volume;
    float changingVolume = volumeLvlInitial;
    while (changingVolume > 0f)
    {
      currentBGM.source.volume = changingVolume;
      print(currentBGM.source.volume);
      changingVolume -= (volumeLvl / 40f);
      yield return new WaitForSecondsRealtime(5f / 40f);
    }
    changingVolume = 0f;
    currentBGM.source.Stop();
    PlayAudio(newBGM);
    currentBGM.source.volume = changingVolume;
    while (changingVolume < volumeLvl)
    {
      currentBGM.source.volume = changingVolume;
      changingVolume += (volumeLvl / 40f);
      yield return new WaitForSecondsRealtime(5f / 40f);
    }
    currentBGM.source.volume = volumeLvl;
    changingBGM = false;
  }
}
