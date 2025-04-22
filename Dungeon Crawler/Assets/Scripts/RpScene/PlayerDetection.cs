using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public int playerID;
    public WorldSwitch thisTp;
    
    private void Awake()
    {
        
    }

    void Start()
    {
        
    }


    private void OnMouseUpAsButton()
    {
        
        if (thisTp.canTp == true)
        {
            if (Mathf.Approximately(rigidbody.gravityScale, 1))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
                transform.localRotation = new Quaternion(180, transform.localRotation.y, 0, 0);
                rigidbody.gravityScale = -1;
                Debug.Log("Player : " + playerID + " has been flipped to down");
            }
            else if (Mathf.Approximately(rigidbody.gravityScale, -1))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
                transform.localRotation = new Quaternion(0, transform.localRotation.y, 0, 0);
                rigidbody.gravityScale = 1;
                Debug.Log("Player : " + playerID + " has been flipped to up");
            }
            
        }
    }
}
