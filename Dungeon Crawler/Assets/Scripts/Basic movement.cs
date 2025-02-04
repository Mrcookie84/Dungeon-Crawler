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
    
    /*
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;
    [SerializeField]private bool isGrounded = false;
    public Transform checkSol;
    private float rayonSol = 0.3f;
    [SerializeField] private LayerMask sol;
    private Animator animatorRef;
    [SerializeField] private float timeForJumpAnim ;
    private float count;
    */

    /*
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animatorRef = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(checkSol.position, rayonSol, sol);
    }
    */

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





        /*
        count += 0.001f;
        if (isGrounded && count > 0.1 )
        {
            animatorRef.SetBool("isJumping", false);
        }
        float x = Input.GetAxis("Horizontal");
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            count = 0;
            animatorRef.Play("takeOff");
            animatorRef.SetBool("isJumping", true);
            rb.AddForce(new Vector2(0,250));
        }


        if (x > 0)
        {
            transform.Translate(x * moveSpeed * Time.deltaTime,0,0);
            transform.eulerAngles = new Vector2(0,0);
            animatorRef.SetBool("isRunning", true);
        }

        else if (x < 0)
        {
            transform.Translate(-x * moveSpeed * Time.deltaTime,0,0);
            transform.eulerAngles = new Vector3(0, 180, 0);
            animatorRef.SetBool("isRunning", true);
        }
        else
        {
            animatorRef.SetBool("isRunning", false);
        }

        */


    }
}