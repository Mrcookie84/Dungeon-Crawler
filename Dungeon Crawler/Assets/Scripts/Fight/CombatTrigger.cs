using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTrigger : MonoBehaviour
{
    [Header("Fight state")] [SerializeField]
    private FightPositionData posData;
    //Faire en sorte de pouvoir set les ennemies utiliser dans le combat sur les trigger 
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(EnterCombatRoutine());
        }
    }


    IEnumerator EnterCombatRoutine()
    {
        // Initialisation du combat
        PositionManager.posData = posData;
        PositionManager.FillGrids();
        RuneSelection.InitializeStats();
        EnemyAIControler.UpdateAllMasks();

        SceneManager.GoToFight();
        TurnManager.InitializeTurn();
        gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);
    }

}