using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneManager : MonoBehaviour
{
    public GameObject MainMenuScene;
    public static GameObject SceneRp;
    public static GameObject SceneFight;
    public static GameObject EndingScene;
    
    public static SceneManager SceneManagerInstance;
    
    public void Awake()
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(1, LoadSceneMode.Additive);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2, LoadSceneMode.Additive);
        UnityEngine.SceneManagement.SceneManager.LoadScene(3, LoadSceneMode.Additive);
        
        if (SceneManagerInstance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            SceneManagerInstance = this;
            DontDestroyOnLoad(this.transform.root.gameObject);
        }
        
    }

    public void Start()
    {
        StartTheGame();
    }

    public void Reload()
    {
        
    }
    
    private void StartTheGame()
    {
        MainMenuScene.SetActive(true);
        SceneRp.SetActive(false);
        SceneFight.SetActive(false);
        EndingScene.SetActive(false);
    }
    
    public void StartGame()
    {
        MainMenuScene.SetActive(false);
        GoToRP();
    }
    
    public static void GoToRP()
    {
        SceneRp.SetActive(true);
        SceneFight.SetActive(false);
    }
    
    public static void GoToFight()
    {
        SceneFight.SetActive(true);
        SceneRp.SetActive(false);
    }

    public static void GoToEnding()
    {
        SceneRp.SetActive(false);
        SceneFight.SetActive(false);
        EndingScene.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
