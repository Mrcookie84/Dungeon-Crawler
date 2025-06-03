using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager SINGLETON;

    [Header("Audio Sources")]
    public AudioSource[] fxSources;
    public AudioSource musicSource;
    public AudioSource menuMusic;

    [Header("UI Sliders")]
    public Slider fxVolumeSlider;
    public Slider musicVolumeSlider;

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
            return;
        }
    }

    private void Start()
    {
        float savedFX = PlayerPrefs.GetFloat("FXVolume", 1f);
        float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 1f);

        if (fxVolumeSlider != null)
        {
            fxVolumeSlider.value = savedFX;
            fxVolumeSlider.onValueChanged.AddListener(SetFXVolume);
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = savedMusic;
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        SetFXVolume(savedFX);
        SetMusicVolume(savedMusic);
    }

    public void SetFXVolume(float volume)
    {
        foreach (AudioSource src in fxSources)
        {
            src.volume = volume;
        }

        PlayerPrefs.SetFloat("FXVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            if (volume <= 0.001f)
            {
                musicSource.Stop();
            }
            else
            {
                if (!musicSource.isPlaying)
                    musicSource.Play();

                musicSource.volume = volume;
            }

            PlayerPrefs.SetFloat("MusicVolume", volume);
            PlayerPrefs.Save();
        }
    }

    public void PlayMusic(AudioSource source)
    {
        if (musicSource == null || source == null) return;

        if (musicSource.clip == source) return;

        musicSource.Stop();

        float volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        if (volume > 0.001f)
        {
            musicSource.volume = volume;
            musicSource.Play();
        }
    }
}
