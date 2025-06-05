using UnityEngine;

public class BarrierGrid : MonoBehaviour
{
    public static BarrierGrid Grid;
    
    public enum BarrierState
    {
        Destroyed,
        Reinforced
    }

    private BarrierState[] barrierStates = new BarrierState[3];

    
    
    public static BarrierState GetBarrierState(int column)
    {
        return Grid.barrierStates[column];
    }

    public static bool IsBarrierBroken(int column)
    {
        return Grid.barrierStates[column] == BarrierState.Destroyed;
    }

    public static void ChangeBarrierState(int column, BarrierState newState)
    {
        Grid.barrierStates[column] = newState;
        Animator animator = Grid.transform.GetChild(column).GetComponent<Animator>();
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
        Grid = this;
        
        for (int i = 0; i < barrierStates.Length; i++)
        {
            barrierStates[i] = BarrierState.Reinforced;
        }
    }
}
