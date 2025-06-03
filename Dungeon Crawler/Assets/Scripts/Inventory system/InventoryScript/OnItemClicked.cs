using System;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class OnItemClicked : MonoBehaviour, IPointerClickHandler
{
    public static List<OnItemClicked> mAllItems = new List<OnItemClicked>();

    public ItemData item;

    //public GameObject selectedGo;
    
    private void Awake()
    {
        mAllItems.Add(this);
    }

    private void Start()
    {
        UnselectAllItems();
    }

    private void SelectItem()
    {
        UnselectAllItems();
        transform.localScale = new Vector3(1.1f,1.1f);
        //selectedGo = gameObject;
    }
    
    public static void UnselectAllItems()
    {
        foreach (var varItem in mAllItems)
        {
            varItem.transform.localScale = new Vector3(1,1);
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        SelectItem();
        Debug.Log($"Clicked on : " + item.itemName);
        InventoryManagerScript.InventoryINSTANCE.SaveSelectedItemData(item);
    }
    
    private void OnDestroy()
    {
        mAllItems.Remove(this);
    }
    
}
