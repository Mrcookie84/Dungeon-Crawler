using UnityEngine;

public class BarrierGrid : MonoBehaviour
{
    public enum BarrierState
    {
        Destroyed,
        Reinforced
    }

    public BarrierState[] barrierStates;

    public BarrierState CheckBarrierState(int column)
    {
        return barrierStates[column];
    }

    public void ChangeBarrierState(int column, BarrierState newState)
    {
        barrierStates[column] = newState;
    }
}
