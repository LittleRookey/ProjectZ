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
    private PlayerData playerData;

    [SerializeField]
    private HealthController healthControl;

    [SerializeField]
    private ExpManager expManager;

    [SerializeField]
    private EnemyPool enemyPool;

    [SerializeField]
    private BossPool bossPool;

    // enemies that can be spawned
    public List<Enemy> enemies;

    public List<Enemy> bosses;
    //[SerializeField]
    //public List<Enemy> currentEnemy;

    private Enemy frontEnemy;

    [SerializeField]
    private List<Enemy> currentEnemies;

    public bool enemyIsDead;

    public List<Button> nextSceneButtons;

    public GameObject toggle;

    public bool toggleOn;

    private Coroutine wait_Run;

    [SerializeField]
    private int enemySpawnedNumber, enemyDeadNum;

    [SerializeField]
    private int bossSpawnPeriod = 10;

    //public int stage;

    //public int enemySpawnedThisRound;

    [SerializeField]
    private List<Transform> enemySpawnPos;

    [SerializeField]
    private List<Transform> bossSpawnPos;

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
            int stage = playerData.game_stage;
            playerData.game_enemySpawnedThisRound = playerData.game_stage % 3 + 1;
            // Spawn enemy by the given number
            /*SpawnEnemies(playerData.game_enemySpawnedThisRound)*/
            if (stage % bossSpawnPeriod != 0)
            {
                SpawnEnemies(playerData.game_enemySpawnedThisRound);
            }
            else
            {
                SpawnBoss();
            }
        }
    }

    public void InitGame()
    {
        TurnToggle(false);
        toggleOn = false;
        TouchManager.Instance.gameObject.SetActive(true);
        

    }

    public void GoToNextStage()
    {
        //TODO show gamestage effect or animation
        playerData.game_stage++;
        TouchManager.Instance.gameObject.SetActive(false);
        TurnToggle(true);
        EmptyEnemiesAndHealth();

        SpawnEnemy(5f);
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
            SpawnEnemies(playerData.game_enemySpawnedThisRound);
        }
    }

    public void SpawnEnemies(int num)
    {
        int playerLevel = playerData.player_level;
        for (int i = 0; i < num; ++i)
        {
            int randNum = Random.Range(0, enemies.Count);
            Enemy clone = enemyPool.GetFromPool(randNum);
            //clone.Init((int)((playerLevel + .5 * gameStage) * 100), 
            //    (int)((playerLevel + .5 * gameStage) * 10),
            //    (int)((playerLevel + .5 * gameStage) * 5),
            //    (int)((playerLevel + .5 * gameStage) * ((playerLevel + .5 * gameStage) * 10 + (playerLevel + .5 * gameStage) * 5)/2),
            //    (int)(1 * (playerLevel + (gameStage * 1.5)))
            //    );

            currentEnemies.Add(clone);
            //AddEnemy(clone);
            enemySpawnedNumber++;
        }

        LocateEnemies(currentEnemies);
    }

    public void SpawnBoss(int num = 1)
    {
        int playerLevel = playerData.player_level;

        for (int i = 0; i < num; i++)
        {
            int randNum = Random.Range(0, bosses.Count);
            Enemy bossClone = bossPool.GetFromPool(randNum);
            currentEnemies.Add(bossClone);
        }
        LocateBosses(currentEnemies);
    }

    // spawn enemy by given number
    public void SpawnEnemy(int num)
    {
        int playerLevel = playerData.player_level;
        for (int i = 0; i < num; ++i)
        {
            int randNum = Random.Range(0, enemies.Count);
            Enemy clone = enemyPool.GetFromPool(randNum);
            Debug.Log(randNum + " randNum");
            //clone.Init((int)((playerLevel + .5 * gameStage) * 100), 
            //    (int)((playerLevel + .5 * gameStage) * 10),
            //    (int)((playerLevel + .5 * gameStage) * 5),
            //    (int)((playerLevel + .5 * gameStage) * ((playerLevel + .5 * gameStage) * 10 + (playerLevel + .5 * gameStage) * 5)/2),
            //    (int)(1 * (playerLevel + (gameStage * 1.5)))
            //    );
            Debug.Log("11");
            //AddEnemy(clone);
            
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
                Debug.Log("22");
                //AddEnemy(temp);
            }
        } else
        {
            // spawn enemy based on stage?
            SpawnEnemy(playerData.game_stage % 3 + 1);
        }
    }

    // locates enemy to random pos and also locate healthbar to each enemy
    public void LocateEnemies(List<int> enemIDs)
    {
        List<Enemy> temps = GetEnemiesByIDs(enemIDs);

        SortEnemy(temps);
        for (int k = 0; k < temps.Count; ++k)
        {
            temps[k].transform.position = enemySpawnPos[k].position;
        }

        healthControl.LocateHealthBar(temps);
    }

    public void LocateEnemies(List<Enemy> enemss)
    {
        SortEnemy(enemss);
        for (int k = 0; k < enemss.Count; ++k)
        {
            enemss[k].transform.position = enemySpawnPos[k].position;
        }

        healthControl.LocateHealthBar(enemss);
    }

    public void LocateBosses(List<Enemy> bosse)
    {
        SortEnemy(bosse);
        for (int k = 0; k < bosse.Count; ++k)
        {
            bosse[k].transform.position = bossSpawnPos[k].position;
        }
        healthControl.Locatebosshealthbar(bosse);
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

        if(playerData.game_enemySpawnedThisRound == 0)
        {
            Debug.Log("no Enemy");
            return true;
        }

        for(int i = 0; i < currentEnemies.Count; ++i) 
        {
            allDead = allDead && !currentEnemies[i].isAlive;
        }

        
        return allDead;
    }

    // Empties Current enemies list and health bar list
    public void EmptyEnemiesAndHealth()
    {
        currentEnemies.Clear();
        healthControl.currentHealths.Clear();
        healthControl.HPTexts.Clear();
    }
    
    // Calls and clones enemies by given ids
    public List<Enemy> GetEnemiesByIDs(List<int> ids)
    {
        List<Enemy> listOfEnems = new List<Enemy>();

        for(int i = 0; i < enemies.Count; ++i)
        {
            for(int k = 0; k < ids.Count; ++k)
            {
                if(enemies[i].getID() == ids[k])
                {
                     listOfEnems.Add(enemies[i]);
                }
            }
        }
        return listOfEnems;
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

    public Enemy GetFrontEnemy()
    {

        return currentEnemies[0];
    }

    public List<Enemy> GetAllEnemies()
    {
        return enemies;
    }

    public void SaveGame()
    {
        Debug.Log("Saved");
        SaveClass.Instance.SaveGame(playerData);
    }

    public void LoadGame()
    {
        playerData = SaveClass.Instance.LoadGame();
    }

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    public int GetGameStage()
    {
        return playerData.game_stage;
    }

    public void DeleteFirstEnemy()
    {
        currentEnemies.RemoveAt(0);
    }

    
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        LoadGame();
        PlayerController.Instance.SetPlayerData(playerData);
        enemySpawnedNumber = 0;
        toggleOn = true;
        SpawnEnemy(0f);
        UpdateGameWithLoadedData();
    }

    public void UpdateGameWithLoadedData()
    {
        UIController.Instance.UpdatePlayerLevel();
        UIController.Instance.UpdatePlayerInfoStat();
        expManager.UpdateExp(PlayerController.Instance.GetCurrentExp(), PlayerController.Instance.GetMaxExp());
        //TODO update player hp

    }
    //public void ResetData()
    //{
    //    Debug.Log("SetnewData");
    //    playerData = new PlayerData();
    //    playerData.player_name = "player" + UnityEngine.Random.Range(0, 99999).ToString();
    //    playerData.player_currentHP = 100;
    //    playerData.player_maxHP = 100;
    //    playerData.player_attack = 10;
    //    playerData.player_defense = 5;
    //    playerData.player_gold = 10;
    //    playerData.player_level = 1;
    //    playerData.player_maxExp = 10;
    //    playerData.player_currentExp = 0;
    //    //playerData.player_Inventory = new List<Item>();


    //    playerData.game_stage = 1;
    //    playerData.game_currentEnemy = new EnemySaveData();
    //    playerData.game_currentEnemy.currentHP = new List<float>();
    //    playerData.game_currentEnemy.maxHP = new List<float>();
    //    playerData.game_currentEnemy.id = new List<int>();
    //    playerData.game_currentEnemy.isAlive = new List<bool>();
    //    playerData.game_enemySpawnedThisRound = playerData.game_stage % 3 + 1;
    //}

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
