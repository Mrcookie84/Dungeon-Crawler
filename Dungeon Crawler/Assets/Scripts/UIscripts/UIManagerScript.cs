using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour
{
    
    
    [Header("Menu")]
    [SerializeField] public GameObject menu;
    [SerializeField] public GameObject menuButton;
    private bool _menuOpen;
    
    
    [Header("Inventory in menu")]
    [SerializeField] public GameObject inventory;
    public List<GameObject> buttonToDisable1 = new List<GameObject>();
    private bool _inventory;
    
    //
    //Map here ???
    //
    
    [Header("Parameter in menu")]
    [SerializeField] public GameObject parameterMenu;
    public List<GameObject> buttonToDisable3 = new List<GameObject>();
    private bool _parameterMenu;
    
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.Escape) && _inventory == false && _parameterMenu == false)
        {
            menuButton.SetActive(!_menuOpen);
            OpenCloseMenu();
        }
    }

    private void Start()
    {
        menu.SetActive(false);
        menuButton.SetActive(true);
    }

    public void OpenCloseMenu()
    {
        menuButton.SetActive(_menuOpen);
        _menuOpen = !_menuOpen;
        menu.SetActive(_menuOpen);
        
    }
    
    public void OpenCloseInventory()
    {
        
        _inventory = !_inventory;
        inventory.SetActive(_inventory);

        
        foreach (var iGameObject in buttonToDisable1)
        {
            iGameObject.SetActive(!_inventory);
        }
    }
    
    public void OpenCloseParameter()
    {
        
        _parameterMenu = !_parameterMenu;
        parameterMenu.SetActive(_parameterMenu);
        
        foreach (var iGameObject in buttonToDisable3)
        {
            iGameObject.SetActive(!_parameterMenu);
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
