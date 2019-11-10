using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;


//public enum eItemType
//{
//    Consume,
//    Non_consume
//}

//[System.Serializable]
//public class Item
//{
//    public string Name;
//    public Sprite Icon;
//    public int ID;
//    public string Contents;
//    public double Value;
//    public eItemType ConsumeType;


//}

public class JSonGenerator : MonoBehaviour
{
    // resources 폴더 만들어야함
    [SerializeField]
    private Item[] ItemArr;

    [SerializeField]
    private Sprite[] ItemIcons;

    [SerializeField]
    private ShopController shop;

    //Start is called before the first frame update
    void Start()
    {
        //Init();
        //string data = JsonConvert.SerializeObject(ItemArr, Formatting.Indented);
        //Debug.Log(data);

        //StreamWriter writer = new StreamWriter(Application.dataPath + "/item.json");
        //writer.Write(data);
        //writer.Close();

        //파일 불러오기
        string data = Resources.Load<TextAsset>("JsonFiles/item").text;
        ItemArr = JsonConvert.DeserializeObject<Item[]>(data);
        SetIcons(ItemArr);
        shop.SetOriginItems(ItemArr);
        shop.RenewShop();
    }

    private void Save()
    {
        Init();
        string data = JsonConvert.SerializeObject(ItemArr, Formatting.Indented);
        Debug.Log(data);

        StreamWriter writer = new StreamWriter(Application.dataPath + "/item.json");
        writer.Write(data);
        writer.Close();
    }

    private void Load()
    {
        string data = Resources.Load<TextAsset>("JsonFiles/item").text;
        ItemArr = JsonConvert.DeserializeObject<Item[]>(data);
    }

    private void SetIcons(Item[] itemss)
    {
        for(int i = 0; i < itemss.Length; i++)
        {
            itemss[i].icon = ItemIcons[i];
        }
    }
        
    private void Init()
    {
        ItemArr = new Item[2];
        ItemArr[0] = new Item();

        ItemArr[0].Name = "EXP포션(하)";
        ItemArr[0].ID = 11000;
        ItemArr[0].Contents = "경험치를 10 회복합니다.";
        ItemArr[0].Value = 10;
        ItemArr[0].itemType = eItemType.potion_ExpLow;

        ItemArr[1] = new Item();
        ItemArr[1].Name = "HP포션(하)";
        ItemArr[1].ID = 11001;
        ItemArr[1].Contents = "체력을 15% 회복합니다.";
        ItemArr[1].Value = .15f;
        ItemArr[1].itemType = eItemType.potion_HpLow;
    }

}
