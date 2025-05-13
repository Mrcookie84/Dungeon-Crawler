using UnityEngine;

public class BarrierGrid : MonoBehaviour
{
    public Animator animator;
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
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
             animator.SetTrigger("destruction");
        }
    }
    public void ChangeBarrierState(int column, BarrierState newState)
    {
        barrierStates[column] = newState;
        if (newState == BarrierState.Destroyed)
        {
            animator.SetTrigger("destruction");
            //transform.GetChild(column).gameObject.SetActive(false);
            
        }
        else
        {
            transform.GetChild(column).gameObject.SetActive(true);
        }
    }
}
