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
    [FoldoutGroup("Enemy Stats"), SerializeField] private Transform attackPoint;

    [FoldoutGroup("References")] public EnemyStateHandler stateMachine;
    [FoldoutGroup("References")] public EnemyIdleState idleState;
    [FoldoutGroup("References")] public EnemySeekingState seekingState; // Seeking: moving to player
    [FoldoutGroup("References")] public EnemyCombatState attackState;
    [FoldoutGroup("References")] public EnemyIsAttackingState isAttackingState;

    private Animator animator;
    private EnemyMovement movement;

    private void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        movement = GetComponent<EnemyMovement>();
    }

    void Start()
    {
        stateMachine = new EnemyStateHandler();
        seekingState = new EnemySeekingState(this, stateMachine, animator);
        attackState = new EnemyCombatState(this, stateMachine, animator);
        isAttackingState = new EnemyIsAttackingState(this, stateMachine, animator);
        idleState = new EnemyIdleState(this, stateMachine, animator);
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

    public void Stop()
    {
        movement.StopMovement();
    }
    
    // TODO: Update to use collider to be active during enemy swipe + dash at player?
    public void Attack()
    {
        lastAttack = Time.deltaTime;
        // Detect enemies in range of attack
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, stats.attackRange);

        // Apply damge to enemies
        foreach (Collider2D c in hitObjects)
        {
            IDamageable damagable = c.GetComponent<IDamageable>();

            if (damagable == null) continue;

            if (damagable.IsAlly(this.EntityType) || damagable.IsDead())
                continue;
            
            DamageData data = new DamageData(); 
            data.damageDealer = transform;
            data.target = c.transform;
            data.damageDealt = stats.damage;
            damagable.TakeDamage(data);
        }
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
}
