using UnityEngine;

public class EntityFightAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private State currentState = State.Idle;
    
    public enum State
    {
        Idle,
        Attack,
        Hurt,
        Dead
    }

    public void ChangeState(State newState)
    {
        currentState = newState;
        
        UpdateAnim();
    }

    public void UpdateAnim()
    {
        switch (currentState)
        {
            case State.Attack:
                animator.SetTrigger("CAttaque");
                currentState = State.Idle;
                animator.SetBool("isIdle", true);
                break;
            
            case State.Hurt:
                animator.SetTrigger("CDegat");
                currentState = State.Idle;
                animator.SetBool("isIdle", true);
                break;
            
            case State.Dead:
                animator.SetBool("isDead", true);
                animator.SetBool("isIdle", false);
                break;
            
            default:
                animator.SetBool("isIdle", true);
                break;
        }
    }
}
