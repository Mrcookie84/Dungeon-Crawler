using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldSwitch : MonoBehaviour
{
    
    
    public bool canTp = false;

    private void Awake()
    {
        
    }

    private void Start()
    {
        canTp = false;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Group"))
        {
            canTp = true;
            
            Debug.Log("Tp has collide with group");
            
            foreach (Transform child in other.transform)
            {
                if (child.GetComponent<PlayerDetection>() != null)
                {
                    child.GetComponent<PlayerDetection>().thisTp = this;
                }
            }
            
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Group"))
        {
            canTp = false;
        }
    }
    
}
