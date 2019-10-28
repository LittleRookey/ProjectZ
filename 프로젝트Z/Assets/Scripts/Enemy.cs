﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eEnemyStatus
{
    dead, Idle, Attack
};


public class Enemy : MonoBehaviour
{
    public Animator anim;

    public HPAndSpeedManager health;
    public Text healthText;
    [SerializeField]
    private string char_name;
    [SerializeField]
    private float currentHP;
    [SerializeField]
    private float maxHP;
    [SerializeField]
    private float attack;
    [SerializeField]
    private float defense;
    [SerializeField]
    private int forwardNumber; // bigger the forward number is, enemy will stand at the front line
    public bool isAlive;
    [SerializeField]
    private int dropGold;
    [SerializeField]
    private int dropExp;
    [SerializeField]
    private int id;
    public eEnemyStatus enemyStatus;
    private static float ONE_BASE_HP = 100;
    private static float ONE_BASE_ATTACK = 10;
    private static float ONE_BASE_DEFENSE = 5;
    [SerializeField]
    private bool isTank;
    [SerializeField]
    private bool isDPS;

    public bool attackAnimationPlaying;

    [SerializeField]
    private MonsterData monsterData;

    [SerializeField]
    private GameObject floatingTextPrefab;

    //[SerializeField]
    //private List<Skill> skills;


    private void OnEnable()
    { 
        isAlive = true;
    }

    public void Init(float hp, float attk, float def, int m_dropGold, int m_dropExp)
    {
        maxHP = hp;
        currentHP = maxHP;
        attack = attk;
        defense = def;
        dropGold = m_dropGold;
        dropExp = m_dropExp;
    }

    public void ResetMonster()
    {
        currentHP = maxHP;
        //health.ShowHP(currentHP, maxHP);
    }

    public bool isDead()
    {
        if (currentHP > 0)
        {
            return false;
        }

        isAlive = false;
        return true;
    }

    public int getID()
    {
        return id;
    }

    public void Attack(PlayerController target)
    {
        target.LoseHealth(attack);
        Debug.Log("Player lost hp");

        //if(floatingTextPrefab)
        //{
        //    ShowFloatingText();
        //    Debug.Log("SHow Text");
        //}

        target.health.ShowHP(target.getCurrentHP(), target.getMaxHP());

        // if dead gameover
        if (target.isDead())
        {
            Debug.Log("Player dead!!");
            //animate dead anim

            // give penalty to player

            // Reload scene

        }
        

    }

    private void ShowFloatingText()
    {
        Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
    }

    // damage received
    public float CalculateDamage(float atk)
    {
        return atk - defense;

    }

    public void LoseHealth(float atk)
    {
        if (atk <= 0)
        {
            return;
        }
        currentHP -= atk;
    }

    public int getDropGold()
    {
        return dropGold;
    }

    public int getDropExp()
    {
        return dropExp;
    }

    public int getForwardNumber()
    {
        return forwardNumber;
    }

    public eEnemyStatus getEnemyState()
    {
        return enemyStatus;
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

    public void SetHealth(Health hps)
    {
        health = hps;
    }
    public void LoseOneHP()
    {
        currentHP--;
    }

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Action());
        enemyStatus = eEnemyStatus.Idle;
        isAlive = true;
        attackAnimationPlaying = false;
    }

    
    public IEnumerator Action(int enemyBehavePattern)
    {
        while (true)
        {
            switch (enemyStatus)
            {
                case eEnemyStatus.Idle:
                    // fade in
                    
                    break;
                case eEnemyStatus.Attack:
                    // constantly attacks player
                    int patt = Random.Range(0, 5);

                    anim.SetTrigger("attack");
                    break;
                case eEnemyStatus.dead:
                    anim.SetTrigger("dead");
                    //fade away
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    //public Enemy Clone()
    //{
    //    return 
    //}
    private IEnumerator AttackPlayer(PlayerController player)
    {
        // appear aura

        yield return new WaitForSeconds(2f);
        Attack(player);

    }
    
    public void SetDamaged()
    {
        anim.SetTrigger("damage");
        attackAnimationPlaying = true;
    }

    public void SetIdle()
    {
        anim.SetTrigger("Idle");
        attackAnimationPlaying = false;
    }

    public void SetDead()
    {
        anim.SetTrigger("dead");
        attackAnimationPlaying = false;
        health.gameObject.SetActive(false);
    }
    
}
