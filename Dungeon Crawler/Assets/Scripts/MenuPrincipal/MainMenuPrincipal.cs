using System.Collections;
using DG.Tweening;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class MainMenuPrincipal : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator StartGameRoutine()
    {
        AudioManager.SINGLETON.fadeImage.gameObject.SetActive(true);
        AudioManager.SINGLETON.fadeImage.DOFade(1f, 0.5f);
        yield return new WaitForSeconds(0.5f);

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}