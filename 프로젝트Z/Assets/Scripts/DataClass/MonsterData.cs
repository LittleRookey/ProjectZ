using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData 
{
    public string char_name;
    public float currentHP;
    public float maxHP;
    public float attack;
    public float defense;
    public int forwardNumber; // bigger the forward number is, enemy will stand at the front line
    public bool isAlive;
    public int dropGold;
    public int dropExp;
    public int id;
    public eEnemyStatus enemyStatus;
}
