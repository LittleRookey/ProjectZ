using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{ 
    private int _gold;

    public int Gold
    {
        get
        {
            return (int)(Random.Range(_gold/2, _gold * 1.5f));
        }
        set
        {
            _gold = value;
        }
    }

    [SerializeField]
    private int Exp;

    [SerializeField]
    private List<GameObject> dropItems;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
