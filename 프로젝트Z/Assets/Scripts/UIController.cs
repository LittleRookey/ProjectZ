using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;


    // Start is called before the first frame update
    void Start()
    {
        
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
