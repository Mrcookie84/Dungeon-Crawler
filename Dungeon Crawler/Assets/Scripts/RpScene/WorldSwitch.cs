using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldSwitch : MonoBehaviour
{
    
    public bool canTp = false;
    public float tpDistance = 2.7f;
    
    
    private void Start()
    {
        canTp = false;
        if (tpDistance == 0)
        {
            Debug.LogWarning(" Attention la valeur de distance n'a pas été mise pour ce tp : " + this.gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Group") || other.gameObject.CompareTag("Player"))
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
