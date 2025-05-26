using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class InventoryManagerScript : MonoBehaviour
{

    public static InventoryManagerScript InventoryINSTANCE;

    
    
    public ItemData CurrentSelectedItemData;
    public ItemData emptyData;
    
    
    [Header("Player individual inventory :")]
    public Player1Inventory player1Inventory;
    public Player2Inventory player2Inventory;

    public ScrollerItems ScrollerItems;

    private void Awake()
    {

        if (InventoryINSTANCE == null)
        {
            InventoryINSTANCE = this;
        }
        else
        {
            Destroy(this);
        }
        
    }

    private void Start()
    {

        CurrentSelectedItemData = emptyData;
        
        player1Inventory.weaponItemData = emptyData;
        player1Inventory.armorItemData = emptyData;
        player1Inventory.accessorie1ItemData = emptyData;
        player1Inventory.accessorie2ItemData = emptyData;
        
        player1Inventory.weaponSprite.sprite = emptyData.sprite;
        player1Inventory.armornSprite.sprite = emptyData.sprite;
        player1Inventory.accessorie1Sprite.sprite = emptyData.sprite;
        player1Inventory.accessorie2Sprite.sprite = emptyData.sprite;
        
        //------------------------------------------------------------------
        
        player2Inventory.weaponItemData = emptyData;
        player2Inventory.armorItemData = emptyData;
        player2Inventory.accessorie1ItemData = emptyData;
        player2Inventory.accessorie2ItemData = emptyData;
        
        player2Inventory.weaponSprite.sprite = emptyData.sprite;
        player2Inventory.armornSprite.sprite = emptyData.sprite;
        player2Inventory.accessorie1Sprite.sprite = emptyData.sprite;
        player2Inventory.accessorie2Sprite.sprite = emptyData.sprite;
        
        //--------------------------------------------------------------------
        
        Debug.Log(" Wrong type of object or boutonID is wrong ");
        
        
        RefreshInventoryUI();
    }

    public void SaveSelectedItemData(ItemData itemData)
    {

        CurrentSelectedItemData = itemData;
        
    }

    #region CheckItemEnDouble

        private void CheckIfAlreadyEquippedSomeWhereElse(int boutonID)
        {
            #region CheckForFox

                if (boutonID == 1 && CurrentSelectedItemData == player2Inventory.weaponItemData)
                {
                    Debug.LogError("Current selected item was already equipped somewhere , attempt to re equip it here ...");
                    player2Inventory.weaponItemData = emptyData;
                    player1Inventory.weaponItemData = CurrentSelectedItemData;
                    RefreshInventoryUI();
                    return;
                }
                
                if (boutonID == 2 && CurrentSelectedItemData == player2Inventory.armorItemData)
                {
                    Debug.LogError("Current selected item was already equipped somewhere , attempt to re equip it here ...");
                    player2Inventory.armorItemData = emptyData;
                    player1Inventory.armorItemData = CurrentSelectedItemData;
                    RefreshInventoryUI();
                    return;
                }
                
                if (boutonID == 3 && CurrentSelectedItemData == player1Inventory.accessorie2ItemData 
                    || CurrentSelectedItemData == player2Inventory.accessorie1ItemData 
                    || CurrentSelectedItemData == player2Inventory.accessorie2ItemData)
                {
                    Debug.LogError("Current selected item was already equipped somewhere , attempt to re equip it here ...");
                    
                    if (CurrentSelectedItemData == player1Inventory.accessorie2ItemData)
                    {
                        player1Inventory.accessorie2ItemData = emptyData;
                        RefreshInventoryUI();
                    }
                    
                    if (CurrentSelectedItemData == player2Inventory.accessorie1ItemData)
                    {
                        player2Inventory.accessorie1ItemData = emptyData;
                        RefreshInventoryUI();
                    }
                    
                    if (CurrentSelectedItemData == player2Inventory.accessorie2ItemData)
                    {
                        player2Inventory.accessorie2ItemData = emptyData;
                        RefreshInventoryUI();
                    }
                        
                    player1Inventory.accessorie1ItemData = CurrentSelectedItemData;
                    RefreshInventoryUI();
                    return;
                }
                
                if (boutonID == 4 && CurrentSelectedItemData == player1Inventory.accessorie1ItemData 
                    || CurrentSelectedItemData == player2Inventory.accessorie1ItemData 
                    || CurrentSelectedItemData == player2Inventory.accessorie2ItemData)
                {
                    Debug.LogError("Current selected item was already equipped somewhere , attempt to re equip it here ...");
                    if (CurrentSelectedItemData == player1Inventory.accessorie1ItemData)
                    {
                        player1Inventory.accessorie1ItemData = emptyData;
                        RefreshInventoryUI();
                    }
                    
                    if (CurrentSelectedItemData == player2Inventory.accessorie1ItemData)
                    {
                        player2Inventory.accessorie1ItemData = emptyData;
                        RefreshInventoryUI();
                    }
                    
                    if (CurrentSelectedItemData == player2Inventory.accessorie2ItemData)
                    {
                        player2Inventory.accessorie2ItemData = emptyData;
                        RefreshInventoryUI();
                    }
                        
                    player1Inventory.accessorie2ItemData = CurrentSelectedItemData;
                    RefreshInventoryUI();
                    return;
                }
                
            #endregion

            #region CheckForFrog

                if (boutonID == 9 && CurrentSelectedItemData == player1Inventory.weaponItemData)
                {
                    Debug.LogError("Current selected item was already equipped somewhere , attempt to re equip it here ...");
                    player1Inventory.weaponItemData = emptyData;
                    player2Inventory.weaponItemData = CurrentSelectedItemData;
                    RefreshInventoryUI();
                    return;
                }
                
                if (boutonID == 10 && CurrentSelectedItemData == player1Inventory.armorItemData)
                {
                    Debug.LogError("Current selected item was already equipped somewhere , attempt to re equip it here ...");
                    player1Inventory.armorItemData = emptyData;
                    player2Inventory.armorItemData = CurrentSelectedItemData;
                    RefreshInventoryUI();
                    return;
                }
                
                if (boutonID == 11 && CurrentSelectedItemData == player2Inventory.accessorie2ItemData 
                    || CurrentSelectedItemData == player1Inventory.accessorie1ItemData 
                    || CurrentSelectedItemData == player1Inventory.accessorie2ItemData)
                {
                    Debug.LogWarning("Current selected item was already equipped somewhere , attempt to re equip it here ... 11");
                        
                    if (CurrentSelectedItemData == player2Inventory.accessorie2ItemData)
                    {
                        player2Inventory.accessorie2ItemData = emptyData;
                        RefreshInventoryUI();
                    }
                    
                    if (CurrentSelectedItemData == player1Inventory.accessorie1ItemData)
                    {
                        player1Inventory.accessorie1ItemData = emptyData;
                        RefreshInventoryUI();
                    }
                    
                    if (CurrentSelectedItemData == player1Inventory.accessorie2ItemData)
                    {
                        player1Inventory.accessorie2ItemData = emptyData;
                        RefreshInventoryUI();
                    }
                        
                    player2Inventory.accessorie1ItemData = CurrentSelectedItemData;
                    RefreshInventoryUI();
                    return;
                }
                
                if (boutonID == 12 && CurrentSelectedItemData == player2Inventory.accessorie1ItemData 
                    || CurrentSelectedItemData == player1Inventory.accessorie1ItemData 
                    || CurrentSelectedItemData == player1Inventory.accessorie2ItemData)
                {
                    Debug.LogWarning("Current selected item was already equipped somewhere , attempt to re equip it here ... 12");
                    
                    if (CurrentSelectedItemData == player2Inventory.accessorie1ItemData)
                    {
                        player2Inventory.accessorie1ItemData = emptyData;
                        RefreshInventoryUI();
                    }
                    
                    if (CurrentSelectedItemData == player1Inventory.accessorie1ItemData)
                    {
                        player1Inventory.accessorie1ItemData = emptyData;
                        RefreshInventoryUI();
                    }
                    
                    if (CurrentSelectedItemData == player1Inventory.accessorie2ItemData)
                    {
                        player1Inventory.accessorie2ItemData = emptyData;
                        RefreshInventoryUI();
                    }
                        
                    player1Inventory.accessorie2ItemData = CurrentSelectedItemData;
                    RefreshInventoryUI();
                    return;
                }
                
            #endregion
            
        }
        
        private void CheckIfAlreadyEquippedInTheSameSpot(int boutonID)
        {
            if (CurrentSelectedItemData == emptyData)
            {
                return;
            }

            #region CheckForFox

                if (boutonID == 1 && CurrentSelectedItemData == player1Inventory.weaponItemData)
                {
                    Debug.LogError("Weapon already equipped , processing unequipped it ...");
                    player1Inventory.weaponItemData = emptyData;
                    player1Inventory.weaponItemData = CurrentSelectedItemData;
                    Debug.LogError("Selected weapon has been requiped / Empty has been equipped");
                    RefreshInventoryUI();
                    return;
                }
                
                
                if (boutonID == 2 && CurrentSelectedItemData == player1Inventory.armorItemData)
                {
                    Debug.LogError("Weapon already equipped , processing unequipped it ...");
                    player1Inventory.armorItemData = emptyData;
                    player1Inventory.armorItemData = CurrentSelectedItemData;
                    Debug.LogError("Selected weapon has been requiped / Empty has been equipped");
                    RefreshInventoryUI();
                    return;
                }
                
                
                if (boutonID == 3 && CurrentSelectedItemData == player1Inventory.accessorie1ItemData)
                {
                    Debug.LogError("Weapon already equipped , processing unequipped it ...");
                    player1Inventory.accessorie1ItemData = emptyData;
                    player1Inventory.accessorie1ItemData = CurrentSelectedItemData;
                    Debug.LogError("Selected weapon has been requiped / Empty has been equipped");
                    RefreshInventoryUI();
                    return;
                }
                
                
                if (boutonID == 4 && CurrentSelectedItemData == player1Inventory.accessorie2ItemData)
                {
                    Debug.LogError("Weapon already equipped , processing unequipped it ...");
                    player1Inventory.accessorie1ItemData = emptyData;
                    player1Inventory.accessorie1ItemData = CurrentSelectedItemData;
                    Debug.LogError("Selected weapon has been requiped / Empty has been equipped");
                    RefreshInventoryUI();
                    return;
                }

            #endregion
                
            // Panda here
            
            #region CheckForFrog

                if (boutonID == 9 && CurrentSelectedItemData == player2Inventory.weaponItemData)
                {
                    Debug.LogError("Weapon already equipped , processing unequipped it ...");
                    player2Inventory.weaponItemData = emptyData;
                    player2Inventory.weaponItemData = CurrentSelectedItemData;
                    Debug.LogError("Selected weapon has been requiped / Empty has been equipped");
                    RefreshInventoryUI();
                    return;
                }
                
                
                if (boutonID == 10 && CurrentSelectedItemData == player2Inventory.armorItemData)
                { 
                    Debug.LogWarning("Weapon already equipped , processing unequipped it ...");
                    player2Inventory.armorItemData = emptyData;
                    player2Inventory.armorItemData = CurrentSelectedItemData;
                    Debug.LogWarning("Selected weapon has been requiped / Empty has been equipped");
                    RefreshInventoryUI();
                    return;
                }
                
                
                if (boutonID == 11 && CurrentSelectedItemData == player2Inventory.accessorie1ItemData)
                {
                    Debug.LogWarning("Weapon already equipped , processing unequipped it ...");
                    player2Inventory.accessorie1ItemData = emptyData;
                    player2Inventory.accessorie1ItemData = CurrentSelectedItemData;
                    Debug.LogWarning("Selected weapon has been requiped / Empty has been equipped");
                    RefreshInventoryUI();
                    return;
                }
                
                
                if (boutonID == 12 && CurrentSelectedItemData == player2Inventory.accessorie2ItemData)
                {
                    Debug.LogWarning("Weapon already equipped , processing unequipped it ...");
                    player2Inventory.accessorie2ItemData = emptyData;
                    player2Inventory.accessorie2ItemData = CurrentSelectedItemData;
                    Debug.LogWarning("Selected weapon has been re equipped / Empty has been equipped");
                    RefreshInventoryUI();
                    return;
                }

            #endregion
            
            RefreshInventoryUI();
            
        }
    #endregion
    
    
    
    
    //------------------------------------------------------------------------------------------------------------------
    
    
    public void EquipementSlot(int boutonID)
    {
        if (CurrentSelectedItemData != null || CurrentSelectedItemData != emptyData)
        {
            if (boutonID == 1 && CurrentSelectedItemData.itemSlot == ItemData.ItemSlot.Weapon && CurrentSelectedItemData != emptyData)
            {
                CheckIfAlreadyEquippedInTheSameSpot(boutonID);
                CheckIfAlreadyEquippedSomeWhereElse(boutonID);
                player1Inventory.weaponItemData = CurrentSelectedItemData;
                RefreshInventoryUI();
            }
            
            if (boutonID == 2 && CurrentSelectedItemData.itemSlot == ItemData.ItemSlot.Armor && CurrentSelectedItemData != emptyData)
            {
                CheckIfAlreadyEquippedInTheSameSpot(boutonID);
                CheckIfAlreadyEquippedSomeWhereElse(boutonID);
                player1Inventory.armorItemData = CurrentSelectedItemData;
                RefreshInventoryUI();
            }
            if (boutonID == 3 && CurrentSelectedItemData.itemSlot == ItemData.ItemSlot.Accessories && CurrentSelectedItemData != emptyData)
            {
                CheckIfAlreadyEquippedInTheSameSpot(boutonID);
                CheckIfAlreadyEquippedSomeWhereElse(boutonID);
                player1Inventory.accessorie1ItemData = CurrentSelectedItemData;
                RefreshInventoryUI();
            }
            
            if (boutonID == 4 && CurrentSelectedItemData.itemSlot == ItemData.ItemSlot.Accessories && CurrentSelectedItemData != emptyData)
            {
                CheckIfAlreadyEquippedInTheSameSpot(boutonID);
                CheckIfAlreadyEquippedSomeWhereElse(boutonID);
                player1Inventory.accessorie2ItemData = CurrentSelectedItemData;
                RefreshInventoryUI();
            }
            
        
            //--------------------
        
            if (boutonID == 9 && CurrentSelectedItemData.itemSlot == ItemData.ItemSlot.Weapon && CurrentSelectedItemData != emptyData)
            {
                CheckIfAlreadyEquippedInTheSameSpot(boutonID);
                CheckIfAlreadyEquippedSomeWhereElse(boutonID);
                player2Inventory.weaponItemData = CurrentSelectedItemData;
                RefreshInventoryUI();
            }
            if (boutonID == 10 && CurrentSelectedItemData.itemSlot == ItemData.ItemSlot.Armor && CurrentSelectedItemData != emptyData)
            {
                CheckIfAlreadyEquippedInTheSameSpot(boutonID);
                CheckIfAlreadyEquippedSomeWhereElse(boutonID);
                player2Inventory.armorItemData = CurrentSelectedItemData;
                RefreshInventoryUI();
            }
            if (boutonID == 11 && CurrentSelectedItemData.itemSlot == ItemData.ItemSlot.Accessories && CurrentSelectedItemData != emptyData)
            {
                CheckIfAlreadyEquippedInTheSameSpot(boutonID);
                CheckIfAlreadyEquippedSomeWhereElse(boutonID);
                player2Inventory.accessorie1ItemData = CurrentSelectedItemData;
                RefreshInventoryUI();
            }
            if (boutonID == 12 && CurrentSelectedItemData.itemSlot == ItemData.ItemSlot.Accessories && CurrentSelectedItemData != emptyData)
            {
                CheckIfAlreadyEquippedInTheSameSpot(boutonID);
                CheckIfAlreadyEquippedSomeWhereElse(boutonID);
                player2Inventory.accessorie2ItemData = CurrentSelectedItemData;
                RefreshInventoryUI();

            }
        
            RefreshInventoryUI();
        }
        else if (CurrentSelectedItemData != null || CurrentSelectedItemData == emptyData)
        {
            if (boutonID == 1)
            {
                player1Inventory.weaponItemData = emptyData;
                RefreshInventoryUI();
            }
            if (boutonID == 2)
            {
                player1Inventory.weaponItemData = emptyData;
                RefreshInventoryUI();
            }
            if (boutonID == 3)
            {
                player1Inventory.weaponItemData = emptyData;
                RefreshInventoryUI();
            }
            if (boutonID == 4)
            {
                player1Inventory.weaponItemData = emptyData;
                RefreshInventoryUI();
            }
            
            if (boutonID == 9)
            {
                player1Inventory.weaponItemData = emptyData;
                RefreshInventoryUI();
            }
            if (boutonID == 10)
            {
                player1Inventory.weaponItemData = emptyData;
                RefreshInventoryUI();
            }
            if (boutonID == 11)
            {
                player1Inventory.weaponItemData = emptyData;
                RefreshInventoryUI();
            }
            if (boutonID == 12)
            {
                player1Inventory.weaponItemData = emptyData;
                RefreshInventoryUI();
            }
        }
        else
        {
            Debug.LogError(" Please select an item first");
        }
        
    }

    private void RefreshInventoryUI()
    {
        player1Inventory.LoadSprite();
        player1Inventory.RefreshItemStatsUpdate();
            
        player2Inventory.LoadSprite();
        player2Inventory.RefreshItemStatsUpdate();
    }
    
}