using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldSwitch : MonoBehaviour
{
    public GameObject interactBubble;
    
    [Header("Destination du TP")]
    public GameObject destination;

    [Header("Overworld ???")] 
    public bool overworld;

    [SerializeField] public List<Rigidbody2D> playersRb;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactBubble.SetActive(true);
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactBubble.SetActive(false);
        }
    }

    private void Start()
    {
        interactBubble.SetActive(false);
    }

    private void Update()
    {
        if (interactBubble.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            for (var i = 0; i < playersRb.Count; i++)
            {
                var playerRb = playersRb[i];
                playerRb.position = new Vector2(destination.transform.position.x - i * 2.0f , destination.transform.position.y);
                

                if (overworld == true)
                {
                    playerRb.gravityScale = -1;
                    playerRb.transform.rotation = Quaternion.RotateTowards(playerRb.transform.rotation, Quaternion.Euler(180, 0, 0), 360f);
                }
                else if (overworld == false)
                {
                    playerRb.gravityScale = 1;
                    playerRb.transform.rotation = Quaternion.RotateTowards(playerRb.transform.rotation, Quaternion.Euler(0, 0, 0), 360f);
                }
            }
        }
    }
}
