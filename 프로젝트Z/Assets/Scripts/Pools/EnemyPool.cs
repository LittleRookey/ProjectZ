using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : ObjectPool<Enemy>
{
    [SerializeField]
    private VFXPool vPool;

    [SerializeField]
    private GameObject recycleEnemyBin;

    protected override Enemy AddFunctionality(int id)
    {
        Enemy enem = Instantiate(mOrigin[id], recycleEnemyBin.transform);
        
        enem.SetVFXPool(vPool);
        mPool[id].Add(enem);
        return enem;
    }
    //protected override Enemy AddFunctionality(int id)
    //{
    //    Enemy enem = Instantiate(mOrigin[id]);
    //    enem.ResetMonster();
    //    enem.SetHealth()
    //    return base.AddFunctionality(id);
    //}
}
