using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uiparametre : MonoBehaviour
{
    public static Uiparametre SINGLETON;

    private void Awake()
    {
        if (SINGLETON == null)
        {
            SINGLETON = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}