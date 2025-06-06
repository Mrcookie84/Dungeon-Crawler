using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Reference")]
    [SerializeField] private GameObject popUpWindow;
    [SerializeField] private TMPro.TextMeshProUGUI popUpText;
    [SerializeField] private Image iconRenderer;
    [SerializeField] private TMPro.TextMeshProUGUI itemName;
    [SerializeField] private TMPro.TextMeshProUGUI desc;

    [HideInInspector] public ItemData currentItem;

    public void SetItem(ItemData item)
    {
        currentItem = item;

        iconRenderer.sprite = item.sprite;
        iconRenderer.SetNativeSize();
        desc.text = item.desc;

        if (popUpText != null )
        {
            popUpText.text = item.itemName;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (popUpWindow != null)
            popUpWindow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (popUpWindow != null)
            popUpWindow.SetActive(false);
    }
}
