using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager SINGLETON;

    public AudioMixer audioMixer;


    public AudioSource menuMusic;
    public AudioSource explorationMusic;

    void Start()
    {
        menuMusic.Play();
        explorationMusic.Stop();
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
    
    public void SwitchToExplorationMusic()
    {
        menuMusic.Stop();
        explorationMusic.Play();
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

}