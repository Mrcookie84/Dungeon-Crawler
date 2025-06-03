using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPrincipal : MonoBehaviour
{
    
    void Start()
    {
        if (AudioManager.SINGLETON != null)
            AudioManager.SINGLETON.PlayMusic(AudioManager.SINGLETON.menuMusic);
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
