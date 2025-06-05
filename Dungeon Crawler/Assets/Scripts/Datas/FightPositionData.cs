using UnityEngine;

[CreateAssetMenu(fileName = "FightPosition", menuName = "ScriptableObject/FightPosition")]
public class FightPositionData : ScriptableObject
{
    public BarrierGrid.BarrierState[] barrierState = new BarrierGrid.BarrierState[3];
    public GameObject[] enemyArray = new GameObject[4];
}
