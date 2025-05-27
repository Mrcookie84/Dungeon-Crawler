using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public KeybindManager KeybindManager;

    public enum PlayerState
    {
        idle,
        walking,
        interact
    };

    [SerializeField] public PlayerState playerState = PlayerState.idle;
    private Animator animatorComponent;


    void Start()
    {
        animatorComponent = GetComponent<Animator>();
        if (animatorComponent == null)
        {
            Debug.LogWarning("No animator detected on" + gameObject.name);
        }
    }


    void Update()
    {
        if (Input.GetKey(KeybindManager.SINGLETON.GetKey("Right")) == false ||
            (Input.GetKey(KeybindManager.SINGLETON.GetKey("Left")) == false))
        {
            playerState = PlayerState.idle;
        }

        if (Input.GetKey(KeybindManager.SINGLETON.GetKey("Right")) ||
            (Input.GetKey(KeybindManager.SINGLETON.GetKey("Left"))))
        {
            playerState = PlayerState.walking;
        }

        switch (playerState)
        {
            case PlayerState.idle:
                animatorComponent.SetBool("isIdle", true);

                animatorComponent.SetBool("isWalking", false);

                animatorComponent.SetBool("isInteracting", false);
                break;

            case PlayerState.walking:
                animatorComponent.SetBool("isIdle", false);

                animatorComponent.SetBool("isWalking", true);

                animatorComponent.SetBool("isInteracting", false);
                break;

            case PlayerState.interact:

                animatorComponent.SetBool("isIdle", false);

                animatorComponent.SetBool("isWalking", false);

                animatorComponent.SetBool("isInteracting", true);
                break;

            default:
                // code block
                break;
        }
    }
}