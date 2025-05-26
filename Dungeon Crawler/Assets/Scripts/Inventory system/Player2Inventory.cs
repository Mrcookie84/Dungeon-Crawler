using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player2Inventory : MonoBehaviour
{
    [Header("Text : ")]
    public TMP_Text spaceDMGBonusText;
    public TMP_Text realityDMGBonusText;
    public TMP_Text itemEquippedText;
    
    [Header("Weapon : ")]
    public ItemData weaponItemData;
    public Image weaponSprite;
    
    [Header("Armor : ")]
    public ItemData armorItemData;
    public Image armornSprite;
    
    [Header("Accessorie 1 : ")]
    public ItemData accessorie1ItemData;
    public Image accessorie1Sprite;
    
    [Header("Accessorie 2 : ")]
    public ItemData accessorie2ItemData;
    public Image accessorie2Sprite;
    
    
    public void LoadSprite()
    {
        weaponSprite.sprite = weaponItemData.sprite;
        armornSprite.sprite = armorItemData.sprite;
        accessorie1Sprite.sprite = accessorie1ItemData.sprite;
        accessorie2Sprite.sprite = accessorie2ItemData.sprite;
    }
    
    public void RefreshItemStatsUpdate()
    {
        int totalSpaceDMGBonus = weaponItemData.spaceDmgBonus +
                                 armorItemData.spaceDmgBonus +
                                 accessorie1ItemData.spaceDmgBonus +
                                 accessorie2ItemData.spaceDmgBonus;
        
        int totalRealityDMGBonus = weaponItemData.realityDmgBonus +
                                   armorItemData.realityDmgBonus +
                                   accessorie1ItemData.realityDmgBonus +
                                   accessorie2ItemData.realityDmgBonus;
        
        
        spaceDMGBonusText.text = "Total space damage bonus is : " + totalSpaceDMGBonus ;
        realityDMGBonusText.text = "Total space damage bonus is : " + totalRealityDMGBonus ;

        
        itemEquippedText.text = "weapon : " + weaponItemData.itemName + " / " +
                                "armor : " + armorItemData.itemName + " / " +
                                "accessorie 1 : " + accessorie1ItemData.itemName + " / " +
                                "accessorie 2 : " + accessorie2ItemData.itemName;


    }
    
    
    
    
    
    
    
    
}
