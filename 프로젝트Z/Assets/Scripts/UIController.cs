using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static UIController instance;

    public static UIController Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private Text playerLevelText;

    [SerializeField]
    private Text playerName, playerAttack, playerDefense, playerGold, playerExp;

    [SerializeField]
    private GameObject statButton;

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
    public void UpdatePlayerLevel()
    {
        playerLevelText.text = PlayerController.Instance.GetLevel().ToString();
    }

    public void TurnOnOrOffButton()
    {
        if(statButton.gameObject.activeInHierarchy)
        {
            Debug.Log("Turn off button");
            statButton.gameObject.SetActive(false);
            return;
        }
        Debug.Log("Turn on button");
        statButton.gameObject.SetActive(true);
    }

    public void UpdatePlayerInfoStat()
    {
        playerName.text = "이름: " + PlayerController.Instance.getName();
        playerAttack.text = "공격력: " + PlayerController.Instance.getAttack();
        playerDefense.text = "방어력: " + PlayerController.Instance.getDefense();
        playerGold.text = "골드: " + PlayerController.Instance.GetCurrentGold();
        playerExp.text = "경험치: " + PlayerController.Instance.GetCurrentExp() + " / " + PlayerController.Instance.GetMaxExp();
    }


    private void Start()
    {
        UpdatePlayerInfoStat();
        statButton.gameObject.SetActive(false);
    }

    //public void UpdateEnemyHP()
    //{
    //    enemy = GameController.gameControl.enemy;
    //    Text est = enemy.GetComponentInChildren<Text>();
    //    est.text = enemy.getCurrentHP().ToString() + " / " + enemy.getMaxHP().ToString();
    //    //= enemy.getCurrentHP().ToString() + " / " + enemy.getMaxHP().ToString();

    //}
    // Update is called once per frame

}
