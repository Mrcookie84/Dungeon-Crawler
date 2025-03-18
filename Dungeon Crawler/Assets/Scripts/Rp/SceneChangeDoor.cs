using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneChangeDoor : MonoBehaviour
{
    [Header("Temporary solution to unlock :")] 
    [SerializeField] public GameObject solution;
    
    
    [Header("Door Sprite :")]
    [SerializeField] public GameObject doorClosed;
    [SerializeField] public GameObject doorOpened;
    
    [Header("Others :")]
    [SerializeField] public GameObject interactBubble;
    [SerializeField] public BoxCollider2D triggerBox;
    [SerializeField] public GameObject mapUI;

    [Header("Scene to go :")]
    [SerializeField] public int sceneNumber = 0;

    


    private void Start()
    {
        doorOpened.SetActive(false);
        interactBubble.SetActive(false);
        triggerBox.enabled = false;
        doorClosed.SetActive(true);
    }

    private void Update()
    {

        if (solution.activeSelf == false)
        {
            doorClosed.SetActive(false);
            doorOpened.SetActive(true);
            triggerBox.enabled = true;
        }
        else
        {
            doorClosed.SetActive(true);
            doorOpened.SetActive(false);
            triggerBox.enabled = false;
        }

        if (interactBubble.activeSelf && doorOpened.activeSelf && Input.GetKeyDown(KeyCode.E) && solution.activeSelf == false)
        {

            SceneManager.GoToFight();

        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && triggerBox.enabled == true)
        {
            interactBubble.SetActive(true);
        }
    }
    
    void OnTriggerExit2D(Collider2D other )
    {
        if (other.gameObject.CompareTag("Player") && triggerBox.enabled == true)
        {
            interactBubble.SetActive(false);
        }
    }
}
