using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public int playerID;
    public WorldSwitch thisTp;
    public Animator animatorComponent;

    private void OnMouseUpAsButton()
    {
        
        if (thisTp.canTp == true)
        {

            if (playerID == 1)
            {
                animatorComponent.Play("interaction_renard");
            }
            else if (playerID == 2)
            {
                Debug.Log("Interact Panda");
            }
            else if(playerID == 3)
            {
                animatorComponent.Play("interaction_grenouille");
            }

            StartCoroutine(InteractCorout());

        }
    }


    IEnumerator InteractCorout()
    {

        yield return new WaitForSeconds(1f);
        
        if (Mathf.Approximately(rigidbody.gravityScale, 1))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - thisTp.tpDistance, transform.position.z);
            transform.localRotation = new Quaternion(180, transform.localRotation.y, 0, 0);
            rigidbody.gravityScale = -1;
            Debug.Log("Player : " + playerID + " has been flipped to down");
        }
        else if (Mathf.Approximately(rigidbody.gravityScale, -1))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + thisTp.tpDistance, transform.position.z);
            transform.localRotation = new Quaternion(0, transform.localRotation.y, 0, 0);
            rigidbody.gravityScale = 1;
            Debug.Log("Player : " + playerID + " has been flipped to up");
        }
        
    }
    
}
