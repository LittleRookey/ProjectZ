using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private Text itemNameText, priceText;

    [SerializeField]
    public Button buyButton;

    public int index;

    private Item itemCopy;

    [SerializeField]
    private Image sold;


    private void OnEnable()
    {
        //buyButton
        //buyButton.onClick.AddListener(BuyItem);
        buyButton.interactable = true;
        ItemSold(false);
    }

    public void SetSlot(Item item, int inde)
    {
        itemCopy = item;
        itemImage.sprite = item.icon;
        itemNameText.text = item.Name;
        priceText.text = item.price.ToString() + "G";
        index = inde;
    }

    public Button GetButton()
    {
        //buyButton.onClick.AddListener(() => { Function(param); OtherFunction(param); });
        return buyButton;
    }

    public void BuyItem()
    {
        if(PlayerController.Instance.GetCurrentGold() < itemCopy.price)
        {
            MessageGUI.Instance.ShowMessage("골드가 부족합니다.");
            return;
        }

        PlayerController.Instance.BuyItem(itemCopy);
        //PlayerController.Instance.AddToInventory(itemCopy);
        Inventory.Instance.Add(itemCopy);
        sold.gameObject.SetActive(true);
        ShopController.Instance.DeleteSlot(index);
        buyButton.interactable = false;
        UIController.Instance.UpdatePlayerGold();
        ShopController.Instance.CheckEnoughMoney();
        //gameObject.SetActive(false);
    }

    public void SetItem(Item item)
    {
        itemCopy = item.Clone();
    }

    public void SetBuyButtonTo(bool enabled)
    {
        buyButton.interactable = enabled;
    }

    public void ItemSold(bool tt)
    {
        sold.gameObject.SetActive(tt);
    }

    public Item GetItem()
    {
        return itemCopy;
    }
}
