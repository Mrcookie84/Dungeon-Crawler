using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemChestSelection : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private Image iconRenderer;
    [SerializeField] private TMPro.TextMeshProUGUI itemName;
    [SerializeField] private TMPro.TextMeshProUGUI desc;

    private ItemData currentItem;
    private Chest linkedChest;

    public void SetItem(ItemData item, Chest origin)
    {
        currentItem = item;
        linkedChest = origin;

        iconRenderer.sprite = item.sprite;
        iconRenderer.SetNativeSize();
        desc.text = item.desc;

        itemName.text = item.itemName;
    }

    public void AddToInv()
    {
        PlayerInventory.AddItem(currentItem);

        linkedChest.ItemSelected();
        Destroy(gameObject);
    }
}
