using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private HealthController healthControl;

    public List<GameObject> enemies;

    public Transform spawnPosition;

    private Enemy enemy;

    public List<Button> nextSceneButtons;

    public GameObject toggle;

  

    private IEnumerator StartSpawnEnemy()
    {
        
        SpawnEnemy();

        while(!enemy.isDead())
        {
            yield return new WaitForSeconds(1f);
        }
        
    }

    // after one monster is spawned at the very beginning
    private IEnumerator GameStart()
    {
        


    }
    public void SpawnEnemy()
    {
        if(enemy == null)
        {
            Debug.Log("Enemy spawned");
            GameObject clone = Instantiate(enemies[Random.Range(0, enemies.Count)]);
            clone.transform.position = Vector3.zero;
            enemy = clone.GetComponent<Enemy>();
            healthControl.LocateHealthBar(enemy);
        }
        else
        {

        }
    }




    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {



    }
}
