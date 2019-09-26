using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected string char_name;
    [SerializeField]
    protected float currentHP;
    [SerializeField]
    protected float maxHP;
    [SerializeField]
    protected float attack;
    [SerializeField]
    protected float defense;

    public bool isAlive;

    public Character(string m_name, float hp, float m_attk, float m_def)
    {
        char_name = m_name;
        maxHP = hp;
        currentHP = hp;
        attack = m_attk;
        defense = m_def;
        isAlive = true;
    }

    public bool isDead()
    {
        isAlive = false;
        return currentHP <= 0;
    }

    public abstract bool IsEnemy();

    public abstract bool IsPlayer();

    public void Attack(Character target)
    {
        if(target.IsPlayer())
        {
            PlayerController temp = ((PlayerController)target);
            temp.LoseHP(attack);
            Debug.Log("Player lost hp");
            temp.health.ShowHP(target.getCurrentHP(), target.getMaxHP());
            // if dead gameover
            if (temp.isDead())
            {
                Debug.Log("Player dead!!");
            }
        } else if(target.IsEnemy())
        {

            Enemy temp = ((Enemy)target);
            temp.LoseHP(attack);
            Debug.Log("Enemy lost hp");
            temp.health.ShowHP(target.getCurrentHP(), target.getMaxHP());
            // if dead
            if (temp.isDead())
            {
                temp.anim.SetBool("isIdle", false);
                temp.anim.SetBool("isDead", true);

               
                Debug.Log("Enemy dead!!");
                temp.gameObject.SetActive(false);

                if (GameController.Instance.AllEnemiesDead())
                {
                    GameController.Instance.TurnToggle(true);
                    temp.health.transform.parent.gameObject.SetActive(false);
                    GameController.Instance.EmptyEnemiesAndHealth();
                    GameController.Instance.SpawnEnemy(5f);
                } else
                {

                }
                
            }
        } else
        {
            Debug.Log("Errorrrrrrrrrrr");
        }

        
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

    private void Start()
    {
        
    }
}
