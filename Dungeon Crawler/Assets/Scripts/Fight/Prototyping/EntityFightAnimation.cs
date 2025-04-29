using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFightAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip idleAnim;
    [SerializeField] private AnimationClip attackAnim;
    [SerializeField] private AnimationClip hurtAnim;
    [SerializeField] private AnimationClip deathAnim;

    private AnimationClip currentAnim;

    // Start is called before the first frame update
    void Start()
    {
        // Lier l'animation d'attaque
        gameObject.GetComponent<SpellCaster>().spellCasted.AddListener(DoAttackAnimation);
        // Lier l'animation de dégâts
        gameObject.GetComponent<EntityHealth>().tookDamage.AddListener(DoHurtAnimation);
        // Lier l'animation de mort
        gameObject.GetComponent<EntityHealth>().isDying.AddListener(DoDeathAnimation);
    }

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
