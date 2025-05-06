using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;

    public List<Transform> characterToLerpList;
    public List<Transform> targetPosList;
    
    public float speed = 10f;
    
    private KeyCode fwKey = KeyCode.D;
    private KeyCode BwKey = KeyCode.A;


    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = ((Component)this).transform.rotation;
    }

    void Update()
    {
        Vector2 force = new Vector2(0, 0);
        
        if (Input.GetKey(fwKey))
        {
            force += Vector2.right * speed;
            targetRotation = Quaternion.Euler(0, 0, 0);
            rigidbody2D.transform.rotation = Quaternion.RotateTowards(((Component)this).transform.rotation, targetRotation, 360f);
            
            for (int i = 0; i < characterToLerpList.Count; i++)
            {
                characterToLerpList[i].rotation = Quaternion.RotateTowards(((Component)this).transform.rotation, targetRotation, 360f);
            }
            
        }
    
        if (Input.GetKey(BwKey))
        {
            force += Vector2.left * speed;
            targetRotation = Quaternion.Euler(0, 180, 0);
            rigidbody2D.transform.rotation = Quaternion.RotateTowards(((Component)this).transform.rotation, targetRotation, 360f);

            for (int i = 0; i < characterToLerpList.Count; i++)
            {
                characterToLerpList[i].rotation = Quaternion.RotateTowards(((Component)this).transform.rotation, targetRotation, 360f);
            }
            
        }


        for (var i = 0; i < targetPosList.Count; i++)
        {
            characterToLerpList[i].position = Vector3.Lerp(targetPosList[i].position, targetPosList[i].position, Time.deltaTime);
        }
        
        
        rigidbody2D.velocity = new Vector2(force.x, rigidbody2D.velocity.y);
        
    }
}
