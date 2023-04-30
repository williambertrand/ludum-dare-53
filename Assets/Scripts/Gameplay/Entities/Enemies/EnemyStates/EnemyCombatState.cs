using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extensions;

public class EnemyCombatState : EnemyState
{

    protected Animator animator;
    protected Enemy self;

    public EnemyCombatState(Enemy enemy, EnemyStateHandler stateMachine, Animator animator) : base(enemy, stateMachine)
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

        bool attackIsCoolingDown = Time.time - self.lastAttack < self.stats.attackSpeed;
        if (attackIsCoolingDown) return;
        // Check if within attack range
        float playerDist = self.transform.position.DistanceSquared(self.target.transform.position);
        if (playerDist <= self.stats.attackRange * self.stats.attackRange)
        {
            self.Attack();
            self.stateMachine.ChangeState(self.isAttackingState);
        } else
        {
            // Player out of range, go back to seeking
            self.stateMachine.ChangeState(self.seekingState);
        }
        base.Update();
    }
}
