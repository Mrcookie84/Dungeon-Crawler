using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPauseMenu : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] public GameObject menu;
    public GameObject menuBouton;
    private bool _menuOpen;
    
    [Header("Inventory")]
    [SerializeField] public GameObject inventory;
    public List<GameObject> boutonToDisable1 = new List<GameObject>();
    private bool _inventory;
    
    //
    //Map here ??
    //
    
    [Header("Parameter")]
    [SerializeField] public GameObject parameter;
    public List<GameObject> boutonToDisable3 = new List<GameObject>();
    private bool _parameter;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _inventory== false && _parameter == false)
        {
            OpenCloseMenu();
        }
    }

    private void Start()
    {
        menu.SetActive(false);
        menuBouton.SetActive(true);
    }

    public void OpenCloseMenu()
    {
        menuBouton.SetActive(_menuOpen);
        _menuOpen = !_menuOpen;
        menu.SetActive(_menuOpen);

    }
    
    public void OpenCloseInventory()
    {
        
        _inventory = !_inventory;
        inventory.SetActive(_inventory);

        foreach (var iGameObject in boutonToDisable1)
        {
            iGameObject.SetActive(!_inventory);
        }
        
    }
    
    public void OpenCloseParameter()
    {
        
        _parameter = !_parameter;
        parameter.SetActive(_parameter);
        
        foreach (var iGameObject in boutonToDisable3)
        {
            iGameObject.SetActive(!_parameter);
        }

    }
    
    
    public void ReloadScene()
    {
        Time.timeScale = 1;
        Debug.Log("Reload de la scene");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0,LoadSceneMode.Single);
    }
    
    public void Quit()
    {
        OpenCloseMenu();
        Time.timeScale = 1;
        Debug.Log("supposed to quit");
        Application.Quit();
    }
    
}
