using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected string char_name;
    protected float currentHP;
    protected float maxHP;
    protected float attack;
    protected float defense;
    

    public Character(string m_name, float hp, float m_attk, float m_def)
    {
        char_name = m_name;
        maxHP = hp;
        currentHP = maxHP;
        attack = m_attk;
        defense = m_def;
    }


    public void Attack(Character target)
    {
        target.LoseHP(attack);
    }

    public void LoseHP(float HP)
    {
        currentHP = currentHP - HP + defense;
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
