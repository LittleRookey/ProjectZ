using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eEnemyStatus
{
    dead, Idle, Attack
};

public class Enemy : Character
{
    public Animator anim;

    public Health health;
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
    public eEnemyStatus enemyStatus;
    private static float ONE_BASE_HP = 100;
    private static float ONE_BASE_ATTACK = 10;
    private static float ONE_BASE_DEFENSE = 5;
    [SerializeField]
    private bool isTank;
    [SerializeField]
    private bool isDPS;

    //[SerializeField]
    //private List<Skill> skills;

    public void Init(float hp, float attk, float def, int m_dropGold, int m_dropExp)
    {
        maxHP = hp;
        currentHP = maxHP;
        attack = attk;
        defense = def;
        dropGold = m_dropGold;
        dropExp = m_dropExp;
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

    public override bool IsEnemy()
    {
        return true;
    }

    public override bool IsPlayer()
    {
        return false;
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
    }

    
    public IEnumerator Action()
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

    private IEnumerator AttackPlayer(PlayerController player)
    {
        // appear aura

        yield return new WaitForSeconds(2f);
        Attack(player);

    }
    
    
}
