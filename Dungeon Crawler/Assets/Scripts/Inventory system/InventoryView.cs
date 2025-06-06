using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    public static InventoryView Instance;

    [Header("UI reference")]
    [SerializeField] private Transform inventoryHolder;
    [SerializeField] private VerticalLayoutGroup groupManager;

    [Header("Item Holder")]
    [SerializeField] private GameObject holderPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public static void ResetInvView()
    {
        for (int i = 0; i < Instance.inventoryHolder.childCount; i++)
        {
            Destroy(Instance.inventoryHolder.GetChild(i).gameObject);
        }
    }

    public static void UpdateInvView()
    {
        ResetInvView();

        foreach (ItemData item in PlayerInventory.Items)
        {
            GameObject itemView = Instantiate(Instance.holderPrefab, Instance.inventoryHolder);

            ItemHolder holder = itemView.GetComponent<ItemHolder>();
            holder.SetItem(item);
        }
    }
}
