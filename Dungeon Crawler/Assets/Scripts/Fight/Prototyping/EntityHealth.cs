using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EntityHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    [SerializeField] private EntityPosition posComponent;
    [SerializeField] private string healthBarsGroupTag;

    public bool dead;

    public UnityEvent tookDamage = new UnityEvent();
    public UnityEvent isDying = new UnityEvent();


    [SerializeField] private Transform healthBarsGroup;
    [SerializeField] private Slider healthSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        healthBarsGroup = GameObject.FindGameObjectWithTag(healthBarsGroupTag).transform;
        
        currentHealth = maxHealth;
        ChangeHealthBar();
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

    public void ChangeHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.gameObject.SetActive(false);
        }
        
        int healthBarIndex = posComponent.gridPos.x + posComponent.gridPos.y * 3;
        Transform newHealthBar = healthBarsGroup.GetChild(healthBarIndex);
        newHealthBar.gameObject.SetActive(true);
        healthSlider = newHealthBar.GetComponent<Slider>();

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}
