using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private static PlayerInventory Instance;

    public List<ItemData> items;
    
    // ============== Propriété =================== //
    public static int ManaBoost
    {
        get
        {
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
        Instance.items = new List<ItemData>();
    }

    public static void AddItem(ItemData item)
    {
        Instance.items.Add(item);
    }
}
