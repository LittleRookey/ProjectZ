using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPool : ObjectPool<Enemy>
{
    [SerializeField]
    private VFXPool vPool;

    protected override Enemy AddFunctionality(int id)
    {
        Enemy enem = Instantiate(mOrigin[id]);
        enem.SetVFXPool(vPool);
        mPool[id].Add(enem);
        return enem;
    }
}
