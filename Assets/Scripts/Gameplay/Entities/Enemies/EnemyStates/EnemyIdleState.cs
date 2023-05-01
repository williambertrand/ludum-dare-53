using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    protected Animator animator;
    protected Enemy self;

    public EnemyIdleState(Enemy enemy, EnemyStateHandler stateMachine, Animator animator) : base(enemy, stateMachine)
    {
        this.animator = animator;
        self = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        //animator.SetTrigger("idle");
    }

    public override void Exit()
    {
        base.Exit();
    }

}
