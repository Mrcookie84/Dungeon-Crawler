using System;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    [SerializeField] private EntityPosition posComponent;

    public bool dead;
    
    [Header("Animator")]
    [SerializeField] private EntityFightAnimation animHandler;

    public UnityEvent tookDamage = new UnityEvent();
    public UnityEvent isDying = new UnityEvent();
    
    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {   
        currentHealth -= amount;

        if (CheckDeath() && !dead)
        {
            dead = true;
            isDying.Invoke();

            TurnManager.TestEndFight(posComponent.LinkedGrid);
            Destroy(gameObject);
        }
        else
        {
            animHandler.ChangeState(EntityFightAnimation.State.Hurt);
            tookDamage.Invoke();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Math.Min(maxHealth, currentHealth + amount);
    }

    private bool CheckDeath()
    {
        return currentHealth <= 0;
    }
}
