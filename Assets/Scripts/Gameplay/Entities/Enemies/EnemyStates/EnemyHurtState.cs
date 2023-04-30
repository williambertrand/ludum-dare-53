using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : EnemyState
{
    protected Animator animator;
    protected Enemy self;
    private float onEnter;

    public EnemyHurtState(Enemy enemy, EnemyStateHandler stateMachine, Animator animator) : base(enemy, stateMachine)
    {
        this.animator = animator;
        self = enemy;
    }
    
    // TODO
    public override void Enter()
    {
        base.Enter();
        // animator.SetTrigger("hurt");
        
    }

    public override void Exit()
    {
        base.Exit();
    }
    
    public override void Update()
    {
        if (Time.time - onEnter >= self.stats.stunRecoveryTime)
        {
            stateMachine.ChangeState(self.combatState);
        }
    }

}