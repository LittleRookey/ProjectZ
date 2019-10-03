using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveClass : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

//    public void SaveGame()
//    {
//        BinaryFormatter bf = new BinaryFormatter();
//        MemoryStream stream = new MemoryStream();
//        // user는 데이터클래스
//        bf.Serialize(stream, user);
//        PlayerPrefs.SetString("player", Convert.ToBase64String(stream.GetBuffer()));
//        stream.Close();

//        //추가
//        stream = new MemoryStream();
//        bf.Serialize(stream, options);
//        PlayerPrefs.SetString("option", Convert.ToBase64String(stream.GetBuffer()));
//        stream.Close();

//        PlayerPrefs.SetString("playerName", userName)
//;    }

//    public void LoadGame()
//    {
//        string data = PlayerPrefs.GetString("player");
//        BinaryFormatter bf = new BinaryFormatter();

//        if(!string.IsNullOrEmpty(data))
//        {
//            MemoryStream stream = new MemoryStream(Convert.FromBase64String(data));
//            user = (PlayerData)bf.Deserialize(stream);
//            stream.Close();
//        } else
//        {
//            // 새로 기본 스탯을 준디. 
//            SetNewData();
//        }
//    }
}
