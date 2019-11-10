using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class PlayerData 
{
    // PlayerController
    public string player_name;
    public float player_currentHP;
    public float player_maxHP;
    public float player_attack;
    public float player_defense;
    public double player_gold;
    public int player_level;
    public float player_maxExp;
    public float player_currentExp;
    //public List<Item> player_Inventory;
    //public List<bool> player_InventoryFull;
    //public Inventory player_Inventory;
    public float player_critRate;
    public float player_critDamage;

    // GameController
    public int game_stage;
    //public EnemySaveData game_currentEnemy;
    public int game_enemySpawnedThisRound;
    public int game_enemySpawnLimitStage;

    // Enemy


    // ShopController
    public List<Item> currentlyActiveSlots;
    public List<Item> currentShopList;

}
