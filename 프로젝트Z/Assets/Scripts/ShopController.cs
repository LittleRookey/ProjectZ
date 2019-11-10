using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopController : MonoBehaviour
{
    private static ShopController instance;

    public static ShopController Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private SlotPool slotPool;

    [SerializeField]
    private Transform Shop, ShopSlots;

    [SerializeField]
    private List<Slot> currentSlots, disabledSlots;

    [SerializeField]
    private List<Slot> copiedSlots;

    [SerializeField]
    private List<Item> shopItemList;

    [SerializeField]
    private Item[] originItems;

    [SerializeField]
    private int itemSlotMax, currentItemSlotNum;

    [SerializeField]
    private PlayerData pData;

    [SerializeField]
    private Slot slotPrefab;

    [SerializeField]
    private double resetGold;

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
        currentSlots = new List<Slot>();
        copiedSlots = new List<Slot>();
        disabledSlots = new List<Slot>();
        shopItemList = new List<Item>();
    }

    public void SetPlayerData(PlayerData pd)
    {
        pData = pd;
    }
    //public int FindItemPrice(int id)
    //{
    //    return FindItemById(id).price;
    //}

    private Item FindItemById(int id)
    {
        for(int i = 0; i < originItems.Length; i++)
        {
            if(originItems[i].ID == id)
            {
                return originItems[i].Clone();
            }
        }

        return null;
    }

    public void SetOriginItems(Item[] items)
    {
        originItems = items;
    }

    public void RenewShop()
    {
        ClearAllLists();
        UIController.Instance.UpdateResetCost(resetGold);
        currentItemSlotNum = Random.Range(1, itemSlotMax + 1);
        
        for (int i = 0; i < currentItemSlotNum; i++)
        {
            int ind = Random.Range(0, originItems.Length);
            shopItemList.Add(originItems[ind].Clone());
        }
        LoadShop();
    }

    public void ResetShopWithGold()
    {
        RenewShop(resetGold);
    }

    private void RenewShop(double goldCost)
    {
        if(pData.player_gold < goldCost)
        {
            Debug.Log(pData.player_gold + ", " + goldCost);
            // UIMessage dont have enough gold
            MessageGUI.Instance.ShowMessage("골드가 부족합니다");
            return;
        }
        pData.player_gold -= goldCost;
        resetGold += 10;
        UIController.Instance.UpdateResetCost(resetGold);
        ClearAllLists();
        currentItemSlotNum = Random.Range(1, itemSlotMax + 1);

        for (int i = 0; i < currentItemSlotNum; i++)
        {
            int ind = Random.Range(0, originItems.Length);
            shopItemList.Add(originItems[ind].Clone());
        }
        LoadShop();
    }

    public void DisAbleItemOnShopOpen()
    {
        // if item is already bught disable the previously bought items
        for (int i = 0; i < disabledSlots.Count; i++)
        {
            disabledSlots[i].SetBuyButtonTo(false);
            disabledSlots[i].ItemSold(true);
        }
    }

    public void ClearAllLists()
    {
        if (copiedSlots.Count != shopItemList.Count)
        {
            Debug.LogError("Different Number of shop");
            return;
        }
        for (int i = 0; i < shopItemList.Count; i++)
        {
            copiedSlots[i].gameObject.SetActive(false);
        }
        currentSlots.Clear();
        copiedSlots.Clear();
        disabledSlots.Clear();
        shopItemList.Clear();
    }

    // 배치하는것 shop 에
    private void LoadShop()
    {
        for(int i = 0; i < currentItemSlotNum; i++)
        {
            //Slot temp = slotPool.GetFromPool();
            Slot temp = Instantiate(slotPrefab, ShopSlots.transform);
            temp.SetSlot(shopItemList[i], i);
            currentSlots.Add(temp);
            copiedSlots.Add(temp);
        }
        CheckEnoughMoney();
    }

    public void CloseShop()
    {
        Shop.gameObject.SetActive(false);
        Shop.SetAsLastSibling();
    }

    public void OpenShop()
    {
        Shop.gameObject.SetActive(true);
        DisAbleItemOnShopOpen();
        Shop.SetAsLastSibling();
        CheckEnoughMoney();
    }

    public void DeleteSlot(int index)
    {
        if(currentSlots.Count <= 1)
        {
            disabledSlots.Add(currentSlots[0]);
            currentSlots.RemoveAt(0);
            return;
        }

        disabledSlots.Add(currentSlots[index]);
        currentSlots.RemoveAt(index);
        for (int i = 0; i < currentSlots.Count; i++)
        {
            if(currentSlots[i].index < index)
            {
                continue;
            }
            else if(currentSlots[i].index > index)
            {
                currentSlots[i].index--;
            } 
        }
    }

    public void CheckEnoughMoney()
    {
        if (currentSlots.Count == 0)
        {
            return;
        }
        else
        {
            for (int i = 0; i < currentSlots.Count; i++)
            {
                if (PlayerController.Instance.GetCurrentGold() >= currentSlots[i].GetItem().price)
                {
                    currentSlots[i].SetBuyButtonTo(true);
                    // enable buttons
                }
                else
                {
                    currentSlots[i].SetBuyButtonTo(false);
                }
            }
        }
    }

    public PlayerData GetPlayerData()
    {
        return pData;
    }
}
