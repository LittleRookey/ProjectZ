using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    [SerializeField]
    private int m_gold;

    private int level;
    [SerializeField]
    private int maxExp;
    [SerializeField]
    private int currentExp;

    public Health health;

    private List<Item> p_inventory;
    
    public PlayerController(string p_name, float p_hp, float p_attk, float p_def) 
        : base(p_name, p_hp, p_attk, p_def)
    {
        level = 1;
        m_gold = 0;
        currentExp = 0;
        maxExp = (int)(10 * (Mathf.Log(level, 2)  + 1));
        p_inventory = new List<Item>();
    }

    public void gainGoldAndExp(Enemy enemy)
    {
        m_gold += enemy.getDropGold();
        if(currentExp + enemy.getDropExp() >= maxExp)
        {
            // TODO player level up
            currentExp = 0;
            int remainExp = maxExp - currentExp;
            int addedExp = enemy.getDropExp() - remainExp;
            currentExp += addedExp;
        } else
        {
            currentExp += enemy.getDropExp();
        }
        
    }

    public void LevelUp()
    {
        level++;
        
        maxHP *= (Mathf.Log(level, 2) + 1);
        attack *= (Mathf.Log(level, 2) + 1);
        defense *= (Mathf.Log(level, 2) + 1);
        currentHP = maxHP;
        
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
