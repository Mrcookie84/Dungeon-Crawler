using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnItemClicked : MonoBehaviour, IPointerClickHandler
{
    private static List<OnItemClicked> m_allItems = new List<OnItemClicked>();

    public Item data;

    private void Awake()
    {
        m_allItems.Add(this);
    }

    private void SelectItem()
    {
        UnselectAllItems();
        transform.localScale = new Vector3(1.1f,1.1f);
    }

    private void UnselectAllItems()
    {
        foreach (var item in m_allItems)
        {
            item.transform.localScale = new Vector3(1,1);
        }
    }
    
    private void OnMouseUpAsButton()
    {
        Debug.LogError($"Clicked on {gameObject.name}");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.LogError($"Clicked on {gameObject.name}");
        SelectItem();
    }
}
