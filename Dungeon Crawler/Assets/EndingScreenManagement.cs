using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScreenManagement : MonoBehaviour
{
    public void Awake()
    {
        SceneManager.EndingScene = this.gameObject;
    }

    public void RetourAlaCasba()
    { 
        Application.Quit();
        Debug.LogWarning("SUPOSSED TO QUICK");
    }
}
