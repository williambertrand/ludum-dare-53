using UnityEngine;
using UnityEditor;

public class Enemy : MonoBehaviour
{

    [Header("Enemy Stats")]
    public EnemyStats stats;

    public Vector3 moveDest;
    public GameObject target;

    public EnemyStateHandler stateMachine;
    public EnemySeekingState seekingState;
    public EnemyAttackState attackState;
    public EnemyIsAttackingState isAttackingState;

    private Animator animator;
    private EnemyMovement movement;

    public float lastAttack;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<EnemyMovement>();

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

    private void OnDrawGizmos()
    {
        if (stateMachine == null) return;
        Handles.Label(transform.position + new Vector3(0f, -1.0f), stateMachine.CurrentState.ToString());

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);
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
    public void Attack()
    {
        lastAttack = Time.deltaTime;
    }

}
