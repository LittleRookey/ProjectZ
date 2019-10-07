﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveClass : MonoBehaviour
{
    private static SaveClass instance;

    public static SaveClass Instance
    {
        get
        {
            return instance;
        }
    }

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
    [SerializeField]
    private PlayerData playerData;

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();
        // user는 데이터클래스
        bf.Serialize(stream, playerData);
        PlayerPrefs.SetString("player", Convert.ToBase64String(stream.GetBuffer()));
        stream.Close();

        //추가
        //stream = new MemoryStream();
        //bf.Serialize(stream, options);
        //PlayerPrefs.SetString("option", Convert.ToBase64String(stream.GetBuffer()));
        //stream.Close();

        //PlayerPrefs.SetString("playerName", userName);
    }

    public void LoadGame()
    {
        string data = PlayerPrefs.GetString("player");
        BinaryFormatter bf = new BinaryFormatter();

        if (!string.IsNullOrEmpty(data))
        {
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(data));
            playerData = (PlayerData)bf.Deserialize(stream);
            stream.Close();
            Debug.Log("levelssss" + playerData.player_level);
        }
        else
        {
            // 새로 기본 스탯을 준디. 
            
            SetNewData();
        }
    }

    public void SetNewData()
    {
        Debug.Log("SetnewData");
        playerData.player_name = "player" + UnityEngine.Random.Range(0, 99999).ToString();
        playerData.player_currentHP = 100;
        playerData.player_maxHP = 100;
        playerData.player_attack = 10;
        playerData.player_defense = 5;
        playerData.player_gold = 10;
        playerData.player_level = 1;
        playerData.player_maxExp = 10;
        playerData.player_currentExp = 0;
        //playerData.player_Inventory = new List<Item>();
        

        playerData.game_stage = 1;
        playerData.game_currentEnemy = new List<Enemy>();
        playerData.game_enemySpawnedThisRound = playerData.game_stage % 3 + 1;
        SaveGame();
    }
}
