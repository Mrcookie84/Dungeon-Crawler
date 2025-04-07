using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigidbody;
    public float speed = 10f;
    
    private KeyCode fwKey = KeyCode.D;
    private KeyCode BwKey = KeyCode.A;


    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        Vector2 force = new Vector2(0, 0);
        
        if (Mathf.Approximately(rigidbody.gravityScale, 1))
        {
            
            if (Input.GetKey(fwKey))
            {
                force += Vector2.right * speed;
                targetRotation = Quaternion.Euler(0, 0, 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f);
                animator.SetFloat("Speed", 1);
            }
        
            if (Input.GetKey(BwKey))
            {
                force += Vector2.left * speed;
                targetRotation = Quaternion.Euler(0, 180, 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f);
                animator.SetFloat("Speed", 1);
            }
        }


        else if (Mathf.Approximately(rigidbody.gravityScale, -1))
        {
            
            if (Input.GetKey(fwKey))
            {
                force += Vector2.right * speed;
                targetRotation = Quaternion.Euler(180, 0, 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f);
                animator.SetFloat("Speed", 1);
            }
        
            if (Input.GetKey(BwKey))
            {
                force += Vector2.left * speed;
                targetRotation = Quaternion.Euler(180, 180, 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f);
                animator.SetFloat("Speed", 1);
            }
        }
        if (rigidbody.velocity.x==0)
        {
            animator.SetFloat("Speed", 0);
        }
        rigidbody.velocity = new Vector2(force.x, rigidbody.velocity.y);
     


    }
    
}
