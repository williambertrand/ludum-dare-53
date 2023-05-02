using System;
using System.Collections;
using System.Collections.Generic;
using extensions;
using UnityEngine;

public class EnemySeekingState : EnemyState
{
    protected Animator animator;
    protected Enemy self;

    private float randOffsetY;

    public EnemySeekingState(Enemy enemy, EnemyStateHandler stateMachine, Animator animator) : base(enemy, stateMachine)
    {
        this.animator = animator;
        this.self = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        randOffsetY = UnityEngine.Random.Range(0.2f, 1.5f);
    }

    public override void Exit()
    {
        base.Exit();
        animator.SetFloat("speed", 0);
        self.Stop();
    }

    public override void Update()
    {
        if (self.GetTarget() == null)
        {
            if (PlayerMovementController.Instance != null)
            {
                Debug.Log("Still no Player");
                return;
            }
            else
            {
                self.SetTarget(PlayerMovementController.Instance.gameObject);
            }
        }
        self.moveDest = getDesiredAttackPosition();
        self.Move();

        // Check if within attack range
        float attackDist = self.transform.position.DistanceSquared(self.GetTarget().transform.position);
        if (attackDist <= self.stats.attackRange * self.stats.attackRange)
        {
            self.stateMachine.ChangeState(self.combatState);
        }
        base.Update();
    }

    private Vector3 getDesiredAttackPosition()
    {
        Vector3 targetPos = self.GetTarget().transform.position;
        Vector3 directionToTarget = (self.transform.position - targetPos).normalized;

        return (self.GetTarget().transform.position + new Vector3(directionToTarget.x, randOffsetY * directionToTarget.y));
    }
}

