using UnityEngine;

[CreateAssetMenu(fileName = "FightPosition", menuName = "ScriptableObject/FightPosition")]
public class FightPositionData : ScriptableObject
{
    public GameObject[] playerArray = new GameObject[6];
    public GameObject[] enemyArray = new GameObject[4];
}
