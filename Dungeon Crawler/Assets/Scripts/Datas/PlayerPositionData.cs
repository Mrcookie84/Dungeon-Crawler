using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPosition", menuName = "ScriptableObject/PlayerPosition")]
public class PlayerPositionData : ScriptableObject
{
    public GameObject[] playerPrefabs = new GameObject[3];
    public int[] currentPos = new int[3];
}
