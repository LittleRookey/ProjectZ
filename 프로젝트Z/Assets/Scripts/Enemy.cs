using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEnemyStatus
{
    alive, dead, attack
};

public class Enemy : Character
{
    private int dropGold;
    private int dropExp;
    public eEnemyStatus enemyStatus;

    private static float ONE_BASE_HP = 100;
    private static float ONE_BASE_ATTACK = 10;
    private static float ONE_BASE_DEFENSE = 5;

    public Enemy(string e_name, float e_hp, float e_attk, float e_def, int m_dropGold, int m_dropExp)  
        :base(e_name, e_hp, e_attk, e_def)
    {
        dropGold = m_dropGold;
        dropExp = m_dropExp;
        enemyStatus = eEnemyStatus.alive;
    }

    public int getDropGold()
    {
        return dropGold;
    }

    public int getDropExp()
    {
        return dropExp;
    }

    public override bool IsEnemy()
    {
        return true;
    }

    public override bool IsPlayer()
    {
        return false;
    }

    public void SetHP(float givenHP)
    {
        maxHP = givenHP;
    }


    public void SetAttack(float givenAttack)
    {
        attack = givenAttack;
    }


    public void SetDefense(float givenDefense)
    {
        defense = givenDefense;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Action()
    {
        switch(enemyStatus)
        {
            case eEnemyStatus.alive:
                // fade in

                break;
            case eEnemyStatus.attack:
                // attack coroutine on + give damage
                break;
            case eEnemyStatus.dead:
                //fade away
                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
