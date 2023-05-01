using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Ranged attack info")]
    public GameObject projectile;
    public float shootVel;

    public override void Attack()
    {
        lastAttack = Time.time;
        // Account for animation frames before firign bullet
        StartCoroutine(FireProjectileAtTarget());
    }
    
    IEnumerator FireProjectileAtTarget()
    {
        yield return new WaitForSeconds(0.85f);
        if (target != null)
        {

            Vector3 accuracyOffset = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f));
            
            Vector3 targetPos = target.transform.position + accuracyOffset;
            Vector3 dir = ( targetPos - transform.position).normalized;

            GameObject proj = Instantiate(projectile, attackPoint.position, Quaternion.identity);
            proj.transform.position = attackPoint.position;
            proj.GetComponent<Rigidbody2D>().velocity = dir * shootVel;
            proj.GetComponent<Projectile>().damage = stats.damage;
            proj.GetComponent<Projectile>().owner = this;
        }
    }
}
