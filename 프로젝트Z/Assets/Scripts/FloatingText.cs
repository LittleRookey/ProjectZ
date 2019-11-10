using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    [SerializeField]
    private Text text;
    // Start is called before the first frame update

    private void OnEnable()
    {
        text.color = new Color(0, 1, 0, 1);
        text.fontSize = 110;
    }

    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }

}
