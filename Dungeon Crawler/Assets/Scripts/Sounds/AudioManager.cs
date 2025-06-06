using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager SINGLETON;

    public AudioSource menuMusic;
    public Image fadeImage;

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
        StartCoroutine(WaitForFade());
    }

    private void Start()
    {
        StartMenuMusic();
        

    }

    IEnumerator WaitForFade()
    {
        fadeImage.DOFade(0f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        fadeImage.gameObject.SetActive(false);
    }

    public IEnumerator TransitionMusic(AudioSource from, AudioSource to)
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(1f, 0.5f);
        from.DOFade(0f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        from.Stop();
        to.volume = 0;
        to.Play();
        to.DOFade(1f, 0.5f);
        fadeImage.DOFade(0f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        fadeImage.gameObject.SetActive(false);
    }

    public void StartMenuMusic() => menuMusic.Play();
}