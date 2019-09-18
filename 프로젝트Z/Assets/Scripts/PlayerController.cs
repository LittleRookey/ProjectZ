using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    private int m_gold;

    public PlayerController(string p_name, float p_hp, float p_attk, float p_def, int gold) 
        : base(p_name, p_hp, p_attk, p_def)
    {
        m_gold = gold;
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
