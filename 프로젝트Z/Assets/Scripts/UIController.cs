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
    private UnitSetter unitSetter;

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private Text stageText;

    [Header("Stat")]
    [SerializeField]
    private GameObject statButton;

    [SerializeField]
    private Text playerLevelText;

    [SerializeField]
    private Text playerName, playerAttack, playerDefense, playerGold, playerExp, playerCritRate, playerCritDamage;

    [SerializeField]
    private Text goldInterfaceText;

    [SerializeField]
    private Transform closedPos, openedPos;

    [SerializeField]
    private float mSpeed;

    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private Text shopResetCostText;

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

    public void SetPlayerData(PlayerData pData)
    {
        playerData = pData;
    }

    public void UpdatePlayerLevel()
    {
        playerLevelText.text = PlayerController.Instance.GetLevel().ToString();
    }

    public void TurnOnOrOffButton()
    {
        StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {
        statButton.transform.SetAsLastSibling();
        WaitForFixedUpdate fx = new WaitForFixedUpdate();
        if (statButton.transform.position.x > 0)
        {
            // closes the stat button

            while (statButton.transform.position.x > closedPos.position.x)
            {
                statButton.transform.Translate(Vector3.left * mSpeed * Time.deltaTime);
                yield return fx;
            }
        } else {
            // opens the stat button

            while (statButton.transform.position.x < openedPos.position.x)
            {
                statButton.transform.Translate(Vector3.right * mSpeed * Time.deltaTime);
                yield return fx;
            }
        }
    }

    public void UpdateStage(int stage)
    {
        stageText.text = "Stage " + stage.ToString();
    }
    public void UpdatePlayerInfoStat()
    {
        playerName.text = "이름: " + PlayerController.Instance.getName();
        playerAttack.text = "공격력: " + PlayerController.Instance.getAttack();
        playerDefense.text = "방어력: " + PlayerController.Instance.getDefense();
        playerGold.text = "골드: " + unitSetter.GetUnitStr(playerData.player_gold);
        playerExp.text = "경험치: " + PlayerController.Instance.GetCurrentExp() + " / " + PlayerController.Instance.GetMaxExp();
        playerCritRate.text = "크리티컬 확률: " + playerData.player_critRate.ToString() + "%";
        playerCritDamage.text = "크리티컬 데미지: " + (playerData.player_critDamage * 100).ToString() + "%";
        goldInterfaceText.text = unitSetter.GetUnitStr(playerData.player_gold);

    }

    public void UpdatePlayerGold()
    {
        playerGold.text = "골드: " + unitSetter.GetUnitStr(playerData.player_gold);
        goldInterfaceText.text = unitSetter.GetUnitStr(playerData.player_gold);
    }

    public void UpdateResetCost(double cost)
    {
        shopResetCostText.text = cost.ToString() + "G";
    }

    private void Start()
    {
        UpdatePlayerInfoStat();
    }

    public void GainGold()
    {
        playerData.player_gold += 150;
        ShopController.Instance.CheckEnoughMoney();
        UpdatePlayerInfoStat();
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
