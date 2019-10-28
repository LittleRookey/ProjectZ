using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPAndSpeedManager : MonoBehaviour
{
    [SerializeField]
    private Image healthImage;
    [SerializeField]
    private Text healthText;
    // 나중에 스피드도 add하기

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
        Debug.Log(healthImage);
        Debug.Log(healthText);
        healthImage.fillAmount = 1f;
        healthText.text = "";
    }

    public Text GetText()
    {
        return healthText;
    }

    public Image GetHealthImage()
    {
        return healthImage;
    }

    public void ShowHP(float current, float max)
    {
        healthImage.fillAmount = current / max;
        Debug.Log(current + "/" + max + "\n" + current / max);
        healthText.text = current.ToString();
    }
}
