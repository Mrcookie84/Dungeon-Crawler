using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    
    public enum PlayerState
    {
        idle,
        walking,
        interact
    };

    [SerializeField] public PlayerState playerState = PlayerState.idle;
    private Animator animatorComponent;
    
    private KeyCode fwKey = KeyCode.D;
    private KeyCode BwKey = KeyCode.A;
    
    // Start is called before the first frame update
    void Start()
    {

        animatorComponent = GetComponent<Animator>();
        if (animatorComponent == null)
        {
            Debug.LogWarning("No animator detected on" + gameObject.name);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(fwKey) == false && Input.GetKey(BwKey) == false)
        {

            playerState = PlayerState.idle;

        }
    
        if (Input.GetKey(fwKey) == true || Input.GetKey(BwKey) == true)
        {

            playerState = PlayerState.walking;

        }
        
        switch(playerState) 
        {
            case PlayerState.idle:
                animatorComponent.SetBool("isIdle" , true);
                
                animatorComponent.SetBool("isWalking", false);
                
                animatorComponent.SetBool("isInteracting" , false);
                break;
            case PlayerState.walking:
                animatorComponent.SetBool("isIdle" , false);
                
                animatorComponent.SetBool("isWalking", true);
                
                animatorComponent.SetBool("isInteracting" , false);
                break;
            case PlayerState.interact:
                
                animatorComponent.SetBool("isIdle" , false);
                
                animatorComponent.SetBool("isWalking", false);
                
                animatorComponent.SetBool("isInteracting" , true);
                break;

            default:
                // code block
                break;
        }
    }
}
