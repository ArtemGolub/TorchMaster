using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager current;
    
    public Sound[] musicSounds, sfxSounds, playerSpeak;
    public AudioSource musicSource, sfxSource, playerSpeakSoruce;
    
    [SerializeField]private List<AudioSource> additionalAudioSource;

    public void AddAdditionalAudioSource(AudioSource source)
    {
        if (source != null)
        {
            if (additionalAudioSource == null)
            {
                additionalAudioSource = new List<AudioSource>();
            }
        
            additionalAudioSource.Add(source);
            ToggleAdditionalSounds(source);
            AdditionalSoundsVolume(source);
            RemoveMissingAudioSources();
        }
    }
    private void RemoveMissingAudioSources()
    {
        if (additionalAudioSource != null)
        {
            additionalAudioSource.RemoveAll(source => source == null || source.gameObject == null);
        }
    }
    private void Awake()
    {
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PlayMainTheme()
    {
        PlayMusic(GetThemeBySceneName(SceneManager.GetActiveScene().name));
    }

    private SoundType GetThemeBySceneName(string SceneName)
    {
        switch (SceneName)
        {
            case "MainMenu":
            {
                return SoundType.ThemeMenu;
            }
            case "Level1":
            {
                return SoundType.ThemeLevel1;
            }
            case "Level2":
            {
                return SoundType.ThemeLevel2;
            }
            case "Level3":
            {
                return SoundType.ThemeLevel3;
            }
            default:
            {
                throw new Exception($"No Sound Theme for scene {SceneName}");
            }
        }
    }
    
    private void PlayMusic(SoundType type)
    {
        Sound s = Array.Find(musicSounds, x => x.type == type);
        if (s == null)
        {
            Debug.LogError($"Sound of type {type} not found");
            return;
        }
        musicSource.clip = s.clip;
        musicSource.Play();
    }

    public void PlaySFX(SoundType type)
    {
        Sound s = Array.Find(sfxSounds, x => x.type == type);
        if (s == null)
        {
            Debug.LogError($"Sound of type {type} not found");
            return;
        }
        sfxSource.PlayOneShot(s.clip);
    }

    public void PlayPlayerSpeak(SoundType type)
    {
        Sound s = Array.Find(playerSpeak, x => x.type == type);
        if (s == null)
        {
            Debug.LogError($"Sound of type {type} not found");
            return;
        }
        playerSpeakSoruce.PlayOneShot(s.clip);
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSounds()
    {
        sfxSource.mute = !sfxSource.mute;
        playerSpeakSoruce.mute = !playerSpeakSoruce.mute;
        foreach (var source in additionalAudioSource)
        {
            ToggleAdditionalSounds(source);
        }
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SoundsVolume(float volume)
    {
        sfxSource.volume = volume;
        playerSpeakSoruce.volume = volume;
        foreach (var source in additionalAudioSource)
        {
            AdditionalSoundsVolume(source);
        }
    }

    private void ToggleAdditionalSounds(AudioSource soruce)
    {
        soruce.mute = sfxSource.mute;
    }

    private void AdditionalSoundsVolume(AudioSource soruce)
    {
        soruce.volume = sfxSource.volume;
    }
}
