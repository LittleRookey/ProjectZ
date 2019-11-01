using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    [SerializeField]
    private SpriteRenderer skillIcon;

    [SerializeField]
    private int skillDurationTime;

    [SerializeField]
    private int skillCooltime;

    [SerializeField]
    private bool canUse = true;

    public void DefenseAllDamageFor()
    {
        //StartCoroutine(DefenseForFewSecs(skillDurationTime));   
    }
    
    private IEnumerator DefenseForFewSecs(int secs)
    {
        // TODO make skill Effect 어떻게?


        PlayerController.Instance.BlockDamage();
        canUse = false;
        // use animation skill cooldown
        yield return new WaitForSeconds(secs);

        // stop block damage + skill cooldown;
        PlayerController.Instance.StopBlock();

        //StartCoroutine(Timer(skillCooltime));
    }

    private IEnumerator Timer(int cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canUse = true;

    }

}
