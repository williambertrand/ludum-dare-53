using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extensions;

public class EnemyAttackState : EnemyState
{

    protected Animator animator;
    protected Enemy self;

    public EnemyAttackState(Enemy enemy, EnemyStateHandler stateMachine, Animator animator) : base(enemy, stateMachine)
    {
        this.animator = animator;
        this.self = enemy;
    }

    public override void Update()
    {
        if (self.target == null)
        {
            Debug.LogError("No target in combat state");
        }

        // Check if canAttack
        Debug.Log("checking can atack: " + self.lastAttack + " / " + self.stats.attackSpeed);
        if (Time.time - self.lastAttack < self.stats.attackSpeed) return;

        Debug.Log("Can atack! ");
        // Check if within attack range
        float playerDist = self.transform.position.DistanceSquared(self.target.transform.position);
        if (playerDist <= self.stats.attackRange * self.stats.attackRange)
        {
            Debug.Log("Attack in range, initiating!");
            self.Attack();
            self.stateMachine.ChangeState(self.isAttackingState);
        }
        base.Update();
    }
}
