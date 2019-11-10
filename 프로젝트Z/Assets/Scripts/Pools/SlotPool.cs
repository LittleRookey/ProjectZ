using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPool : ObjectPool<Slot>
{
    [SerializeField]
    private Transform ShopSlots;

    protected override Slot AddFunctionality(int id)
    {
        Slot slot = Instantiate(mOrigin[id], ShopSlots.transform);
        //bool hasItemAlready = false;
        //for(int i = 0; i < mOrigin.Length; i++)
        //{
        //    if(mOrigin[i].GetItem().ID == slot.GetItem().ID)
        //    {
        //        hasItemAlready = true;
        //    }
        //}
        //// if item does not exist in pool
        //if(!hasItemAlready)
        //{
        //    mOrigin.SetValue(slot , mOrigin.Length + 1);
        //}
        ////if (slot.GetItem().ID )
        mPool[id].Add(slot);
        return slot;

    }
}
