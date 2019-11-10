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

    public bool IsAlive
    {
        get
        {
            return playerData.player_currentHP > 0;
        }
    }

    [SerializeField]
    private ShopController shopControl;

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

    // damage received
    public float CalculateDamage(float atk)
    {

        if(atk - playerData.player_defense <= 0)
        {
            return 0;
        }
        else if(atk - playerData.player_defense <= 0)
        {
            return 1;
        }
        float randNum = Random.Range(0f, 100f);
        if (randNum <= playerData.player_critRate)
        {
            return (atk - playerData.player_defense) * 1.5f;
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

    public void GainHealth(float given)
    {
        if (given + playerData.player_currentHP > playerData.player_maxHP)
        {
            playerData.player_currentHP = playerData.player_maxHP;
        }
        else
        {
            playerData.player_currentHP += given;
        }
        health.ShowHPPlayer(playerData.player_currentHP, playerData.player_maxHP);
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
        Timer t = vPool.GetFromPool(Random.Range(0, 2));
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
            // TODO player level up
            float remainExp = enemy.getDropExp() + playerData.player_currentExp - playerData.player_maxExp;

            LevelUp();
            Transform tt = dmgPool.GetFromPool(2);
            tt.position = transform.position + Vector3.up * 5f;
            //tt.

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
            playerData.player_currentExp += enemy.getDropExp();
            expManager.UpdateExp(playerData.player_currentExp, playerData.player_maxExp);
        }
        UIController.Instance.UpdatePlayerInfoStat();
        ShopController.Instance.CheckEnoughMoney();
        GameController.Instance.SaveGame();
    }
        
    public void GainExp(float value)
    {
        if (playerData.player_currentExp + value >= playerData.player_maxExp)
        {
            // TODO player level up
            float remainExp = value + playerData.player_currentExp - playerData.player_maxExp;

            LevelUp();
            Transform tt = dmgPool.GetFromPool(2);
            tt.position = transform.position + Vector3.up * 5f;
            //tt.

            playerData.player_currentExp += remainExp;
            expManager.UpdateExp(playerData.player_currentExp, playerData.player_maxExp);
            while (playerData.player_currentExp >= playerData.player_maxExp)
            {
                // levels up multiple time as currentExp exceeds max exp
                if (playerData.player_currentExp >= playerData.player_maxExp)
                {
                    float leftExp = playerData.player_currentExp - playerData.player_maxExp;
                    LevelUp();

                    playerData.player_currentExp += leftExp;
                }
            }
            expManager.UpdateExp(playerData.player_currentExp, playerData.player_maxExp);

        }
        else
        {
            playerData.player_currentExp += value;
            expManager.UpdateExp(playerData.player_currentExp, playerData.player_maxExp);

        }
        UIController.Instance.UpdatePlayerInfoStat();
        ShopController.Instance.CheckEnoughMoney();
        GameController.Instance.SaveGame();
    }
    public void LevelUp()
    {
        playerData.player_level++;
        // TODO text popup
        UIController.Instance.UpdatePlayerLevel();
        
        playerData.player_maxHP += (int)(Mathf.Pow(2, (float)(playerData.player_level * .08) + 1));
        playerData.player_attack += (int)(Mathf.Pow(2, (float)(playerData.player_level * .08) + 1));
        playerData.player_defense += (int)(Mathf.Pow(2, (float)(playerData.player_level * .08) + 1));
        playerData.player_maxExp += (int)(Mathf.Pow(2, (float)(playerData.player_level * .08) + 1));
        playerData.player_currentExp = 0;
        playerData.player_currentHP = playerData.player_maxHP;
        health.ShowHPPlayer(playerData.player_currentHP, playerData.player_maxHP);
    }

    public void Attack(Enemy target)
    {
        float plyeratk = GameController.Instance.GetPlayerData().player_attack;
        float damaged = target.CalculateDamage(plyeratk);

        target.LoseHealth(damaged);
        // floating text (damage)
        if (damaged > plyeratk - target.getDefense() && damaged > 1)
        {
            //crit text
            
            ShowCritText(target, damaged);
        }
        else
        {
            ShowFloatingText(target, damaged);
        }

        

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
        if (!target.IsAlive)
        {
           
            target.SetDead();
            GainGoldAndExp(target);

            GameController.Instance.DeleteFirstEnemy();

            // when all enemies are dead in a round
            if (GameController.Instance.AllEnemiesDead())
            {
                GameController.Instance.GoToNextStage();
            }
        }
    }

    private void ShowFloatingText(Enemy enemy, float damage)
    {
        Transform go = dmgPool.GetFromPool();
        go.gameObject.GetComponentInChildren<Text>().text = damage.ToString();
        //temp.transform.position = GameController.Instance.GetFrontEnemy().gameObject.transform.position + Vector3.up * 2;

        if (enemy.isEnemy)
        {
            go.transform.position = GameController.Instance.GetFrontEnemy().gameObject.transform.position + Vector3.up * 1.5f;
            go.transform.position += new Vector3(Random.Range(-randomizeVector.x, randomizeVector.x), 0, 0);
        } else
        {
            go.transform.position = GameController.Instance.GetFrontEnemy().gameObject.transform.position + Vector3.up * 3f;
            go.transform.position += new Vector3(Random.Range(-randomizeVector.x, randomizeVector.x), 0, 0);
        }
        //go.gameObject.transform.localPosition += new Vector3(0, 2, 0);
    }

    private void ShowCritText(Enemy enemy, float damage)
    {
        Transform go = dmgPool.GetFromPool(1);
        go.gameObject.GetComponentInChildren<Text>().text = damage.ToString();
        //temp.transform.position = GameController.Instance.GetFrontEnemy().gameObject.transform.position + Vector3.up * 2;

        if (enemy.isEnemy)
        {
            go.transform.position = GameController.Instance.GetFrontEnemy().gameObject.transform.position + Vector3.up * 1.5f;
            go.transform.position += new Vector3(Random.Range(-randomizeVector.x, randomizeVector.x), 0, 0);
        }
        else
        {
            go.transform.position = GameController.Instance.GetFrontEnemy().gameObject.transform.position + Vector3.up * 3f;
            go.transform.position += new Vector3(Random.Range(-randomizeVector.x, randomizeVector.x), 0, 0);
        }
        //go.gameObject.transform.localPosition += new Vector3(0, 2, 0);
    }
    public void BuyItem(Item item)
    {
        Debug.Log(item.price);
        playerData.player_gold -= item.price;
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

    public double GetCurrentGold()
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

    //public List<Item> GetInventory()
    //{
    //    return playerData.player_Inventory;
    //}


    //public void AddToInventory(Item item)
    //{
    //    List<Item> inv = playerData.player_Inventory;
    //    for (int i = 0; i < inv.Count; i++)
    //    {
    //        if(item.ID == inv[i].ID)
    //        {
    //            // if there is item in the inventory
    //            inv[i].itemCount++;
    //            break;
    //        }
    //    }

    //    // if there is no item, add it to the inventory 

    //}
}
