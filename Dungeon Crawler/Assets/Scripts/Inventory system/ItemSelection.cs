using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSelection : MonoBehaviour
{
    public ItemData item;
    
    public void AddToInv()
    {
        PlayerInventory.AddItem(item);
    }
}
