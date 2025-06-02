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
    
    [Header("Components")]
    [SerializeField] private EntityFightAnimation animHandler;
    [SerializeField] private EntityStatusHolder status;

    public UnityEvent gotAttacked = new UnityEvent();
    public UnityEvent tookDamage = new UnityEvent();
    public UnityEvent isDying = new UnityEvent();
    
    [Header("Display")]
    [SerializeField] private GameObject dmgDisplayPreF;

    [HideInInspector] public bool invicible = false;
    
    void Awake()
    {
        currentHealth = maxHealth;

        healthBarGroup = GameObject.FindGameObjectWithTag(healthBarGroupTag).GetComponent<HealthBarGroupManager>();
    }

    public void TakeDamage(GameObject source, int amount, DamageTypesData.DmgTypes type)
    {
        DamageInfo attackInfo = new DamageInfo(source, amount, type);
        
        if (invicible)
        {
            status.DamageResponse(attackInfo);
            gotAttacked.Invoke();
            return;
        }
        
        // Affichage des dégâts
        GameObject display = Instantiate(dmgDisplayPreF, transform);
        DamageDisplay displayComp = display.GetComponent<DamageDisplay>();
        StartCoroutine(displayComp.DisplayInfo(attackInfo));
        
        status.DamageResponse(attackInfo);
        gotAttacked.Invoke();

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
        healthBarGroup.UpdateHealthBars();
    }

    private bool CheckDeath()
    {
        return currentHealth <= 0;
    }

    [Serializable]
    public struct DamageInfo
    {
        [SerializeField] public GameObject attacker { get; }
        [SerializeField] public int dmgValue { get; }
        [SerializeField] public DamageTypesData.DmgTypes dmgType { get; }
        
        public DamageInfo(GameObject attacker, int dmgValue, DamageTypesData.DmgTypes dmgType = DamageTypesData.DmgTypes.Crush)
        {
            this.attacker = attacker;
            this.dmgValue = dmgValue;
            this.dmgType  = dmgType;
        }
    }
}
