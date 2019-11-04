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

    public void StopSpeed()
    {
        enemy.StopRunning(true);
    }
    
    public void RestartSpeed()
    {
        enemy.StopRunning(false);
    }

    public void UseEffect(eEffectType eff)
    {
        enemy.UseEffect(eff);        
    }

    public void UseEffects(List<eEffectType> effs)
    {
        enemy.UseEffects(effs);
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
