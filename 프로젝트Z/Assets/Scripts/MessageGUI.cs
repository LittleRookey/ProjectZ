using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageGUI : MonoBehaviour
{
    private static MessageGUI instance;

    public static MessageGUI Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private MessagePool messagePool;

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
    
    public void ShowMessage(string value)
    {
        Transform tM = messagePool.GetFromPool();
        Message mm = tM.GetComponent<Message>();
        mm.SetMessage(value);
    }

}
