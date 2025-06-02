using UnityEngine;

public class BarrierGrid : MonoBehaviour
{
    public enum BarrierState
    {
        Destroyed,
        Reinforced
    }

    private BarrierState[] barrierStates = new BarrierState[3];

    public BarrierState GetBarrierState(int column)
    {
        return barrierStates[column];
    }

    public bool IsBarrierBroken(int column)
    {
        return barrierStates[column] == BarrierState.Destroyed;
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

    private void Awake()
    {
        for (int i = 0; i < barrierStates.Length; i++)
        {
            barrierStates[i] = BarrierState.Reinforced;
        }
    }
}
