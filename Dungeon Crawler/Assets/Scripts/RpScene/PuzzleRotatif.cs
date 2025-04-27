using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PuzzleRotatif : MonoBehaviour
{
    [Header("Tab interaction sprite :")]
    public GameObject tabInteractionIcon;
    
    [Header("Interact bubble sprite :")]
    [SerializeField] public GameObject interactBubbleIcon;
    
    [Header("Door :")] 
    [SerializeField] public GameObject door;
    
    
    [Header("LES GD , C'EST ICI QU'IL FAUT MODIFIER LE MDP : ")]
    [Header("mot de passe (1/2/3) :")]
    [SerializeField] public int mdpPart1;
    [SerializeField] public int mdpPart2;
    [SerializeField] public int mdpPart3;
    
    
    [Header("Pas touche a cette liste :")]
    public List<SpriteRenderer> SpritesRenderers = new List<SpriteRenderer>();
    [Header("Mettre les sprite propre ici :")]
    public List<Sprite> Sprites = new List<Sprite>();
    private List<int> curentState = new List<int>();
    
    
    private int selector = 1;
    
    private void Start()
    {
        tabInteractionIcon.SetActive(false);
        interactBubbleIcon.SetActive(false);
        

        SpritesRenderers[0].sprite = Sprites[0];
        SpritesRenderers[1].sprite = Sprites[0];
        SpritesRenderers[2].sprite = Sprites[0];

        foreach (Sprite i in Sprites)
        {
            curentState.Add(1);
        }

    }

    private void Update()
    {
        
        if (interactBubbleIcon.activeSelf)
        {
            SelectorChange();

            ChangeActualBubble();
        }
        

        if ((mdpPart1 == curentState[0]) && (mdpPart2 == curentState[1]) && (mdpPart3 == curentState[2]))
        {
            door.SetActive(false);
        }
        else
        {
            door.SetActive(true);
        }
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tabInteractionIcon.SetActive(true);
            interactBubbleIcon.SetActive(true);
            UpdateInteractBubble();
            
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tabInteractionIcon.SetActive(false);
            interactBubbleIcon.SetActive(false);
        }
    }
    
    private void ChangeActualBubble()
    {
        if (selector == 1 && Input.GetKeyDown(KeyCode.E))
        {
            if (curentState[0] == 1)
            {
                SpritesRenderers[0].sprite = Sprites[1];
                curentState[0] = 2;
            }
            else if (curentState[0] == 2)
            {
                SpritesRenderers[0].sprite = Sprites[2];
                curentState[0] = 3;
            }
            else if (curentState[0] == 3)
            {
                SpritesRenderers[0].sprite = Sprites[0];
                curentState[0] = 1;
            }
        }
        else if (selector == 2 && Input.GetKeyDown(KeyCode.E))
        {
            if (curentState[1] == 1)
            {
                SpritesRenderers[1].sprite = Sprites[1];
                curentState[1] = 2;
            }
            else if (curentState[1] == 2)
            {
                SpritesRenderers[1].sprite = Sprites[2];
                curentState[1] = 3;
            }
            else if (curentState[1] == 3)
            {
                SpritesRenderers[1].sprite = Sprites[0];
                curentState[1] = 1;
            }
        }
        else if (selector == 3 && Input.GetKeyDown(KeyCode.E))
        {
            if (curentState[2] == 1)
            {
                SpritesRenderers[2].sprite = Sprites[1];
                curentState[2] = 2;
            }
            else if (curentState[2] == 2)
            {
                SpritesRenderers[2].sprite = Sprites[2];
                curentState[2] = 3;
            }
            else if (curentState[2] == 3)
            {
                SpritesRenderers[2].sprite = Sprites[0];
                curentState[2] = 1;
            }
        }
    }

    void SelectorChange()
    {
        if (interactBubbleIcon.activeSelf && Input.GetKeyDown(KeyCode.Tab))
        {
            if (selector < 3)
            {
                selector++;
                UpdateInteractBubble();
            }
            else
            {
                selector = 1;
                UpdateInteractBubble();
            }
            
        }
        
        
    }

    void UpdateInteractBubble()
    {
        if (selector == 1)
        {
            interactBubbleIcon.transform.localPosition = new Vector3(-1.36f, 2.68f, -1);
        }
        else if (selector == 2)
        {
            interactBubbleIcon.transform.localPosition = new Vector3(1.01f, 2.11f, -1);
        }
        else if (selector == 3)
        {
            interactBubbleIcon.transform.localPosition = new Vector3(-0.69f, 0.72f, -1);
        }
    }
    
    
}
