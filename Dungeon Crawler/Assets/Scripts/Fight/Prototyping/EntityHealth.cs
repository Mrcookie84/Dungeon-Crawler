using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EntityHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public bool dead;

    public UnityEvent tookDamage = new UnityEvent();
    public UnityEvent isDying = new UnityEvent();

    [SerializeField] private Slider healthSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = currentHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int amount)
    {   
        currentHealth -= amount;
        healthSlider.value = currentHealth;

        if (CheckDeath() && !dead)
        {
            dead = true;
            isDying.Invoke();
        }
        else
        {
            tookDamage.Invoke();
        }
    }

    private bool CheckDeath()
    {
        return currentHealth <= 0;
    }
}
