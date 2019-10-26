using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    Enemy enemy;
    private void Start()
    {
        enemy = transform.parent.GetComponent<Enemy>();
    }
    public void AttacksPlayer()
    {
        Debug.Log("Attacked player");
        enemy.Attack(PlayerController.Instance);
    }

    public void SetIdle()
    {
        enemy.SetIdle();
    }

    public void SetDamage()
    {
        enemy.SetDamaged();
    }

    public void SetDead()
    {
        enemy.SetDead();
    }

    public void SetActiveFalse()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
