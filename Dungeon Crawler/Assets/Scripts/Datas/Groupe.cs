using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Groupe")]

public class Groupe : ScriptableObject
{
    [SerializeField]
    public List<MonsterData> entityList = new List<MonsterData>();
    [SerializeField]
    public List<PlayerData> playerList = new List<PlayerData>();
    [SerializeField]
    public List<int> positionList = new List<int>();
}
