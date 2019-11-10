using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePool : ObjectPool<Transform>
{
    [SerializeField]
    private Canvas canvas;

    protected override Transform AddFunctionality(int id)
    {
        Transform tt = Instantiate(mOrigin[id], canvas.transform);
        mPool[id].Add(tt);
        return tt;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
