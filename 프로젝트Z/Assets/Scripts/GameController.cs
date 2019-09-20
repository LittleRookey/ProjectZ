using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private HealthController healthControl;

    private Enemy enemy;
    // if 0, find enemy and give healthbar
    // if 1, don't give healthbar
    private int enemyHealthSetButton = 0;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        // 적을 찾아서 healthbar 장착시켜준다. 
        if(EnemyActiveOnHierarchy() && enemyHealthSetButton == 0) 
        {
            healthControl.LocateHealthBar(enemy);
            enemyHealthSetButton = 1;
        }

        // 적이 죽으면 healthbar회수 및 적을다시찾는다?
        //if(enemyControl.isDead())
        //{
        //    // DO something
        //}
    }

    // return true if enemy is active on heirarchy
    // return false if enemy is not active
    public bool EnemyActiveOnHierarchy()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        return enemy != null;
    }

    public void MonsterDefeated()
    {
        if(enemy.isDead())
        {
            // 지금몬스터는 없애고 다음몬스터소환 다음씬으로넘어가면
        }
    }

    public void SpawnEnemy()
    {

    }
}
