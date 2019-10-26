using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T: Component
{
    [SerializeField]
    protected T[] mOrigin;
    protected List<T>[] mPool;

    // Start is called before the first frame update
    private void Awake()
    {
        mPool = new List<T>[mOrigin.Length];
        for (int i = 0; i < mPool.Length; i++)
        {
            mPool[i] = new List<T>();
        }
    }
    

    public virtual T GetFromPool(int id)
    {
        
        for(int i = 0; i < mPool[id].Count; i++)
        {
            if(!mPool[id][i].gameObject.activeInHierarchy)
            {
                mPool[id][i].gameObject.SetActive(true);
                return mPool[id][i];
            }
        }

        T temp = Instantiate(mOrigin[id]);
        mPool[id].Add(temp);
        return temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
