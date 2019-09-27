using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    private Image health;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Image>();
        healthText = gameObject.transform.parent.GetComponentInChildren<Text>();
    }

    public void ShowHPAnimation(int current, int max, int hpLoss)
    {
        StartCoroutine(AnimateHPLoss(current, max, hpLoss));
    }

    private IEnumerator AnimateHPLoss(int current, int max, int hpLoss)
    {
        while(true)
        {
            if (current - hpLoss <= 0)
            {
                int leftNum = hpLoss - current;
                for (int i = 0; i < hpLoss - leftNum; ++i)
                {
                    current--;
                    health.fillAmount = ((float)current / (float)max);
                    healthText.text = current.ToString() + " / " + max.ToString();
                    yield return new WaitForSeconds(.05f);
                }
            }
            else
            {
                for (int i = 0; i < hpLoss; ++i)
                {
                    current--;
                    health.fillAmount = ((float)current / (float)max);
                    healthText.text = current.ToString() + " / " + max.ToString();
                    yield return new WaitForSeconds(.05f);
                }
            }
            break;
        }
    }
    public void ShowHP(int current, int max)
    {
        health.fillAmount = ((float)current / (float)max);
        Debug.Log(current + "/" + max +"\n" + current/max);
        healthText.text = current.ToString() + " / " + max.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        

    }
}
