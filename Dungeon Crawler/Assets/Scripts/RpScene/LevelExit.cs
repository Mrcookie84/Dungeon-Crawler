using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelExit : MonoBehaviour
{
    
    [Header("Links :")]
    [SerializeField] public GameObject group;
    public List<GameObject> spawnPoints;
    public List<GameObject> levelParent;
    
    
    private bool _canContinue = false;


    private void Start()
    {
        foreach (var iGameObject in levelParent)
        {
            iGameObject.SetActive(false);
        }
            
        levelParent[0].SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Group"))
        {
            _canContinue = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D other )
    {
        if (other.gameObject.CompareTag("Group"))
        {
            _canContinue = false;
        }
    }

    public void BoutonUse(int boutonID)
    {
        if (boutonID >= 0 && boutonID < spawnPoints.Count)
        {
            foreach (var iGameObject in levelParent)
            {
                iGameObject.SetActive(false);
            }
            
            levelParent[boutonID].SetActive(true);
            
            group.transform.position = spawnPoints[boutonID].transform.position;
        }
        else
        {
            Debug.LogWarning("BoutonID hors limites !");
        }
    }
    
    
}
