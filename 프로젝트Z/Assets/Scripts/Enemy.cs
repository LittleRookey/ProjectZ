using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private int dropGold;
    private int dropExp;

    public Enemy(string e_name, float e_hp, float e_attk, float e_def, int m_dropGold, int m_dropExp)  
        :base(e_name, e_hp, e_attk, e_def)
    {
        dropGold = m_dropGold;
        dropExp = m_dropExp;
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
