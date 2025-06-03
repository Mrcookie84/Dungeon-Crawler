using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiomanagerMenu : MonoBehaviour
{
    public AudioSource menuMusicSource;
    private AudioManager AudioManager;

    private void Start()
    {
        menuMusicSource.Play();
        AudioManager.SINGLETON.musicSource.Stop();
    }

    private void SwitchMusic()
    {
        menuMusicSource.Stop();
        AudioManager.SINGLETON.musicSource.Play();
    }
}
