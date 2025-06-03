using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerInventory : MonoBehaviour
{
    [Header("Text : ")]
    public TMP_Text itemEquippedText;
    
    [SerializedDictionary("Slot","Item")]
    public SerializedDictionary<ItemData.ItemSlot, ItemData> items =
            new SerializedDictionary<ItemData.ItemSlot, ItemData>()
            {
                {ItemData.ItemSlot.Weapon, null},
                {ItemData.ItemSlot.Armor, null},
                {ItemData.ItemSlot.Accessories1, null},
                {ItemData.ItemSlot.Accessories2, null},
                {ItemData.ItemSlot.Accessories3, null}
            };
    
    public static PlayerInventory PInventoryINSTANCE;
    
    // ============== Propriété =================== //
    public static int ManaBoost
    {
        get
        {
            int manaBoost = 0;

            foreach (var item in PInventoryINSTANCE.items.Values)
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

            foreach (var item in PInventoryINSTANCE.items.Values)
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

            foreach (var item in PInventoryINSTANCE.items.Values)
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
        if (PInventoryINSTANCE == null)
            PInventoryINSTANCE = this;
        else
            Destroy(this);
    }

    public static void ResetInventory()
    {
        foreach (var slot in PInventoryINSTANCE.items.Keys)
        {
            PInventoryINSTANCE.items[slot] = null;
        }
    }
    
    public static void LoadSprite()
    {
        
    }

    public static void RefreshItemStatsUpdate()
    {
        
    }
}
