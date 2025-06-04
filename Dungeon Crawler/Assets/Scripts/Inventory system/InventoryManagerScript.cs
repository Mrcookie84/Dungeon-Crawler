using System;
using UnityEngine;

public class InventoryManagerScript : MonoBehaviour
{
    public static InventoryManagerScript InventoryINSTANCE;

    public ItemData CurrentSelectedItemData;
    public ItemData emptyData;
    

    private void Awake()
    {
        if (InventoryINSTANCE == null)
            InventoryINSTANCE = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        //PlayerInventory.ResetInventory();
        CurrentSelectedItemData = emptyData;

        RefreshInventoryUI();
    }

    public void SaveSelectedItemData(ItemData itemData)
    {
        CurrentSelectedItemData = itemData;
    }

    
    public void EquipementSlot(int boutonID)
    {
        if (CurrentSelectedItemData == null)
        {
            Debug.LogWarning("Please select an item first!");
            return;
        }

        if (CurrentSelectedItemData == emptyData)
        {
            UnequipSlot(boutonID);
            return;
        }

        if (!IsValidSlotForItem(boutonID, CurrentSelectedItemData.itemSlot))
            return;

        CheckIfAlreadyEquippedInSameSpot(boutonID);
        CheckIfAlreadyEquippedSomewhereElse(boutonID);
        EquipItem(boutonID);

        RefreshInventoryUI();
    }

    private bool IsValidSlotForItem(int slotID, ItemData.ItemSlot itemSlot)
    {
        /*
        return (slotID == 1 || slotID == 9) && itemSlot == ItemData.ItemSlot.Weapon
            || (slotID == 2 || slotID == 10) && itemSlot == ItemData.ItemSlot.Armor
            || (slotID == 3 || slotID == 4 || slotID == 11 || slotID == 12) && itemSlot == ItemData.ItemSlot.Accessories;*/

        return true;
    }

    private void EquipItem(int boutonID)
    {
        /*
        switch (boutonID)
        {
            case 1: player1Inventory.weaponItemData = CurrentSelectedItemData; break;
            case 2: player1Inventory.armorItemData = CurrentSelectedItemData; break;
            case 3: player1Inventory.accessorie1ItemData = CurrentSelectedItemData; break;
            case 4: player1Inventory.accessorie2ItemData = CurrentSelectedItemData; break;
            case 9: player2Inventory.weaponItemData = CurrentSelectedItemData; break;
            case 10: player2Inventory.armorItemData = CurrentSelectedItemData; break;
            case 11: player2Inventory.accessorie1ItemData = CurrentSelectedItemData; break;
            case 12: player2Inventory.accessorie2ItemData = CurrentSelectedItemData; break;
        }*/
        
    }

    private void UnequipSlot(int boutonID)
    {
        /*
        switch (boutonID)
        {
            case 1: player1Inventory.weaponItemData = emptyData; break;
            case 2: player1Inventory.armorItemData = emptyData; break;
            case 3: player1Inventory.accessorie1ItemData = emptyData; break;
            case 4: player1Inventory.accessorie2ItemData = emptyData; break;
            case 9: player2Inventory.weaponItemData = emptyData; break;
            case 10: player2Inventory.armorItemData = emptyData; break;
            case 11: player2Inventory.accessorie1ItemData = emptyData; break;
            case 12: player2Inventory.accessorie2ItemData = emptyData; break;
        }

        RefreshInventoryUI();*/
    }

    private void CheckIfAlreadyEquippedInSameSpot(int boutonID)
    {
        /*
        if (CurrentSelectedItemData == emptyData) return;

        switch (boutonID)
        {
            case 1 when CurrentSelectedItemData == player1Inventory.weaponItemData:
            case 2 when CurrentSelectedItemData == player1Inventory.armorItemData:
            case 3 when CurrentSelectedItemData == player1Inventory.accessorie1ItemData:
            case 4 when CurrentSelectedItemData == player1Inventory.accessorie2ItemData:
            case 9 when CurrentSelectedItemData == player2Inventory.weaponItemData:
            case 10 when CurrentSelectedItemData == player2Inventory.armorItemData:
            case 11 when CurrentSelectedItemData == player2Inventory.accessorie1ItemData:
            case 12 when CurrentSelectedItemData == player2Inventory.accessorie2ItemData:
                Debug.Log("Item already equipped in the same slot.");
                break;
        }*/
    }

    private void CheckIfAlreadyEquippedSomewhereElse(int boutonID)
    {
        /*
        // Déquiper l'item s’il est équipé ailleurs
        if (player1Inventory.weaponItemData == CurrentSelectedItemData)
            player1Inventory.weaponItemData = emptyData;

        if (player1Inventory.armorItemData == CurrentSelectedItemData)
            player1Inventory.armorItemData = emptyData;

        if (player1Inventory.accessorie1ItemData == CurrentSelectedItemData)
            player1Inventory.accessorie1ItemData = emptyData;

        if (player1Inventory.accessorie2ItemData == CurrentSelectedItemData)
            player1Inventory.accessorie2ItemData = emptyData;

        if (player2Inventory.weaponItemData == CurrentSelectedItemData)
            player2Inventory.weaponItemData = emptyData;

        if (player2Inventory.armorItemData == CurrentSelectedItemData)
            player2Inventory.armorItemData = emptyData;

        if (player2Inventory.accessorie1ItemData == CurrentSelectedItemData)
            player2Inventory.accessorie1ItemData = emptyData;

        if (player2Inventory.accessorie2ItemData == CurrentSelectedItemData)
            player2Inventory.accessorie2ItemData = emptyData;*/
    }

    private void RefreshInventoryUI()
    {
        /*
        PlayerInventory.LoadSprite();
        PlayerInventory.RefreshItemStatsUpdate();*/
    }
}