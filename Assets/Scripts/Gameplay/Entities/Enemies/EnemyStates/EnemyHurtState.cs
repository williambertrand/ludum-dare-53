using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : EnemyState
{
    protected Animator animator;
    protected Enemy self;
    private float onEnter;
    private float duration;
    
    public EnemyHurtState(Enemy enemy, EnemyStateHandler stateMachine, Animator animator, float duration) : base(enemy, stateMachine)
    {
        this.animator = animator;
        self = enemy;
        this.duration = duration;
    }
    
    public override void Enter()
    {
        base.Enter();
        onEnter = Time.time;
        animator.SetTrigger("hurt");
    }

    public override void Exit()
    {
        base.Exit();
    }
    
    public override void Update()
    {
        if (self.IsDead())
        {
            // Stay in hurt state if enemy has died
            // TODO: if time add death fade out
            // animator.SetTrigger("dead");
            return;
        }
        
        if (Time.time - onEnter >= duration)
        {
            stateMachine.ChangeState(self.combatState);
        }
    }

}