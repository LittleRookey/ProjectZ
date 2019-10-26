using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    Vector3 offset = new Vector3(0, 4f, 0);
    private Text text;
    // Start is called before the first frame update
    void Awake()
    {
        //transform.localPosition += offset;
        text = GetComponent<Text>();


    }

    public void SetActiveFalse()
    {
        this.gameObject.SetActive(false);
    }

}
