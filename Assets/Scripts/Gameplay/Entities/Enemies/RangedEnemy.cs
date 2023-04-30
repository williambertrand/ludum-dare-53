using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Ranged attack info")]
    public GameObject projectile;
    public float shootVel;
    public float attackDamage;
    
    public override void Attack()
    {
        lastAttack = Time.deltaTime;
        FireProjectileAtTarget();
    }
    
    private void FireProjectileAtTarget()
    {
        if (target == null) return;
        // TODO: Animation
        //animator.SetTrigger("attack");

        Vector3 targetPos = target.transform.position;
        Vector3 dir = ( targetPos- transform.position).normalized;

        GameObject proj = Instantiate(projectile, attackPoint.position, Quaternion.identity);
        proj.transform.position = attackPoint.position;
        proj.GetComponent<Rigidbody2D>().velocity = dir * shootVel;
        proj.GetComponent<Projectile>().damage = attackDamage;
        lastAttack = Time.time;
    }
}
