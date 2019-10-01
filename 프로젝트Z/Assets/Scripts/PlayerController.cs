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

    [SerializeField]
    private int m_gold;
    [SerializeField]
    private int level;
    [SerializeField]
    private float maxExp;
    [SerializeField]
    private float currentExp;

    [SerializeField]
    private UIController uiControl;

    [SerializeField]
    private ExpManager expManager;

    public Health health;

    private List<Item> p_inventory;
    
    //public PlayerController(string p_name, int p_hp, int p_attk, int p_def) 
    //    : base(p_name, p_hp, p_attk, p_def)
    //{
    //    level = 1;
    //    m_gold = 0;
    //    currentExp = 0;
    //    maxExp = (int)(10 * (Mathf.Log(level, 2)  + 1));
    //    p_inventory = new List<Item>();
    //}

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
    }
    private void Start()
    {
        Init("player", 10, 5, 0);
    }

    public void Init(string p_name, int p_hp, int p_attk, int p_def)
    {
        name = p_name;
        maxHP = p_hp;
        currentHP = maxHP;
        attack = p_attk;
        defense = p_def;
        level = 1;
        m_gold = 0;
        currentExp = 0;
        maxExp = (int)(10 * (Mathf.Log(level, 2) + 1));
        p_inventory = new List<Item>();
    }
    public void GainGoldAndExp(Enemy enemy)
    {
        m_gold += enemy.getDropGold();
        if(currentExp + enemy.getDropExp() >= maxExp)
        {
            // TODO player level up
            float remainExp = enemy.getDropExp() + currentExp - maxExp;

            LevelUp();
            currentExp += remainExp;
            expManager.UpdateExp(0f , currentExp, maxExp);
        } else
        {
            currentExp += enemy.getDropExp();
            expManager.UpdateExp(currentExp - enemy.getDropExp(), currentExp, maxExp);
        }

        
    }

    public void LevelUp()
    {
        level++;
        // TODO text popup
        uiControl.UpdatePlayerLevel();
        maxHP += (int)(Mathf.Log(level, 2) + 1);
        attack += (int)(Mathf.Log(level, 2) + 1);
        defense += (int)(Mathf.Log(level, 2) + 1);
        currentHP = maxHP;

        currentExp = 0;
    }

    public override bool IsEnemy()
    {
        return false;
    }

    public override bool IsPlayer()
    {
        return true;
    }

    public int GetLevel()
    {
        return level;
    }

    public float GetCurrentExp()
    {
        return currentExp;
    }

    public float GetMaxExp()
    {
        return maxExp;
    }

    public void AddExp(float add)
    {
        currentExp += add;
    }

}
