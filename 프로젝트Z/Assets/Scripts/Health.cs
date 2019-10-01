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

    //public void ShowHPAnimation(Enemy enem, float hpLoss)
    //{
    //    StartCoroutine(AnimateHPLoss(enem, hpLoss));
    //}

    //public IEnumerator AnimateHPLoss(Enemy enem, float hpLoss)
    //{
    //    while (true)
    //    {


    //        if (current - hpLoss <= 0)
    //        {
    //            float leftNum = hpLoss - current;
    //            // when health goes below 0
    //            for (int i = 0; i < hpLoss - leftNum; ++i)
    //            {
    //                enem.LoseOneHP();
    //                health.fillAmount = current / max;
    //                healthText.text = current.ToString() + " / " + max.ToString();
    //                yield return new WaitForSeconds(.01f);
    //            }
    //            transform.parent.gameObject.SetActive(false);
    //            Debug.Log("left health1" + " " + current);
    //        }
    //        else
    //        {
    //            // when health is above 0
    //            for (int i = 0; i < hpLoss; ++i)
    //            {
    //                float current = enem.getCurrentHP();
    //                float max = enem.getMaxHP();
    //                enem.LoseOneHP();
    //                health.fillAmount = current / max;
    //                healthText.text = current.ToString() + " / " + max.ToString();
    //                Debug.Log(current.ToString() + " / " + max.ToString());
    //                yield return new WaitForSeconds(.01f);
    //            }
    //            Debug.Log("left health2" + " " + current + " " + max);

    //        }
    //        break;
    //    }
    //}

    // HPLoss = damage character get
    //private IEnumerator AnimateHPLoss(Enemy temp, float current, float max, float hpLoss)
    //{

    //    while(true)
    //    {
    //        if (current - hpLoss <= 0)
    //        {
    //            float leftNum = hpLoss - current;

    //            for (int i = 0; i < hpLoss; ++i)
    //            {
    //                current--;
    //                health.fillAmount = current / max;
    //                healthText.text = current.ToString() + " / " + max.ToString();
    //                yield return new WaitForSeconds(.05f);
    //            }



    //            transform.parent.gameObject.SetActive(false);


    //        }
    //        else
    //        {
    //            float prevHP = current;

    //            for (int i = 0; i < hpLoss; ++i)
    //            {
    //                current--;
    //                health.fillAmount = current / max;

    //                healthText.text = current.ToString() + " / " + max.ToString();
    //                yield return new WaitForSeconds(.05f);
    //            }



    //        }
    //        break;
    //    }
    //}

    public void ShowHP(float current, float max)
    {
        health.fillAmount = current / max;
        Debug.Log(current + "/" + max +"\n" + current/max);
        healthText.text = current.ToString() + " / " + max.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
