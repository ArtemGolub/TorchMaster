using UnityEngine;
using UnityEngine.UI;

public class SoundControllerUI : MonoBehaviour
{
    public Button toggleMusic, toggleSounds;
    public Slider musicSlider, soundsSlider;

    private void Start()
    {
        toggleMusic.onClick.AddListener(ToggleMusic);
        toggleSounds.onClick.AddListener(ToggleSounds);

        musicSlider.value = AudioManager.current.musicSource.volume;
        soundsSlider.value = AudioManager.current.sfxSource.volume;
        
        musicSlider.onValueChanged.AddListener(MusicVolume);
        soundsSlider.onValueChanged.AddListener(SoundsVolume);
    }

    private void ToggleMusic()
    {
        AudioManager.current.ToggleMusic();
    }

    private void ToggleSounds()
    {
        AudioManager.current.ToggleSounds();
    }

    private void MusicVolume(float value)
    {
        AudioManager.current.MusicVolume(value);
    }

    private void SoundsVolume(float value)
    {
        AudioManager.current.SoundsVolume(value);
    }
}
