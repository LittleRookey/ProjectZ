using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;

    public static PlayerController Instance
    {
        get
        {
            return instance;
        }
    }


    //[SerializeField]
    //private string char_name;
    //[SerializeField]
    //private float currentHP;
    //[SerializeField]
    //private float maxHP;
    //[SerializeField]
    //private float attack;
    //[SerializeField]
    //private float defense;
    //[SerializeField]
    //private int m_gold;
    //[SerializeField]
    //private int level;
    //[SerializeField]
    //private float maxExp;
    //[SerializeField]
    //private float currentExp;

    public bool isAlive;

    [SerializeField]
    private DamageTextPool dmgPool;

    [SerializeField]
    private VFXPool vPool;

    [SerializeField]
    private ExpManager expManager;

    [SerializeField]
    private PlayerData playerData;

    public Health health;

    private List<GameObject> skills;

    private List<Item> p_inventory;

    private bool blockSkillOn = false;

    Vector3 randomizeVector = new Vector3(.2f, 0, 0);

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        skills = new List<GameObject>();
    }

    public bool isDead()
    {
        if(getCurrentHP() > 0)
        {
            return false;
        } 

        isAlive = false;
        return true;
    }

    // damage received
    public float CalculateDamage(float atk)
    {
        if(atk - playerData.player_defense <= 0)
        {
            return 0;
        }
        return atk - playerData.player_defense;

    }

    public void LoseHealth(float atk)
    {
        if(blockSkillOn)
        {
            return;
        }
        playerData.player_currentHP -= atk;
    }

    public void BlockDamage()
    {
        blockSkillOn = true;
    }

    public void StopBlock()
    {
        blockSkillOn = false;
    }
    public void SetPlayerData(PlayerData pData)
    {
        playerData = pData;
    }

    public void Touch(Vector3 hitPos)
    {
        Attack(GameController.Instance.GetFrontEnemy());
        Timer t = vPool.GetFromPool(1);
        t.transform.position = hitPos;
    }
    //public void Init(string p_name, int p_hp, int p_attk, int p_def)
    //{
    //    char_name = p_name;
    //    maxHP = p_hp;
    //    currentHP = maxHP;
    //    attack = p_attk;
    //    defense = p_def;
    //    level = 1;
    //    m_gold = 0;
    //    currentExp = 0;
    //    maxExp = (int)(10 * (Mathf.Log(level, 2) + 1));
    //    p_inventory = new List<Item>();
    //}


       

    public void GainGoldAndExp(Enemy enemy)
    {
        playerData.player_gold += enemy.getDropGold();
        // if given exp allows level up
        if(playerData.player_currentExp + enemy.getDropExp() >= playerData.player_maxExp)
        {
            Debug.Log(playerData.player_currentExp + ", " + enemy.getDropExp() + " max:"
                + playerData.player_maxExp);
            Debug.Log("LVLUP");
            // TODO player level up
            float remainExp = enemy.getDropExp() + playerData.player_currentExp - playerData.player_maxExp;

            LevelUp();
            playerData.player_currentExp += remainExp;
            expManager.UpdateExp(playerData.player_currentExp, playerData.player_maxExp);
            while (playerData.player_currentExp >= playerData.player_maxExp) {
                // levels up multiple time as currentExp exceeds max exp
                if (playerData.player_currentExp >= playerData.player_maxExp)
                {
                    float leftExp = playerData.player_currentExp - playerData.player_maxExp;
                    LevelUp();

                    playerData.player_currentExp += leftExp;
                }
            }
            expManager.UpdateExp(playerData.player_currentExp, playerData.player_maxExp);

        } else
        {
            Debug.Log("Gained exp");
            playerData.player_currentExp += enemy.getDropExp();
            expManager.UpdateExp(playerData.player_currentExp, playerData.player_maxExp);
            GameController.Instance.SaveGame();
        }
        UIController.Instance.UpdatePlayerInfoStat();
        
    }

    public void LevelUp()
    {
        playerData.player_level++;
        // TODO text popup
        UIController.Instance.UpdatePlayerLevel();
        
        playerData.player_maxHP += (int)(Mathf.Pow(2, (float)(playerData.player_level * .08) + 1));
        playerData.player_attack += (int)(Mathf.Pow(2, (float)(playerData.player_level * .08) + 1));
        playerData.player_defense += (int)(Mathf.Pow(2, (float)(playerData.player_level * .08) + 1));
        playerData.player_currentHP = playerData.player_maxHP;
        playerData.player_maxExp += (int)(Mathf.Pow(2, (float)(playerData.player_level * .08) + 1));
        playerData.player_currentExp = 0;
    }

    public void Attack(Enemy target)
    {
        float damaged = target.CalculateDamage(GameController.Instance.GetPlayerData().player_attack);
        //temp.LoseHealth(damaged);
        Debug.Log(damaged);

        target.LoseHealth(damaged);

       // floating text (damage)
        ShowFloatingText(target, damaged);
        Debug.Log("SHow Text");
        

        target.health.ShowHP(target.getCurrentHP(), target.getMaxHP());

        if (!target.attackAnimationPlaying)
        {
            if (damaged > 0)
            {
                target.SetDamaged();
            }
        }
        //GameController.Instance.SaveGame();
        // if dead
        if (target.isDead())
        {

            target.SetDead();

            GainGoldAndExp(target);

            Debug.Log("Enemy dead!!");

            GameController.Instance.DeleteFirstEnemy();

            // when all enemies are dead in a round
            if (GameController.Instance.AllEnemiesDead())
            {
                Debug.Log("Enemies all dead");
                GameController.Instance.GoToNextStage();
            }

        }
        
       


    }

    private void ShowFloatingText(Enemy enemy, float damage)
    {
        Text go = dmgPool.GetFromPool();
        go.text = damage.ToString();
        //temp.transform.position = GameController.Instance.GetFrontEnemy().gameObject.transform.position + Vector3.up * 2;
        
        go.transform.position = GameController.Instance.GetFrontEnemy().gameObject.transform.position + Vector3.up * 1.5f;
        go.transform.position += new Vector3(Random.Range(-randomizeVector.x, randomizeVector.x), 0, 0);
        Debug.Log(enemy.transform.position);
        //go.gameObject.transform.localPosition += new Vector3(0, 2, 0);
    }

    public string getName()
    {
        return playerData.player_name;
    }

    public float getMaxHP()
    {
        return playerData.player_maxHP;
    }

    public float getCurrentHP()
    {
        return playerData.player_currentHP;
    }

    public float getAttack()
    {
        return playerData.player_attack;
    }

    public float getDefense()
    {
        return playerData.player_defense;
    }

    public int GetLevel()
    {
        return playerData.player_level;
    }

    public int GetCurrentGold()
    {
        return playerData.player_gold;
    }

    public float GetCurrentExp()
    {
        return playerData.player_currentExp;
    }

    public float GetMaxExp()
    {
        return playerData.player_maxExp;
    }

    public void AddExp(float add)
    {
        playerData.player_currentExp += add;
    }

}
