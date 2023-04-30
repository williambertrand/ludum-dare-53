using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack()
    {
        lastAttack = Time.deltaTime;
        // Detect enemies in range of attack
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
}
