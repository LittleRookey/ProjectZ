using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    [SerializeField]
    private Text textMessage;

    private Image im;

    private Color color;

    private void Awake()
    {
        im = GetComponent<Image>();
    }

    private void OnEnable()
    {
        im.color = Color.white;
        if (ColorUtility.TryParseHtmlString("#60FF23", out color))
        {
            textMessage.color = color;
        }
    }

    public void SetMessage(string value)
    {
        textMessage.text = value;
    }

    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
