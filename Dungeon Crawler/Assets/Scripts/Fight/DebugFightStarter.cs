using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFightStarter : MonoBehaviour
{
    public bool startFightOnLoad;
    
    [Header("Fight Data")]
    public FightPositionData posData;
    public GameObject eventSystem;
    
    void Start()
    {
        if (startFightOnLoad)
        {
            eventSystem.SetActive(true);
            
            // Initialisation du combat
            if (PositionManager.Instance == null) Debug.LogError("Kill yourself");
            PositionManager.posData = posData;
            PositionManager.FillGrids();
        }
    }
}
