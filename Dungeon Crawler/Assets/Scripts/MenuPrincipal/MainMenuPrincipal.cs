using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPrincipal : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(WaitForLoading());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitForLoading()
    {
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
