using System.Collections;
using System.Collections.Generic;
using extensions;
using UnityEngine;

public class EnemySeekingState : EnemyState
{
    protected Animator animator;
    protected Enemy self;

    public EnemySeekingState(Enemy enemy, EnemyStateHandler stateMachine, Animator animator) : base(enemy, stateMachine)
    {
        this.animator = animator;
        this.self = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        if (PlayerMovementController.Instance != null)
        {
            self.target = PlayerMovementController.Instance.gameObject;
        }
    }

    public override void Exit()
    {
        base.Exit();
        animator.SetFloat("move", 0);
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
            self.stateMachine.ChangeState(self.attackState);
        }
        base.Update();
    }

    private Vector3 getDesiredAttackPosition()
    {
        Vector3 targetPos = self.target.transform.position;
        int deltaX = self.transform.position.x < targetPos.x ? -1 : 1;
        int deltaY = self.transform.position.x < targetPos.x ? -1 : 1;

        Vector3 delta = (self.transform.position - targetPos).normalized;

        return (self.target.transform.position + new Vector3(delta.x, 0.5f * delta.y));
    }
}
