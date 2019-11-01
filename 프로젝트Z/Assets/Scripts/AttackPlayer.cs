using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField]
    Enemy enemy;

    private void OnEnable()
    {
        enemy = transform.parent.GetComponent<Enemy>();
    }
    public void AttacksPlayer()
    {
        Debug.Log("Attacked player");

        enemy.Attack(PlayerController.Instance);
    }

    public void UseEffect(eEffectType eff)
    {
        enemy.UseEffect(eff);        
    }

    public void SetIdle()
    {
        enemy.SetIdle();
    }

    public void SetAttack()
    {
        enemy.SetAttack();
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
