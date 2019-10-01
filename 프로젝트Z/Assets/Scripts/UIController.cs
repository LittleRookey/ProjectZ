using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private Text playerLevelText;


    public void UpdatePlayerLevel()
    {
        playerLevelText.text = PlayerController.Instance.GetLevel().ToString();
    }
    //public void UpdateEnemyHP()
    //{
    //    enemy = GameController.gameControl.enemy;
    //    Text est = enemy.GetComponentInChildren<Text>();
    //    est.text = enemy.getCurrentHP().ToString() + " / " + enemy.getMaxHP().ToString();
    //    //= enemy.getCurrentHP().ToString() + " / " + enemy.getMaxHP().ToString();

    //}
    // Update is called once per frame
    void Update()
    {
     
    }
}
