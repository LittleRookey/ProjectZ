using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextPool : ObjectPool<Transform>
{

    [SerializeField]
    private Canvas canvas;

    protected override Transform AddFunctionality(int id)
    {
        Transform temp = Instantiate(mOrigin[id], canvas.transform);

        mPool[id].Add(temp);

        return temp;
    }

  
}
