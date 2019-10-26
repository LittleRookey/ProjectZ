using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private static HealthController instance;

    public static HealthController Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    public MonsterHPBarPool monsterHPBarPool;

    [SerializeField]
    public Image PlayerHPBar;

    public List<Health> currentHealths;

    public List<Text> HPTexts;

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

    public void LocateHealthBar(List<Enemy> enemy)
    {
        Debug.Log("HP Located");
        for(int i = 0; i < enemy.Count; ++i)
        {
            // TODO critical image need to be used
            Health enemHealthBar = monsterHPBarPool.GetFromPool();
            enemHealthBar.transform.position = enemy[i].transform.position + Vector3.up * 1f;
            enemHealthBar.transform.localScale = Vector3.one;

            //health connect with enemy
            currentHealths.Add(enemHealthBar.GetComponentInChildren<Health>());
            HPTexts.Add(enemHealthBar.GetComponentInChildren<Text>());

            enemy[i].health = currentHealths[i];
            Debug.Log(enemy.Count);
            enemy[i].healthText = HPTexts[i];

            //enemy[i].healthText.text = enemy[i].getCurrentHP().ToString() + " / " 
            //    + enemy[i].getMaxHP().ToString();
        }
    }

    //public void LocateBossHealthBar(List<Enemy> enemy)
    //{
    //    Debug.Log("HP Located");
    //    for (int i = 0; i < enemy.Count; ++i)
    //    {
    //        Image enemHealthBar = Instantiate(healthBarEnemy, canvas.transform);
    //        enemHealthBar.transform.position = enemy[i].transform.position + Vector3.up * 1f;
    //        enemHealthBar.transform.localScale = Vector3.one;

    //        //health connect with enemy
    //        currentHealths.Add(enemHealthBar.GetComponentInChildren<Health>());
    //        HPTexts.Add(enemHealthBar.GetComponentInChildren<Text>());

    //        enemy[i].health = currentHealths[i];
    //        Debug.Log(enemy.Count);
    //        enemy[i].healthText = HPTexts[i];

    //        //enemy[i].healthText.text = enemy[i].getCurrentHP().ToString() + " / " 
    //        //    + enemy[i].getMaxHP().ToString();
    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        
    }
}
