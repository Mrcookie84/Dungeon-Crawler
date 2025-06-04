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


    private void Start()
    {
        door.SetActive(true);
        doorOpened.SetActive(false);
        interactBubble.SetActive(false);
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
                
                lever.transform.Rotate(0, 180, 0);
            }
            else
            {
                
                doorOpened.SetActive(false);
                door.SetActive(true);
                
                lever.transform.Rotate(0, 180, 0);
            }
            
        }
    }
}
