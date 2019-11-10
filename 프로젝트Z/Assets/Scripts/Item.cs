using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eItemType
{
    potion_HpLow,
    potion_HpMid,
    potion_HpHigh,
    potion_ExpLow,
    potion_ExpMid,
    potion_ExpHigh,
    
    potion_AttkLow,
    potion_AttkMid,
    potion_AttkHigh,
    potion_DefenseLow,
    potion_DefenseMid,
    potion_DefenseHigh,

    potion_CritRate,
    potion_CritDamage
}

[System.Serializable]
public class Item
{
    public string Name;
    public Sprite icon;
    public int ID;
    public string Contents;
    public float Value;
    public eItemType itemType;
    public double price;
    public int itemCount;
    public bool isDefaultItem;
    
    public Item Clone()
    {
        return (Item)this.MemberwiseClone();
    }

    public virtual void Use()
    {
        // use item
        GameController.Instance.UseItem(this);
        itemCount--;
        UIController.Instance.UpdatePlayerInfoStat();
    }

    
}
