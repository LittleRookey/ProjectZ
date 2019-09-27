using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected string char_name;
    [SerializeField]
    protected int currentHP;
    [SerializeField]
    protected int maxHP;
    [SerializeField]
    protected int attack;
    [SerializeField]
    protected int defense;

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
            temp.LoseHP(attack);
            Debug.Log("Player lost hp");
            temp.health.ShowHPAnimation(target.getCurrentHP(), target.getMaxHP(), attack);
            // if dead gameover
            if (temp.isDead())
            {
                Debug.Log("Player dead!!");
            }
        }
        else if(target.IsEnemy())
        {

            Enemy temp = ((Enemy)target);
            temp.LoseHP(attack);
            Debug.Log("Enemy lost hp");
            temp.health.ShowHPAnimation(temp.getCurrentHP(), temp.getMaxHP(), attack);
            // if dead
            if (temp.isDead())
            {
                
                temp.anim.SetBool("isIdle", false);
                temp.anim.SetBool("isDead", true);

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
    public void LoseHP(int atk)
    {
        currentHP -= atk + defense;
        
    }



    public string getName()
    {
        return char_name;
    }

    public int getMaxHP()
    {
        return maxHP;
    }
    
    public int getCurrentHP()
    {
        return currentHP;
    }

    public int getAttack()
    {
        return attack;
    }

    public int getDefense()
    {
        return defense;
    }

    private void Start()
    {
        
    }
}
