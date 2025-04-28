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
        if (newState == BarrierState.Destroyed)
        {
            transform.GetChild(column).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(column).gameObject.SetActive(true);
        }
    }
}
