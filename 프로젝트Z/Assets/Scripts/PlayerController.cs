using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
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
    private UIController uiControl;

    [SerializeField]
    private ExpManager expManager;

    public Health health;

    private List<Item> p_inventory;

    private GameController gameControl;

    private PlayerData playerData;

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
        gameControl = GameController.Instance;
        playerData = gameControl.GetPlayerData();
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
        playerData.player_currentHP -= atk;
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

    //public void SavePlayerData()
    //{
    //    PlayerData pd = gameControl.GetPlayerData();
    //    pd.player_name = char_name;
    //    pd.player_maxHP = maxHP;
    //    pd.player_currentHP = currentHP;
    //    pd.player_attack = attack;
    //    pd.player_defense = defense;
    //    pd.player_level = level;
    //    pd.player_gold = m_gold;
    //    pd.player_currentExp = currentExp;
    //    pd.player_maxExp = maxExp;
    //    //save.player_Inventory = p_inventory;
    //}

    //public void LoadPlayerData()
    //{
    //    PlayerData pd = gameControl.GetPlayerData();

    //    char_name = pd.player_name;
    //    maxHP = pd.player_maxHP;
    //    currentHP = pd.player_currentHP;
    //    attack = pd.player_attack;
    //    defense = pd.player_defense;
    //    level = pd.player_level;
    //    m_gold = pd.player_gold;
    //    currentExp = pd.player_currentExp;
    //    maxExp = pd.player_maxExp;
    //    //p_inventory = save.player_Inventory;
    //}
    public void GainGoldAndExp(Enemy enemy)
    {
        playerData.player_gold += enemy.getDropGold();
        if(playerData.player_currentExp + enemy.getDropExp() >= playerData.player_maxExp)
        {
            // TODO player level up
            float remainExp = enemy.getDropExp() + playerData.player_currentExp - playerData.player_maxExp;

            LevelUp();
            playerData.player_currentExp += remainExp;
            expManager.UpdateExp(playerData.player_currentExp, playerData.player_maxExp);
        } else
        {
            playerData.player_currentExp += enemy.getDropExp();
            expManager.UpdateExp(playerData.player_currentExp, playerData.player_maxExp);
        }

        
    }

    public void LevelUp()
    {
        playerData.player_level++;
        // TODO text popup
        uiControl.UpdatePlayerLevel();
        
        playerData.player_maxHP += (int)(Mathf.Pow(2, (float)(playerData.player_level * .03) + 1));
        playerData.player_attack += (int)(Mathf.Pow(2, (float)(playerData.player_level * .03) + 1));
        playerData.player_defense += (int)(Mathf.Pow(2, (float)(playerData.player_level * .03) + 1));
        playerData.player_currentHP = playerData.player_maxHP;

        playerData.player_currentExp = 0;
    }

    public override bool IsEnemy()
    {
        return false;
    }

    public override bool IsPlayer()
    {
        return true;
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
