using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static GameObject sceneRP;
    public static GameObject sceneFight;
    
    public void Awake()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1, LoadSceneMode.Additive);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    public void Start()
    {
        GoToRP();
    }

    public static void GoToRP()
    {
        sceneRP.SetActive(true);
        sceneFight.SetActive(false);
    }
    
    public static void GoToFight()
    {
        sceneFight.SetActive(true);
        sceneRP.SetActive(false);
        
    }
}
