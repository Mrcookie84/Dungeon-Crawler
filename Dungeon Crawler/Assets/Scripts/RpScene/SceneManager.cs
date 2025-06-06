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
    public static GameObject StartingScene;
    public static GameObject SceneFight;
    
    public static SceneManager SceneManagerInstance;
    public CombatTrigger combatTrigger;
    
    public GameObject MainMenuCamera;
    
    public void Awake()
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(1, LoadSceneMode.Additive);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2, LoadSceneMode.Additive);
        
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
    
    private void StartTheGame()
    {
        StartingScene.SetActive(false);
        SceneFight.SetActive(false);
    }
    
    public void StartGame()
    {
        MainMenuCamera.SetActive(false);
        MainMenuScene.SetActive(false);
        GoToRP();
    }
    
    public static void GoToRP()
    {
        StartingScene.SetActive(true);
        SceneFight.SetActive(false);
    }
    
    public static void GoToFight()
    {
        SceneFight.SetActive(true);
        StartingScene.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    internal static void LoadScene(string nextSceneName)
    {
        throw new NotImplementedException();
    }
}
