using System;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private HealthData data;
    [SerializeField] private bool isPlayer;
    [HideInInspector] public float maxHealth;
    [HideInInspector] public float currentHealth;
    [SerializeField] private string healthBarGroupTag;
    private HealthBarGroupManager healthBarGroup;

    [HideInInspector] public bool dead;
    [SerializeField] private bool destoryOnDeath;
    
    [Header("Components")]
    [SerializeField] private EntityFightAnimation animHandler;
    [SerializeField] private EntityStatusHolder status;
    [SerializeField] private EntityPosition posComp;
    [SerializeField] private EntityStatsModifier bonusStats;

    public UnityEvent gotAttacked = new UnityEvent();
    public UnityEvent tookDamage = new UnityEvent();
    public UnityEvent isDying = new UnityEvent();
    
    [Header("Display")]
    [SerializeField] private GameObject dmgDisplayPreF;

    [HideInInspector] public bool invicible = false;

    public static void InitializeCurrentHealth()
    {
        HealthData foxHealth = Resources.Load<HealthData>("Health/FoxHealthData");
        foxHealth.currentHealth = foxHealth.defaultHealth;

        HealthData pandaHealth = Resources.Load<HealthData>("Health/PandaHealthData");
        pandaHealth.currentHealth = pandaHealth.defaultHealth;

        HealthData frogHealth = Resources.Load<HealthData>("Health/FrogHealthData");
        frogHealth.currentHealth = frogHealth.defaultHealth;
    }
    
    void Awake()
    {
        maxHealth = data.defaultHealth;
        if (data.isLinkedToInv)
            maxHealth += PlayerInventory.HealthBoost;

        if (data.keepHealth)
            currentHealth = data.currentHealth;
        else
            currentHealth = maxHealth;

        healthBarGroup = GameObject.FindGameObjectWithTag(healthBarGroupTag).GetComponent<HealthBarGroupManager>();
    }

    public void TakeDamage(GameObject source, int amount, DamageTypesData.DmgTypes type)
    {
        if (dead) return;
        
        DamageInfo attackInfo = new DamageInfo(source, amount, type);
        
        if (invicible)
        {
            status.DamageResponse(attackInfo);
            gotAttacked.Invoke();
            return;
        }
        
        // Faiblesse dimensionnel
        if (!posComp.IsWeak && type == DamageTypesData.DmgTypes.DimensionalWeakness)
        {
            attackInfo = new DamageInfo(source, 10, DamageTypesData.DmgTypes.Reality);
        }
        
        // Application des resistances
        int coef = 100;
        
        // Vérifier si le coef est altéré par défaut
        if (data.resistInfos.ContainsKey(type))
        {
            coef -= data.resistInfos[type];
        }
        
        // Vérifier si l'inventaire change le coef
        if (data.isLinkedToInv)
        {
            coef -= PlayerInventory.GetDmgRestance(type);
        }

        coef += bonusStats.generalResBoost;

        coef = Math.Max(0, coef);
        int trueAmount = (int)(attackInfo.dmgValue * (coef / 100f));

        attackInfo = new DamageInfo(source, trueAmount, attackInfo.dmgType);
        
        
        // Affichage des dégâts
        if (amount > 0)
        {
            GameObject display = Instantiate(dmgDisplayPreF, transform);
            DamageDisplay displayComp = display.GetComponent<DamageDisplay>();
            StartCoroutine(displayComp.DisplayInfo(attackInfo));
        }
        
        status.DamageResponse(attackInfo);
        gotAttacked.Invoke();

        currentHealth -= amount;
        healthBarGroup.UpdateHealthBars();

        #region Death
        if (CheckDeath())
        {
            dead = true;
            animHandler.ChangeState(EntityFightAnimation.State.Dead);
            isDying.Invoke();

            if (isPlayer)
            {
                SpellCaster.ResetCaster();
                CharaPortraitHandler.ResetPortait();
            }

            TurnManager.TestEndFight(posComp.LinkedGrid);

            // A changer pour laisser l'animation de mort se jouer
            if (destoryOnDeath) Destroy(gameObject);
            
            return;
        }
        #endregion
        
        animHandler.ChangeState(EntityFightAnimation.State.Hurt);
        tookDamage.Invoke();
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
