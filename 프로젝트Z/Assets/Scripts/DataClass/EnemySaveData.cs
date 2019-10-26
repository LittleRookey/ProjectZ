using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemySaveData 
{
    public List<float> currentHP;
    public List<float> maxHP;
    public List<int> id;
    public List<bool> isAlive;

    //public void Clear()
    //{
    //    currentHP.Clear();
    //    maxHP.Clear();
    //    id.Clear();
    //    isAlive.Clear();
    //}

    //public void InitList()
    //{
    //    currentHP = new List<float>();
    //    maxHP = new List<float>();
    //    id = new List<int>();
    //    isAlive = new List<bool>();
    //}

    //public void addEnemy(Enemy enem)
    //{
    //    currentHP.Add(enem.getCurrentHP());
    //    maxHP.Add(enem.getMaxHP());
    //    id.Add(enem.getID());
    //    isAlive.Add(enem.isAlive);
    //}

    //public void deleteFirstEnemy()
    //{
    //    currentHP.RemoveAt(0);
    //    maxHP.RemoveAt(0);
    //    id.RemoveAt(0);
    //}


}
