using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSetter : MonoBehaviour
{
    //[SerializeField]
    //private Text mUnitText;

    //[SerializeField]
    //private double mNumber;

    //[Header("TestData")]
    //[SerializeField]
    //private double mSum = 1;
    //[SerializeField]
    //private double mSumWeight;

    //public void EnforceSum()
    //{
    //    mSum = mSum * mSumWeight;
    //}

    static readonly string[] unit = { "", "K", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar","as","at","au","av","aw","ax","ay","az",
                                                              "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br","bs","bt","bu","bv","bw","bx","by","bz",};
    // Update is called once per frame
    //void Update()
    //{
    //    mNumber += mSum;

    //    string n = mNumber.ToString("N0");
    //    string[] splited = n.Split(',');

    //    // 3 자리소수점
    //    if (splited.Length >= 2)
    //    {
    //        mUnitText.text = string.Format("{0}.{1} {2}", splited[0], splited[1], unit[splited.Length - 2]);
    //    }
    //    else
    //    {
    //        mUnitText.text = splited[0];
    //    }
    //}

    public string GetUnitStr(double number)
    {
        string n = number.ToString("N0");
        string[] splited = n.Split(',');
        // 2자리소수점
        if (splited.Length >= 2)
        {
            int unitID = splited.Length - 1;
            char[] min = splited[1].ToCharArray();
            return string.Format("{0}.{1}{2} {3}", splited[0], min[0], min[1], unit[unitID]);
        }
        else
        {
            return splited[0];
        }
    }
}
