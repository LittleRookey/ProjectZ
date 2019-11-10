using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    private static Inventory instance;

    public static Inventory Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion

    public delegate void OnItemChanged();

    public OnItemChanged onItemChangedCallBack;


    public List<Item> items = new List<Item>();

    public void Add(Item item)
    {
        if(!FindItem(item))
        {
            Debug.Log("Added new");
            items.Add(item);
            item.isDefaultItem = true;
            if(onItemChangedCallBack != null)
            {
                onItemChangedCallBack.Invoke();
            }

        } else
        {
            Debug.Log("Added old");
            for (int i = 0; i < items.Count; i++)
            {
                if (item.ID == items[i].ID)
                {
                    items[i].itemCount++;
                    break;
                }
            }
        }
        // if does not have item
    }


    //[SerializeField]
    //private Transform itemsParent;

    //[SerializeField]
    //private InventorySlot[] slots;

    private bool FindItem(Item it)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if(items[i].ID == it.ID)
            {
                return true;
            } 
        }
        return false;
    }

    private Item GetItem(int index)
    {
        return items[index];
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        //slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    

}
