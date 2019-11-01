using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHPBarPool : ObjectPool<HPAndSpeedManager>
{
    [SerializeField]
    private Canvas canvas;

    // Start is called before the first frame update

    protected override HPAndSpeedManager AddFunctionality(int id)
    {
        HPAndSpeedManager temp = Instantiate(mOrigin[id], canvas.transform);
        mPool[id].Add(temp);
        return temp;
    }
}
