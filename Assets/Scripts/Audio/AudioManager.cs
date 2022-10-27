using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Members

    #region Singleton Members
    private static AudioManager instance;
    public static AudioManager Instance => instance;
    #endregion

    public GameObject UISoundsObject = null;
    public GameObject AmbientSoundsObject = null;
    public GameObject EventSoundsObject = null;
    public GameObject FootStepSoundsObject = null;

    [Space(20)]

    public List<SoundAsset> UISounds = null;    
    public List<SoundAsset> AmbientSounds = null;
    public List<SoundAsset> EventSounds = null;

    [Space(20)]

    public List<SoundAsset> FootStepSounds = null;
    
    #endregion

    #region Unity Methods

    private void Awake()
    {
        Singleton();
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    private void Singleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            GenerateSoundSources();
        }
    }

    #region Methods

    #region Generate Sound Sources

    private void GenerateSoundSources()
    {
        GenerateUISoundSources();
        GenerateAmbientSoundSources();
        GenerateEventSoundSources();
        GenerateFootStepSounds();
    }

    private void GenerateUISoundSources()
    {
        foreach (SoundAsset soundAsset in UISounds)
        {
            soundAsset.Source = UISoundsObject.AddComponent<AudioSource>();
            soundAsset.Source.clip = soundAsset.Clip;
            soundAsset.Source.volume = soundAsset.Volume;
            soundAsset.Source.pitch = soundAsset.Pitch;
        }
    }

    private void GenerateAmbientSoundSources()
    {
        foreach (SoundAsset soundAsset in AmbientSounds)
        {
            soundAsset.Source = AmbientSoundsObject.AddComponent<AudioSource>();
            soundAsset.Source.clip = soundAsset.Clip;
            soundAsset.Source.volume = soundAsset.Volume;
            soundAsset.Source.pitch = soundAsset.Pitch;
            soundAsset.Source.loop = soundAsset.Loop;
        }
    }

    private void GenerateEventSoundSources()
    {
        foreach (SoundAsset soundAsset in EventSounds)
        {
            soundAsset.Source = EventSoundsObject.AddComponent<AudioSource>();
            soundAsset.Source.clip = soundAsset.Clip;
            soundAsset.Source.volume = soundAsset.Volume;
            soundAsset.Source.pitch = soundAsset.Pitch;
        }
    }

    private void GenerateFootStepSounds()
    {
        foreach (SoundAsset soundAsset in FootStepSounds)
        {
            soundAsset.Source = EventSoundsObject.AddComponent<AudioSource>();
            soundAsset.Source.clip = soundAsset.Clip;
            soundAsset.Source.volume = soundAsset.Volume;
            soundAsset.Source.pitch = soundAsset.Pitch;
        }
    }


    #endregion

    public void PlayUISound(string name)
    {
        PlaySound(UISounds.Find(sound => sound.Name == name));
    }    

    public void PlayAmbientSound(string name)
    {
        PlaySound(AmbientSounds.Find(sound => sound.Name == name));
    }

    public void PlayAmbientSoundFadeIn(string name, float fadeInOffset)
    {
        SoundAsset soundAsset = AmbientSounds.Find(sound => sound.Name == name);
        PlaySound(soundAsset);
        StartCoroutine(StartFade(soundAsset.Source, fadeInOffset));
    }

    public void StopAmbientSound(string name)
    {
        StopSound(AmbientSounds.Find(sound => sound.Name == name));
    }

    public void PlayEventSound(string name)
    {
        PlaySound(EventSounds.Find(sound => sound.Name == name));
    }

    public void PlayEventSound(string name, float delayOffset)
    {
        PlaySoundDelayed(EventSounds.Find(sound => sound.Name == name), delayOffset);
    }

    public void PlayRandomFootstepSound()
    {
        int random = UnityEngine.Random.Range(0, FootStepSounds.Count - 1);

        PlaySound(FootStepSounds[random]);
    }

    private void PlaySound(SoundAsset soundAsset)
    {
        if (soundAsset == null)
        {
            Debug.LogWarning($"Sound not found!");
        }
        else
        {
            soundAsset.Source.Play();
        }
    }

    private void StopSound(SoundAsset soundAsset)
    {
        if (soundAsset == null)
        {
            Debug.LogWarning($"Sound not found!");
        }
        else
        {
            soundAsset.Source.Stop();
        }
    }

    private void PlaySoundDelayed(SoundAsset soundAsset, float delayOffset)
    {
        if (soundAsset == null)
        {
            Debug.LogWarning($"Sound not found!");
        }
        else
        {
            soundAsset.Source.PlayDelayed(delayOffset);
        }
    }

    public IEnumerator StartFade(AudioSource audioSource, float duration)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, 1, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    #endregion
}
