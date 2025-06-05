using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFightStarter : MonoBehaviour
{
    public bool startFightOnLoad;
    
    [Header("Fight Data")]
    public FightPositionData posData;
    public GameObject eventSystem;

    private void Awake()
    {
        EntityHealth.InitializeCurrentHealth();
    }

    void Start()
    {
        if (startFightOnLoad)
        {
            eventSystem.SetActive(true);
            
            // Initialisation du combat
            PositionManager.posData = posData;
            PositionManager.FillGrids();
            RuneSelection.InitializeStats();
            EnemyAIControler.UpdateAllMasks();

        }
    }
}
