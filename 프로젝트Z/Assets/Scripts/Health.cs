using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    private Image health;
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Image>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        health.fillAmount = enemy.getCurrentHP() / enemy.getMaxHP();
    }
}
