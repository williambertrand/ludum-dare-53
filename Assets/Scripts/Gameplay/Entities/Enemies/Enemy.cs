using System;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

public enum TargetPriorityType
{
    Player,
    Carriage
}

public class Enemy : Entity
{
    [FoldoutGroup("Enemy Stats")] public TargetPriorityType priorityTarget;
    [FoldoutGroup("Enemy Stats")] public GameObject target;
    [FoldoutGroup("Enemy Stats")] public EnemyStats stats;
    [FoldoutGroup("Enemy Stats")] public Vector3 moveDest;
    [FoldoutGroup("Enemy Stats")] public float lastAttack;
    [FoldoutGroup("Enemy Stats"), SerializeField] protected Transform attackPoint;

    [FoldoutGroup("References")] public EnemyStateHandler stateMachine;
    [FoldoutGroup("References")] public EnemyIdleState idleState;
    [FoldoutGroup("References")] public EnemySeekingState seekingState; // Seeking: moving to player
    [FoldoutGroup("References")] public EnemyCombatState combatState;
    [FoldoutGroup("References")] public EnemyIsAttackingState isAttackingState;
    [FoldoutGroup("References")] public EnemyHurtState hurtState;
    
    private Animator animator;
    private EnemyMovement movement;

    private IDamageable _damageable;

    protected void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        movement = GetComponent<EnemyMovement>();
        _damageable = GetComponent<IDamageable>();
    }

    protected void Start()
    {
        stateMachine = new EnemyStateHandler();
        seekingState = new EnemySeekingState(this, stateMachine, animator);
        combatState = new EnemyCombatState(this, stateMachine, animator);
        isAttackingState = new EnemyIsAttackingState(this, stateMachine, animator);
        idleState = new EnemyIdleState(this, stateMachine, animator);
        hurtState = new EnemyHurtState(this, stateMachine, animator);
        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        if (stateMachine?.CurrentState == null)
        {
            Debug.LogError("NO Current state");
            return;
        }
        stateMachine.CurrentState.Update();
    }

    public void Move()
    {
        movement.HandleMoveUpdate();
    }

    public void FaceTarget()
    {
        movement.HandleDirectionCheck();
    }

    public void Stop()
    {
        movement.StopMovement();
    }
    
    // TODO: Update to use collider to be active during enemy swipe + dash at player?
    public virtual void Attack()
    {
        throw new NotImplementedException("Implement attack for enemy");
    }
    private void OnDrawGizmos()
    {
        if (stateMachine == null) return;
        Handles.Label(transform.position + new Vector3(0f, -1.0f), stateMachine.CurrentState.ToString());

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);
    }
    public void SetTarget(GameObject t)
    {
        this.target = t;
    }

    public GameObject GetTarget()
    {
        // TODO: Need a ref to carriage
        if (priorityTarget == TargetPriorityType.Carriage)
            return CarriageController.Instance.gameObject;
        return target;
    }
    
    private void OnEnable()
    {
        healthController.OnDamaged += Enemy_OnDamaged;
    }   

    private void OnDisable()
    {
        healthController.OnDamaged -= Enemy_OnDamaged;
    }

    public void Enemy_OnDamaged(DamageData d)
    {
        stateMachine.ChangeState(hurtState);
    }

    public bool IsDead()
    {
        return _damageable.IsDead();
    }
}
