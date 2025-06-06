using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private static PlayerInventory Instance;

    public List<ItemData> items;
    
    // ============== Propriété =================== //
    public static List<ItemData> Items
    {
        get { return Instance.items; }
    }

    public static int ManaBoost
    {
        get
        {
            if (Instance == null) return 0;
            
            int manaBoost = 0;

            foreach (var item in Instance.items)
            {
                if (item == null) continue;
                manaBoost += item.addedMana;
            }

            return manaBoost;
        }
    }
    
    public static int StabilityBoost
    {
        get
        {
            if (Instance == null) return 0;
            
            int stabilityBoost = 0;

            foreach (var item in Instance.items)
            {
                if (item == null) continue;
                stabilityBoost += item.addedStability;
            }

            return stabilityBoost;
        }
    }
    
    public static int HealthBoost
    {
        get
        {
            if (Instance == null) return 0;
            
            int healthBoost = 0;

            foreach (var item in Instance.items)
            {
                if (item == null) continue;
                healthBoost += item.addedPV;
            }

            return healthBoost;
        }
    }
    
    // ============== Méthode =================== //
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public static void ResetInventory()
    {
        if (Instance == null) return;
        
        Instance.items = new List<ItemData>();
    }

    public static void AddItem(ItemData item)
    {
        if (Instance == null) return;
        
        Instance.items.Add(item);

        InventoryView.UpdateInvView();
    }

    public static int GetDmgRestance(DamageTypesData.DmgTypes dmgType)
    {
        if (Instance == null) return 0;
        
        int resist = 0;

        foreach (var item in Instance.items)
        {
            if (item.dmgResistance.ContainsKey(dmgType))
                resist += item.dmgResistance[dmgType];
        }

        return resist;
    }
    
    public static int GetDmgBoost(DamageTypesData.DmgTypes dmgType)
    {
        if (Instance == null) return 0;
        
        int boost = 0;

        foreach (var item in Instance.items)
        {
            if (item.dmgBoost.ContainsKey(dmgType))
                boost += item.dmgBoost[dmgType];
        }
        
        return boost;
    }
}
