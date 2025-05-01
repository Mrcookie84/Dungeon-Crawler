using UnityEngine;

public class EntityFightAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip idleAnim;
    [SerializeField] private AnimationClip attackAnim;
    [SerializeField] private AnimationClip hurtAnim;
    [SerializeField] private AnimationClip deathAnim;

    private AnimationClip currentAnim;

    public void DoIdleAnimation()
    {
        currentAnim = idleAnim;
        animator.Play(idleAnim.name);
    }

    public void DoAttackAnimation()
    {
        currentAnim = attackAnim;
        animator.Play(attackAnim.name);
    }

    public void DoHurtAnimation()
    {
        currentAnim = hurtAnim;
        animator.Play(hurtAnim.name);
    }

    public void DoDeathAnimation()
    {
        currentAnim = deathAnim;
        animator.Play(deathAnim.name);
    }
}
