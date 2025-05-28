using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPauseMenu : MonoBehaviour
{
    
    

    #region PartieDeDroiteDeclaration

        [Header("Partie De Droite : ")]
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

    #endregion

    #region PartieDeGaucheDeclaration

        [Header("Partie De Gauche : ")]
        [Header("Renard")]
        [SerializeField] public GameObject renardPersonalInventory;
        public List<GameObject> boutonToDisableRenard = new List<GameObject>();
        private bool _renardPersonalInventoryState = false;
        
        /*
        [Header("Panda")]
        [SerializeField] public GameObject inventory;
        public List<GameObject> boutonToDisablePanda = new List<GameObject>();
        private bool _pandaPersonalInventoryState;
        */
        
        [Header("Parameter")]
        [SerializeField] public GameObject grenouillePersonalInventory;
        public List<GameObject> boutonToDisableGrenouille = new List<GameObject>();
        private bool _grenouillePersonalInventoryState = false;

    #endregion
    
    
    private void Start()
    {
        menu.SetActive(false);
        menuBouton.SetActive(true);
        
        renardPersonalInventory.SetActive(false);
        grenouillePersonalInventory.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _inventory== false && _parameter == false)
        {
            OpenCloseMenu();
        }

        if (Input.GetKeyDown(KeybindManager.SINGLETON.GetKey("Inventaire")))
        {
            OpenCloseInventory();
        }

        if (Input.GetKeyDown(KeybindManager.SINGLETON.GetKey("Renard")))
        {
            OpenCloseRenardPersonalInventory();
        }

        if (Input.GetKeyDown(KeybindManager.SINGLETON.GetKey("Grenouille")))
        {
            OpenCloseGrenouillePersonalInventory();
        }
    }
    
    #region UIdeDroite
        
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
    #endregion

    #region UIdeGauche

        public void OpenCloseRenardPersonalInventory()
        {
            {
                _renardPersonalInventoryState = !_renardPersonalInventoryState;
                renardPersonalInventory.SetActive(_renardPersonalInventoryState);

                foreach (var iGameObject in boutonToDisableRenard)
                {
                    iGameObject.SetActive(!_renardPersonalInventoryState);
                }
            }
        }
        
        public void OpenCloseGrenouillePersonalInventory()
        {
            {
                _grenouillePersonalInventoryState = !_grenouillePersonalInventoryState;
                grenouillePersonalInventory.SetActive(_grenouillePersonalInventoryState);

                foreach (var iGameObject in boutonToDisableGrenouille)
                {
                    iGameObject.SetActive(!_grenouillePersonalInventoryState);
                }
            }
        }

    #endregion
    

    

    #region General
    
        private GUIStyle SetFontStyle()
        {
            GUIStyle currentStyle = new GUIStyle();
            currentStyle.fontSize = 10;
            currentStyle.fontStyle = FontStyle.Bold;
            currentStyle.normal.textColor = Color.yellow;

            return currentStyle;

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
        
    #endregion
    
    
}
