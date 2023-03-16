using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioManagerBGM : AudioManagerGeneral
{
  [SerializeField]
  List<Sound> SoundList;
  public Sound currentBGM;
  float volumeSettingStart;
  void Awake()
  {
    // if (instance == null)
    // {
    //   instance = gameObject;
    // }
    // else
    // {
    //   Destroy(gameObject);
    // }
    // DontDestroyOnLoad(gameObject);
    SetAudioSources(SoundList, gameObject);
  }
  void Start()
  {
    volumeSettingStart = SettingsManager.volumeTheme;
    PlayAudio("MenuTheme");
  }
  void Update()
  {
    if (volumeSettingStart != SettingsManager.volumeTheme)
    {
      volumeSettingStart = SettingsManager.volumeTheme;
      currentBGM.source.volume = volumeSettingStart;
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
    StartCoroutine("BGMfade", newBGMname);
  }
  IEnumerator BGMfade(string newBGM)
  {
    float volumeLvl = SettingsManager.volumeTheme;
    float changingVolume = volumeLvl;
    while (changingVolume > 0f)
    {
      currentBGM.source.volume = changingVolume;
      changingVolume -= volumeLvl / 40f;
      yield return new WaitForSecondsRealtime(1f / 40f);
    }
    changingVolume = 0f;
    currentBGM.source.Stop();
    PlayAudio(newBGM);
    currentBGM.source.volume = changingVolume;
    while (changingVolume < volumeLvl)
    {
      currentBGM.source.volume = changingVolume;
      changingVolume += volumeLvl / 40f;
      yield return new WaitForSecondsRealtime(1f / 40f);
    }
    currentBGM.source.volume = volumeLvl;
  }

}
