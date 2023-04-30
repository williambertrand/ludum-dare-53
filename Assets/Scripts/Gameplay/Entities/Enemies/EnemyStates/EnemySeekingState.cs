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
        //animator.SetFloat("move", 0);
        self.Stop();
    }

    public override void Update()
    {
        if (self.target == null)
        {
            if (PlayerMovementController.Instance != null)
            {
                Debug.Log("Still no Player");
                return;
            }
            else
            {
                Debug.Log("Found Player");
                self.target = PlayerMovementController.Instance.gameObject;
            }
        }
        self.moveDest = getDesiredAttackPosition();
        self.Move();

        // Check if within attack range
        float playerDist = self.transform.position.DistanceSquared(self.target.transform.position);
        if (playerDist <= self.stats.attackRange * self.stats.attackRange)
        {
            self.stateMachine.ChangeState(self.combatState);
        }
        base.Update();
    }

    private Vector3 getDesiredAttackPosition()
    {
        Vector3 targetPos = self.target.transform.position;
        Vector3 directionToTarget = (self.transform.position - targetPos).normalized;

        return (self.target.transform.position + new Vector3(directionToTarget.x, randOffsetY * directionToTarget.y));
    }
}

