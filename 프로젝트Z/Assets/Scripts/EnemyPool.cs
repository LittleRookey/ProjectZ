using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public Enemy origin;
    private List<Enemy> pool;
    // Start is called before the first frame update
    void Start()
    {
        pool = new List<Enemy>();
    }

    public Enemy GetFromPool()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                pool[i].gameObject.SetActive(true);
                return pool[i];
            }
        }
        Enemy newObj = Instantiate(origin);
        pool.Add(newObj);
        return newObj;
    }
}
