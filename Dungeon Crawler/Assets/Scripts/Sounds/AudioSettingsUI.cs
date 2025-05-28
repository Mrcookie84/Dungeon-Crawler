using System;
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

    public static AudioSettingsUI SINGLETON;

    [Range(0f, 10f)] public float defaultMasterSoundValue;
    [Range(0f, 10f)] public float defaultMusicSoundValue;
    [Range(0f, 10f)] public float defaultFxSoundValue;


    private void Start()
    {
        PlayerPrefs.SetFloat(MasterKey, defaultMasterSoundValue);
        PlayerPrefs.SetFloat(MusicKey, defaultMusicSoundValue);
        PlayerPrefs.SetFloat(SFXKey, defaultFxSoundValue);
        masterSlider.value = PlayerPrefs.GetFloat(MasterKey);
        musicSlider.value = PlayerPrefs.GetFloat(MusicKey);
        sfxSlider.value = PlayerPrefs.GetFloat(SFXKey);

        SoundManager.SINGLETON.SetMasterVolume(masterSlider.value);
        SoundManager.SINGLETON.SetMusicVolume(musicSlider.value);
        SoundManager.SINGLETON.SetSFXVolume(sfxSlider.value);

        masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    private void Awake()
    {
        if (SINGLETON == null)
        {
            SINGLETON = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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