using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private static GameController instance;

    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private HealthController healthControl;

    [SerializeField]
    private EnemyPool enemyPool;

    // enemies that can be spawned
    public List<GameObject> enemies;

    [SerializeField]
    public List<Enemy> currentEnemy;

    private Enemy prevEnemy;

    public bool enemyIsDead;

    public List<Button> nextSceneButtons;

    public GameObject toggle;

    public bool toggleOn;

    private Coroutine wait_Run;

    [SerializeField]
    private int enemySpawnedNumber, enemyDeadNum;


    public int enemySpawnedThisRound;

    [SerializeField]
    private List<Transform> enemySpawnPos;

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

        currentEnemy = new List<Enemy>();
    }

    private IEnumerator SelectInSeconds(float seconds)
    {
        while (seconds > 0)
        {
            if (!toggleOn)
            {
                yield return new WaitForSeconds(1f);
                seconds--;
                Debug.Log("Seconds After");

            } else
            {
                Debug.Log("Coroutine Stopped");
                break;
                // 코루틴 끊고 하는법? 계속유니티 터짐
                //StopCoroutine(wait_Run);
            }
            
        }
        
        Debug.Log("1");
        // if toggle is yet false, make it true
        if (!toggleOn)
            toggleOn = !toggleOn;

        if (toggleOn)
        {
            Debug.Log("Enemy spawned");

            // touch set active, toggle set inactive
            InitGame();

            // Spawn enemy by the given number

            SpawnEnemy(enemySpawnedThisRound);

            LocateEnemies(currentEnemy);

            
        }
    }

    public void InitGame()
    {
        TurnToggle(false);
        toggleOn = false;

        TouchManager.touchManager.gameObject.SetActive(true);
    }

    // after one monster is spawned at the very beginning
    // turn toggle off, touch off, spawn random enemy, spawn healthbar
    public void SpawnEnemy(float seconds)
    {
        if (enemySpawnedNumber > 0)
        {    
            wait_Run = StartCoroutine(SelectInSeconds(seconds));   
        } else
        {
            Debug.Log("First Spawn");

            // touch set active, toggle set inactive
            InitGame();

            // Spawn enemy by the given number
            SpawnEnemy(enemySpawnedThisRound);
            LocateEnemies(currentEnemy);
        }
       

    }

    // spawn enemy by given number
    public void SpawnEnemy(int num)
    {
        for (int i = 0; i < num; ++i)
        {
            GameObject clone = Instantiate(enemies[Random.Range(0, enemies.Count)]);
            currentEnemy.Add(clone.GetComponent<Enemy>());
            enemySpawnedNumber++;
        }
    }

    // locates enemy to random pos and also locate healthbar to each enemy
    public void LocateEnemies(List<Enemy> enems)
    {
        List<Transform> temp = new List<Transform>();
        for(int i = 0; i < enemySpawnPos.Count; ++i)
        {
            temp.Add(enemySpawnPos[i]);
        }

        for(int k = 0; k < enems.Count; ++k)
        {
            int randNum = Random.Range(0, temp.Count);
            enems[k].transform.position = temp[randNum].position;
            healthControl.LocateHealthBar(currentEnemy);
            temp.RemoveAt(randNum);
        }
    }

    // counts no enemy as all dead 
    // checks if all spawned enemies are dead
    public bool AllEnemiesDead()
    {
        bool allDead = true;

        if(currentEnemy.Count == 0)
        {
            return true;
        }

        foreach(Enemy enemy in currentEnemy)
        {
            allDead = allDead && !enemy.isAlive;
        }
        return allDead;
    }

    // Empties Current enemies list and health bar list
    public void EmptyEnemiesAndHealth()
    {
        currentEnemy.Clear();
        healthControl.currentHealths.Clear();
    }

    public void SetNextEnemyToFrontLine()
    {
        
    }

    public void TurnToggle(bool set)
    {
        toggle.gameObject.SetActive(set);
    }

    public void SetToggleTrue()
    {
        toggleOn = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnedNumber = 0;
        toggleOn = true;
        SpawnEnemy(0f);
    }


    // Update is called once per frame
    void Update()
    {

        

    }
}
