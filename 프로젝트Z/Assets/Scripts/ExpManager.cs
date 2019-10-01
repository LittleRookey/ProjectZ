using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{
    [SerializeField]
    private Image playerExp;

    private void Start()
    {
        playerExp.fillAmount = PlayerController.Instance.GetCurrentExp() / PlayerController.Instance.GetMaxExp();
    }

    public void UpdateExp(float prevExp, float currentExp, float maxExp)
    {
        //playerExp.fillAmount = currentExp / maxExp;
        StartCoroutine(AnimateExp(prevExp, currentExp, maxExp));
    }

    public IEnumerator AnimateExp(float prevExp, float nextExp, float maxExp)
    {
        for(int i = 0; i < nextExp - prevExp; ++i)
        {
            //PlayerController.Instance.AddExp(1);
            Debug.Log("Exp udpate");
            prevExp++;
            playerExp.fillAmount = prevExp / maxExp;
            yield return new WaitForSeconds(.1f);
        }
    }
}
