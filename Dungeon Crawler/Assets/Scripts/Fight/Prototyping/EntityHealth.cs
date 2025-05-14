using System;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    [SerializeField] private string healthBarGroupTag;
    private HealthBarGroupManager healthBarGroup;
    [SerializeField] private EntityPosition posComponent;

    [HideInInspector] public bool dead;
    [SerializeField] private bool destoryOnDeath;
    
    [Header("Animator")]
    [SerializeField] private EntityFightAnimation animHandler;

    public UnityEvent tookDamage = new UnityEvent();
    public UnityEvent isDying = new UnityEvent();
    
    void Awake()
    {
        currentHealth = maxHealth;

        healthBarGroup = GameObject.FindGameObjectWithTag(healthBarGroupTag).GetComponent<HealthBarGroupManager>();
    }

    public void TakeDamage(int amount)
    {   
        currentHealth -= amount;
        healthBarGroup.UpdateHealthBars();

        if (CheckDeath() && !dead)
        {
            dead = true;
            animHandler.ChangeState(EntityFightAnimation.State.Dead);
            isDying.Invoke();

            TurnManager.TestEndFight(posComponent.LinkedGrid);
            if (destoryOnDeath) Destroy(gameObject);
        }
        else if (!dead)
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
