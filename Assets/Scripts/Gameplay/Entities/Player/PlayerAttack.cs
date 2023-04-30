using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class PlayerAttack : MonoBehaviour
{
    
    [SerializeField]
    private float attackCooldown;
    private float lastAttackTime;

    [SerializeField] private LayerMask enemyLayers;

    [Header("Basic Attack")]
    [SerializeField] private Transform basicAttackPoint;
    [SerializeField] private int basicAttackDamage;
    [SerializeField] private float basicAttackRange;

    [Header("Combo Management")]
    [SerializeField] private Combo basicCombo;


    // Hold current list of active attacks to determine if we've hit a combo
    private Stack<AttackInput> attackState;
    private string _debugCurrentStateString;
    [SerializeField] private float comboHangTime;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        attackState = new Stack<AttackInput>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        updateAttackState();

        bool canAttack = Time.time - lastAttackTime >= attackCooldown; // AND playerstate !== Rolling / dodging / hurt
        if (!canAttack) return;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Attack
            Attack();
        }
    }

    // TODO: if other attack types are added (e.g. heavy, kick, throw) then add a type param here
    private void Attack()
    {
        lastAttackTime = Time.time;
        attackState.Push(new AttackInput(AttackType.Basic, Time.time));
        _debugCurrentStateString += AttackType.Basic.ToString() + ", ";

        handleAnimationForAttack();

        if (checkAttackStateAgainstBasicCombo())
        {
            // Execute combo
            _debugCurrentStateString = "Hit Basic Combo!";
            animator.SetTrigger("basicCombo");
            handleAttack(basicCombo.damage);
        } else
        {
            handleAttack(basicAttackDamage);
        }
    }

    private void handleAnimationForAttack()
    {
        if (attackState.Count == 1)
        {
            Debug.Log("ATTACK BASIC ANIM 1!");
            animator.SetTrigger("attack");
            return;
        }
        else if (attackState.Count == 2)
        {
            Debug.Log("ATTACK ANIM 2!");
            animator.SetTrigger("attack2");
            return;
        } else
        {
            //Woah nice job, hit that combo!
            Debug.Log("ATTACK ANIM 3!");
            animator.SetTrigger("attack3");
        }

    }

    private void handleAttack(int damage)
    {
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(basicAttackPoint.position, basicAttackRange, enemyLayers);

        // Apply damge to enemies
        foreach (Collider2D c in hitEnemies)
        {
            TestEnemy e = c.GetComponent<TestEnemy>();
            if (e != null)
            {
                DamageData data = new DamageData();
                data.damageDealer = transform;
                data.target = e.transform;
                data.damageDealt = damage;
                e.TakeDamage(data);
            }
        }
    }

    private bool checkAttackStateAgainstBasicCombo()
    {
        var i = 0;
        foreach(AttackInput input in attackState)
        {
            if(basicCombo.attackTypes[i] != input.Type)
            {
                return false;
            }
            i++;
        }
        if(i == basicCombo.attackTypes.Count)
            return true;
        return false;
    }


    // Clear attack state if the combo hang time has been surpassed
    private void updateAttackState()
    {

        if (attackState.Count == 0) return;

        AttackInput lastInput = attackState.Peek();
        if (Time.time - lastInput.Time >= comboHangTime)
        {
            _debugCurrentStateString = "";
            attackState.Clear();
        }
    }

    // Can be called o ntaking damage to null out the combo state
    public void ClearAttackState()
    {
        attackState.Clear();
    }

    private void OnDrawGizmos()
    {
        if (attackState != null)
        {
            Handles.Label(transform.position + new Vector3(1.5f, 1.0f), _debugCurrentStateString);
        }

        if (basicAttackPoint != null)
        {
            Gizmos.DrawWireSphere(basicAttackPoint.position, basicAttackRange);
        }
    }
}
