using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingCritText : MonoBehaviour
{
    [SerializeField]
    private Text text;
    // Start is called before the first frame update
    [SerializeField]
    private Color color;

    private void OnEnable()
    {
        text.color = color;

        text.fontSize = 140;
    }

    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
