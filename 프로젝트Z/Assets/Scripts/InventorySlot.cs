using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    public Image icon;

    [SerializeField]
    private Item item;

    [SerializeField]
    private Image itemCountBG;

    [SerializeField]
    private Text itemCountText;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        UpdateItemCount(item.itemCount);
        itemCountBG.gameObject.SetActive(true);
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        itemCountBG.gameObject.SetActive(false);
    }

    public void UpdateItemCount(int count)
    {
        itemCountText.text = count.ToString();
    }
    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}
