using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LeverScript : MonoBehaviour
{
    public GameObject lever;
    public GameObject door;
    public GameObject doorOpened;
    public GameObject interactBubble;
    private KeyCode interactKey = KeyCode.E;
    
    public Animator animatorComponent;

    private void Start()
    {
        door.SetActive(true);
        doorOpened.SetActive(false);
        interactBubble.SetActive(false);
        
        animatorComponent = GetComponent<Animator>();
        if (animatorComponent == null)
        {
            Debug.LogWarning("No animator detected on" + gameObject.name);
<<<<<<< HEAD
<<<<<<< HEAD
            
=======
>>>>>>> f746321 (Bug fix)
=======
>>>>>>> origin/gd/Thomas
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactBubble.SetActive(true);
            
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactBubble.SetActive(false);
            
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey) && interactBubble.activeInHierarchy)
        {
            if (door.activeSelf == true)
            {
                
                doorOpened.SetActive(true);
                door.SetActive(false);

                StartCoroutine(LeverAnimationCorout());
            }
            else if (door.activeSelf == false)
            {
                
                doorOpened.SetActive(false);
                door.SetActive(true);
                
                StartCoroutine(LeverAnimationCorout());
            }
            
        }
    }

    private IEnumerator LeverAnimationCorout()
    {
        
        animatorComponent.Play("levier");
        yield return new WaitForSeconds(1f);
        lever.transform.Rotate(0, 180, 0);
        
    }
    
    
}
