﻿using System.Collections;
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
    private PlayerData playerData; 


    [SerializeField]
    private HealthController healthControl;

    [SerializeField]
    private EnemyPool enemyPool;

    // enemies that can be spawned
    public List<Enemy> enemies;

    //[SerializeField]
    //public List<Enemy> currentEnemy;

    private Enemy prevEnemy;

    public bool enemyIsDead;

    public List<Button> nextSceneButtons;

    public GameObject toggle;

    public bool toggleOn;

    private Coroutine wait_Run;

    [SerializeField]
    private int enemySpawnedNumber, enemyDeadNum;

    //public int stage;

    //public int enemySpawnedThisRound;

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

        playerData.game_currentEnemy = new List<Enemy>();
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
            SpawnEnemy(playerData.game_enemySpawnedThisRound);
            LocateEnemies(playerData.game_currentEnemy);

            
        }
    }

    public void InitGame()
    {
        TurnToggle(false);
        toggleOn = false;
        TouchManager.Instance.gameObject.SetActive(true);
        

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
            SpawnEnemy(playerData.game_enemySpawnedThisRound);
            LocateEnemies(playerData.game_currentEnemy);
        }
       

    }

    // spawn enemy by given number
    public void SpawnEnemy(int num)
    {
        int playerLevel = playerData.player_level;
        for (int i = 0; i < num; ++i)
        {
            Enemy clone = Instantiate(enemies[Random.Range(0, enemies.Count)]);
            //clone.Init((int)((playerLevel + .5 * gameStage) * 100), 
            //    (int)((playerLevel + .5 * gameStage) * 10),
            //    (int)((playerLevel + .5 * gameStage) * 5),
            //    (int)((playerLevel + .5 * gameStage) * ((playerLevel + .5 * gameStage) * 10 + (playerLevel + .5 * gameStage) * 5)/2),
            //    (int)(1 * (playerLevel + (gameStage * 1.5)))
            //    );
            playerData.game_currentEnemy.Add(clone);
            enemySpawnedNumber++;
        }
       
    }

    // spawn enemy if enemiesleft is not empty
    public void SpawnEnemy(List<Enemy> enemiesLeft)
    {
        

        // if enemy exist previous game
        if(enemiesLeft.Count > 0)
        {
            for (int i = 0; i < enemiesLeft.Count; ++i)
            {
                Enemy temp = Instantiate(enemiesLeft[i]);

                playerData.game_currentEnemy.Add(temp);
            }
        } else
        {
            // spawn enemy based on stage?
            SpawnEnemy(playerData.game_stage % 3 + 1);
        }
    }

    // locates enemy to random pos and also locate healthbar to each enemy
    public void LocateEnemies(List<Enemy> enems)
    {
        SortEnemy(enems);
        for (int k = 0; k < enems.Count; ++k)
        {
            enems[k].transform.position = enemySpawnPos[k].position;
        }
        healthControl.LocateHealthBar(enems);
    }

    // sorts enemy based on their forwardNumber
    public void SortEnemy(List<Enemy> lists)
    {
        Enemy temp;

        for (int i = 0; i < lists.Count; i++)
        {
            for (int j = i + 1; j < lists.Count; j++)
            {
                if (lists[i].getForwardNumber() > lists[j].getForwardNumber())
                {
                    temp = lists[i];
                    lists[i] = lists[j];
                    lists[j] = temp;
                }
            }
        }
    }

    // counts no enemy as all dead 
    // checks if all spawned enemies are dead
    public bool AllEnemiesDead()
    {
        bool allDead = true;

        if(playerData.game_currentEnemy.Count == 0)
        {
            Debug.Log("no Enemy");
            return true;
        }

        foreach (Enemy enemy in playerData.game_currentEnemy)
        {
            allDead = allDead && !enemy.isAlive;
        }
        
        return allDead;
    }

    // Empties Current enemies list and health bar list
    public void EmptyEnemiesAndHealth()
    {
        playerData.game_currentEnemy.Clear();
        healthControl.currentHealths.Clear();
    }



    public void TurnToggle(bool set)
    {
        toggle.gameObject.SetActive(set);
    }

    public void SetToggleTrue()
    {
        toggleOn = true;
    }

    public int GetStage()
    {
        return playerData.game_stage;
    }

    public void SaveGame()
    {
        //PlayerController.Instance.SavePlayerData();
        //SaveProgress();
        SaveClass.Instance.SaveGame();
    }

    //public void SaveProgress()
    //{
    //    playerData.game_stage = stage;
    //    playerData.game_currentEnemy = currentEnemy;
    //    playerData.game_enemySpawnedThisRound = enemySpawnedThisRound;
    //}

    public void LoadGame()
    {
        SaveClass.Instance.LoadGame();
        //LoadProgress();
        //PlayerController.Instance.LoadPlayerData();
        //Debug.Log("PlayerLevellll: " + playerData.player_level);
    }

    //public void LoadProgress()
    //{
    //    stage = playerData.game_stage;
    //    currentEnemy = playerData.game_currentEnemy;
    //    enemySpawnedThisRound = playerData.game_enemySpawnedThisRound;
    //}

    public PlayerData GetPlayerData()
    {
        return playerData;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadGame();
        enemySpawnedNumber = 0;
        toggleOn = true;
        SpawnEnemy(0f);
    }


    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            SaveGame();
        } else
        {
            
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    // Update is called once per frame
    void Update()
    {

        

    }
}
