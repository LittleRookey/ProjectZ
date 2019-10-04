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
            temp.LoseHealth(attack);
            Debug.Log("Player lost hp");
            temp.health.ShowHP(temp.getCurrentHP(), temp.getMaxHP());
            // if dead gameover
            if (temp.isDead())
            {
                Debug.Log("Player dead!!");
            }
        }
        else if(target.IsEnemy())
        {
            Enemy temp = ((Enemy)target);

            
            float damaged = temp.CalculateDamage(attack);
            //temp.LoseHealth(damaged);
            Debug.Log(damaged);

            temp.LoseHealth(damaged);
            temp.health.ShowHP(temp.getCurrentHP(), temp.getMaxHP());
            // if dead
            if (temp.isDead())
            {

                temp.anim.SetTrigger("dead");

                PlayerController.Instance.GainGoldAndExp(temp);

                Debug.Log("Enemy dead!!");

                temp.gameObject.SetActive(false);
                temp.health.transform.parent.gameObject.SetActive(false);
                GameController.Instance.currentEnemy.RemoveAt(0);
                
                // when all enemies are dead in a round
                if (GameController.Instance.AllEnemiesDead())
                {
                    Debug.Log("Enemies all dead");
                    TouchManager.Instance.gameObject.SetActive(false);
                    GameController.Instance.TurnToggle(true);
                    GameController.Instance.EmptyEnemiesAndHealth();
                    GameController.Instance.SpawnEnemy(5f);

                } 
                
            }
        } else
        {
            Debug.Log("Errorrrrrrrrrrr");
        }

        
    }

    // damage received
    public float CalculateDamage(float atk)
    {
        return atk - defense;
        
    }

    public void LoseHealth(float atk)
    {
        if(atk <= 0)
        {
            return;
        }
        currentHP -= atk;
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

    public void setCurrentHP(float given)
    {
        currentHP = given;
    }

    public void setMaxHP(float given)
    {
        maxHP = given;
    }
    private void Start()
    {
        
    }
}
