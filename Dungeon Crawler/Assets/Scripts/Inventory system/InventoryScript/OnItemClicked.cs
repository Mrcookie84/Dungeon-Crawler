using System;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class OnItemClicked : MonoBehaviour, IPointerClickHandler
{

    
    
    public List<OnItemClicked> m_allItems = new List<OnItemClicked>();

    public Item item;

    public GameObject selectedGO;
    
    private void Awake()
    {
        m_allItems.Add(this);
    }

    private void Start()
    {
        UnselectAllItems();
    }

    private void SelectItem()
    {
        UnselectAllItems();
        transform.localScale = new Vector3(1.1f,1.1f);
        selectedGO = this.gameObject;
    }
    
    public void UnselectAllItems()
    {
        foreach (var item in m_allItems)
        {
            item.transform.localScale = new Vector3(1,1);
        }
        
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {

            SelectItem();
            Debug.Log($"Clicked on : " + this.item.ItemData.itemName);
            InventoryManagerScript.InventoryINSTANCE.SaveSelectedItemData(item.ItemData);
        
    }
}
