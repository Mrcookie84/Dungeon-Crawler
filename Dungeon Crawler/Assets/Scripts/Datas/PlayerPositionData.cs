using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPosition", menuName = "ScriptableObject/PlayerPosition")]
public class PlayerPositionData : ScriptableObject
{
    public static PlayerPositionData Instance;

    public GameObject[] playerPrefabs = new GameObject[3];
    public int[] defaultPos = new int[3];

    [HideInInspector] public static int[] currentPos = new int[3];

    // Propriétés
    public static GameObject[] PlayerPrefabs
    {
        get { return Instance.playerPrefabs; }
    }

    // Méthodes
    public static void ResetPos()
    {
        currentPos = Instance.defaultPos;
    }

    private void OnValidate()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else if (Instance == null)
        {
            Instance = this;
        }

        ResetPos();
    }
}
