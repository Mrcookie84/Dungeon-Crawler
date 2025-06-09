using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TittleManager : MonoBehaviour
{
    public void Awake()
    {
        //SceneManager.MainMenuScene = this.gameObject;
    }

    public void StartGameProbably()
    {
        SceneManager.SceneManagerInstance.StartGame();
    }
}
