using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTransparent : MonoBehaviour
{
    [SerializeField]
    private Image toggle;

    

    private void OnEnable()
    {
        Debug.Log("Enable button");
        toggle = GetComponent<Image>();
        StartCoroutine(MakeTransparent());
    }

    private IEnumerator MakeTransparent()
    {
        Color col = toggle.color;
        while(true)
        {
            DecreaseA(col, 120);
            yield return new WaitForSeconds(.5f);
            increaseA(col, 255);
            yield return new WaitForSeconds(.5f);
        }
    }

    private void DecreaseA(Color col, int a)
    {
        Debug.Log("decrease");
        for (int i = 0; i < 255; ++i)
        {
            if (col.a > a)
            {
                col.a--;
            }
        }
    }

    private void increaseA(Color col, int a)
    {
        Debug.Log("increase");
        //while (toggle.color.a < a)
        //{
        //    col.a++;
        //}
        for (int i = 0; i < 255; ++i)
        {
            if (col.a < a)
            {
                col.a++;
            }
        }
    }

}
