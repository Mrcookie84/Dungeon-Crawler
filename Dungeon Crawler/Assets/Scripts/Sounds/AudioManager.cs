using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private Image FadeImage;
    private void Start()
    {
        AudioSource.DOFade(1, 1f);
        FadeImage.DOFade(0, 1f);
        StartCoroutine(EndFade());
    }

    IEnumerator EndFade()
    {
        yield return new WaitForSeconds(1f);
        FadeImage.gameObject.SetActive(false);
    }

    public void FadeOff()
    {
        AudioSource.DOFade(0, 1f);
        FadeImage.gameObject.SetActive(true);
        FadeImage.DOFade(1, 1f);
    }
}
