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
    
    [Header("Others :")]
    [SerializeField] public GameObject interactBubble;
    [SerializeField] public BoxCollider2D triggerBox;
    [SerializeField] public RectTransform mapUI;

    


    private void Start()
    {
        interactBubble.SetActive(false);
        //triggerBox.enabled = false;
    }

    private void Update()
    {

        if (interactBubble.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            
            mapUI.DOAnchorPosY(21, 1, false).SetEase(Ease.InCubic);

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
