using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


public class DecorsMovement : MonoBehaviour
{
    public KeybindManager KeybindManager;
    public Rigidbody2D RB;
    public float speed = 10f;


    private Quaternion targetRotation;
    
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offSett;


    void Update()
    {
        Vector2 force = new Vector2(0, 0);

        if (Input.GetKey(KeybindManager.SINGLETON.GetKey("Right")))
        {
            force += Vector2.right * speed;
            targetRotation = Quaternion.Euler(0, 0, 0);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f);
        }

        if (Input.GetKey(KeybindManager.SINGLETON.GetKey("Left")))
        {
            force += Vector2.left * speed;
            targetRotation = Quaternion.Euler(0, 180, 0);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f);
        }
        RB.velocity = new Vector2(force.x, RB.velocity.y);
    }
}
