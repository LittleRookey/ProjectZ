using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private Transform itemsParent;

    [SerializeField]
    private InventorySlot[] slots;


    Inventory inventory;

    private void Start()
    {
        inventory = Inventory.Instance;
        inventory.onItemChangedCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

    }

    public void UpdateItemCounts()
    {
        for(int i = 0; i < inventory.items.Count; i++)
        {
            slots[i].UpdateItemCount(inventory.items[i].itemCount);
        }
    }

    private void UpdateUI()
    {
        Debug.Log("Updated UI");
        

        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            } else
            {
                slots[i].ClearSlot();
            }
        }
        UpdateItemCounts();
    }

    public void OpenInventory()
    {
        inventory.gameObject.SetActive(true);
        ShopController.Instance.CloseShop();
        inventory.transform.SetAsLastSibling();
    }

    public void CloseInventory()
    {
        inventory.gameObject.SetActive(false);
    }
}
