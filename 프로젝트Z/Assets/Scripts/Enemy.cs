using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEnemyStatus
{
    dead, Idle
};

public class Enemy : Character
{
    public Animator anim;

    public Health health;

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
        enemyStatus = eEnemyStatus.Idle;
    }

    public int getDropGold()
    {
        return dropGold;
    }

    public int getDropExp()
    {
        return dropExp;
    }

    public eEnemyStatus getEnemyState()
    {
        return enemyStatus;
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

    public bool isActive()
    {
        return gameObject.activeInHierarchy;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.FindGameObjectWithTag("HP").GetComponentInChildren<Health>();
        //StartCoroutine(Action());
        enemyStatus = eEnemyStatus.Idle;
    }

    
    public IEnumerator Action()
    {
        while (true)
        {
            switch (enemyStatus)
            {
                case eEnemyStatus.Idle:
                    // fade in
                    anim.SetBool("isIdle", true);
                    break;
                case eEnemyStatus.dead:
                    anim.SetBool("isDead", true);
                    //fade away
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    
}
