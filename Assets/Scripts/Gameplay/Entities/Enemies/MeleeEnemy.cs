using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    public Collider2D attackCollider;

    public override void Attack()
    {
        lastAttack = Time.deltaTime;
        // Detect enemies in range of attack
        StartCoroutine(AttackAfterPrep());
    }

    //TODO: Delayed attack with movement towards player
    private void HandleAttack()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, stats.attackRange);
        
        // Apply damge to enemies
        foreach (Collider2D c in hitObjects)
        {
            IDamageable damagable = c.GetComponent<IDamageable>();

            if (damagable == null) continue;

            if (damagable.IsAlly(this.EntityType) || damagable.IsDead())
                continue;

            DamageData data = new DamageData(); 
            data.damageDealer = transform;
            data.target = c.transform;
            data.damageDealt = stats.damage;
            damagable.TakeDamage(data);
        }
    }
    
    IEnumerator AttackAfterPrep()
    {
        yield return new WaitForSeconds(1);
        HandleAttack();
    }

}
