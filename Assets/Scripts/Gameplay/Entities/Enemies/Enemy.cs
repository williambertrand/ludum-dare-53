using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

//Is this for all enemies? Or is this just for the melee one? Depending on the answer
//You'll need to change the class name to MeleeEnemy or Bandit or something :)
public class Enemy : Entity
{
    [FoldoutGroup("Enemy Stats")] public GameObject target;
    [FoldoutGroup("Enemy Stats")] public EnemyStats stats;
    [FoldoutGroup("Enemy Stats")] public Vector3 moveDest;
    [FoldoutGroup("Enemy Stats")] public float lastAttack;

    [FoldoutGroup("References")] public EnemyStateHandler stateMachine;
    [FoldoutGroup("References")] public EnemySeekingState seekingState;
    [FoldoutGroup("References")] public EnemyAttackState attackState;
    [FoldoutGroup("References")] public EnemyIsAttackingState isAttackingState;

    private Animator animator;
    private EnemyMovement movement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<EnemyMovement>();
    }

    void Start()
    {
        stateMachine = new EnemyStateHandler();
        seekingState = new EnemySeekingState(this, stateMachine, animator);
        attackState = new EnemyAttackState(this, stateMachine, animator);
        isAttackingState = new EnemyIsAttackingState(this, stateMachine, animator);
        stateMachine.Initialize(seekingState);
    }

    void Update()
    {
        if (stateMachine == null || stateMachine.CurrentState == null)
        {
            Debug.LogError("NO Current state");
        }
        stateMachine.CurrentState.Update();
    }

    public void Move()
    {
        movement.HandleMoveUpdate();
    }

    public void Stop()
    {
        movement.StopMovement();
    }

    //TODO: Handle the actual attack: Check for player overlap with attack boundary
    //Make sure to use the IDamageable interface instead of the EntityHealthController :)
    public void Attack()
    {
        lastAttack = Time.deltaTime;
    }
    private void OnDrawGizmos()
    {
        if (stateMachine == null) return;
        Handles.Label(transform.position + new Vector3(0f, -1.0f), stateMachine.CurrentState.ToString());

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);
    }
}
