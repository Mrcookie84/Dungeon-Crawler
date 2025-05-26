using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemData
{
    public string itemName;
    public Sprite sprite;
    
    public int spaceDmgBonus;
    public int realityDmgBonus;
        
    public enum ItemSlot
    {
        Weapon,
        Armor,
        Accessories
    }

    public ItemSlot itemSlot;
}
