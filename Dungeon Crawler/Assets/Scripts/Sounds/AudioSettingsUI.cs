using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private const string MasterKey = "Volume_Master";
    private const string MusicKey = "Volume_Music";
    private const string SFXKey = "Volume_SFX";

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat(MasterKey, 1f);
        musicSlider.value = PlayerPrefs.GetFloat(MusicKey, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(SFXKey, 1f);
        
        SoundManager.SINGLETON.SetMasterVolume(masterSlider.value);
        SoundManager.SINGLETON.SetMusicVolume(musicSlider.value);
        SoundManager.SINGLETON.SetSFXVolume(sfxSlider.value);
        
        masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    private void OnMasterVolumeChanged(float value)
    {
        SoundManager.SINGLETON.SetMasterVolume(value);
        PlayerPrefs.SetFloat(MasterKey, value);
    }

    private void OnMusicVolumeChanged(float value)
    {
        SoundManager.SINGLETON.SetMusicVolume(value);
        PlayerPrefs.SetFloat(MusicKey, value);
    }

    private void OnSFXVolumeChanged(float value)
    {
        SoundManager.SINGLETON.SetSFXVolume(value);
        PlayerPrefs.SetFloat(SFXKey, value);
    }
}