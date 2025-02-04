using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    
    public float Speed = 4000.0f;

    private KeyCode fwKey = KeyCode.D;
    private KeyCode BwKey = KeyCode.A;

    void Update()
    { 
        
        Vector3 force = new Vector3(0, 0, 0);
        
        if (Input.GetKey(fwKey))
        {
            force += Vector3.right * Speed;
            transform.eulerAngles = new Vector2(0,0);
        }
        
        if (Input.GetKey(BwKey))
        {
            force += Vector3.left * Speed;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        
        force = force.normalized;
        Rigidbody.velocity = force;
    }
}