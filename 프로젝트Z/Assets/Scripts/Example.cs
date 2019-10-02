using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public class SaveData
{
    public int id;
    public string name;
    public double gold;
}

public class Example : MonoBehaviour
{

    [SerializeField]
    private SaveData save;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Save()
    {
        BinaryFormatter bf; //등등 따라하기
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
