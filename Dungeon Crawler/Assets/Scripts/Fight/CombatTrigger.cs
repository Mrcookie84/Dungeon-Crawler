using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTrigger : MonoBehaviour
{
    [Header("Fight state")]
    [SerializeField] private FightPositionData posData;
    //Faire en sorte de pouvoir set les ennemies utiliser dans le combat sur les trigger 
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Group"))
        {
            SceneManager.GoToFight();
            
            // Initialisation du combat
            PositionManager.posData = posData;
            PositionManager.FillGrids();
            RuneSelection.InitializeStats();
            EnemyAIControler.UpdateAllMasks();
            
            Destroy(gameObject);
        }
    }
}
