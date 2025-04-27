using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneManager : MonoBehaviour
{
    public static GameObject StartingScene;
    public static GameObject SceneFight;
    
    public static SceneManager SceneManagerInstance;
    public CombatTrigger combatTrigger;
    public MapUIManagerScript mapUIManager;
    
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

}
