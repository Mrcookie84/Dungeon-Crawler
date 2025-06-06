using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Item info")]
    public string itemName;
    public Sprite sprite;
    
    [Header("Added runes")]
    public SerializedDictionary<RuneType, int> runes;

    [Header("Stats Boosts")]
    public int addedPV;
    public int addedStability;
    public int addedMana;
    [Space(10)]
    
    [SerializedDictionary("Damage Type", "Percent")]
    public SerializedDictionary<DamageTypesData.DmgTypes, int> dmgResistance;
    
    [SerializedDictionary("Damage Type", "Percent")]
    public SerializedDictionary<DamageTypesData.DmgTypes, int> dmgBoost;

    [Header("Description")]
    public string desc;

    public enum RuneType
    {
        Reality = 0,
        Space = 1,
        Time = 2,
        Focus = 3,
        Extension = 4,
        Affliction = 5
    }
        
    public enum ItemSlot
    {
        Weapon = 0,
        Armor = 1,
        Accessories1 = 2,
        Accessories2 = 3,
        Accessories3 = 4
    }
}
