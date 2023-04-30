using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttackState : EnemyState
{
    protected Animator animator;
    protected Enemy self;
    public float onEnter;

    public EnemyRangedAttackState(Enemy enemy, EnemyStateHandler stateMachine, Animator animator) : base(enemy, stateMachine)
    {
        this.animator = animator;
        self = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        // animator.SetTrigger("fire");
        onEnter = Time.time;
        self.Attack();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Time.time - onEnter >= self.stats.attackDuration)
        {
            stateMachine.ChangeState(self.combatState);
        }
    }
}
