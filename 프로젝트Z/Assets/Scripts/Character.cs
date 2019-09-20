using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected string char_name;

    protected float currentHP;
    [SerializeField]
    protected float maxHP;
    [SerializeField]
    protected float attack;
    [SerializeField]
    protected float defense;
    

    public Character(string m_name, float hp, float m_attk, float m_def)
    {
        char_name = m_name;
        maxHP = hp;
        currentHP = maxHP;
        attack = m_attk;
        defense = m_def;
    }

    public bool isDead()
    {
        return currentHP <= 0;
    }

    public abstract bool IsEnemy();

    public abstract bool IsPlayer();

    public void Attack(Character target)
    {
        target.LoseHP(attack);
    }

    public void LoseHP(float HP)
    {
        currentHP = currentHP - HP + defense;
    }

    public string getName()
    {
        return char_name;
    }

    public float getMaxHP()
    {
        return maxHP;
    }
    
    public float getCurrentHP()
    {
        return currentHP;
    }

    public float getAttack()
    {
        return attack;
    }

    public float getDefense()
    {
        return defense;
    }

}
