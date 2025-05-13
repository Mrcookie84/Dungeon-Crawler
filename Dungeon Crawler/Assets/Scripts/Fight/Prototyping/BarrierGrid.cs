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
        Animator animator = transform.GetChild(column).GetComponent<Animator>();
        if (newState == BarrierState.Destroyed)
        {
            animator.SetTrigger("destruction");
            animator.SetBool("isDestroyed", true);
            //transform.GetChild(column).gameObject.SetActive(false);
            
        }
        else
        {
            animator.SetTrigger("reconstruction");
            animator.SetBool("isDestroyed", false);
            //transform.GetChild(column).gameObject.SetActive(true);
        }
    }
}
