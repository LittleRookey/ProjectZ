using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    public Image healthBarEnemy;

    [SerializeField]
    public Image PlayerHPBar;

    public List<Health> currentHealths;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void LocateHealthBar(List<Enemy> enemy)
    {
        for(int i = 0; i < enemy.Count; ++i)
        {
            Image enemHealthBar = Instantiate(healthBarEnemy, canvas.transform);
            enemHealthBar.transform.position = enemy[i].transform.position + Vector3.up * 2f;
            enemHealthBar.transform.localScale = Vector3.one;

            //health connect with enemy
            currentHealths.Add(enemHealthBar.GetComponentInChildren<Health>());
            enemy[i].health = currentHealths[i];
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
