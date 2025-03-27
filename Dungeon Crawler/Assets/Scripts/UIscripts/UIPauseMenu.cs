using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPauseMenu : MonoBehaviour
{
    
    [SerializeField] public GameObject menu;
    
    private bool _menuOpen;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.SceneFight.activeSelf == false)
        {
            OpenCloseMenu();
        }
    }

    private void Start()
    {
        menu.SetActive(false);
    }

    public void OpenCloseMenu()
    {
        
        _menuOpen = !_menuOpen;
        menu.SetActive(_menuOpen);

        if (_menuOpen == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        
    }
    
    
    
    
    public void ReloadScene()
    {
        Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
    }
    
    public void Quit()
    {
        OpenCloseMenu();
        Time.timeScale = 1;
        Debug.Log("supposed to quit");
        Application.Quit();
    }
    
}
