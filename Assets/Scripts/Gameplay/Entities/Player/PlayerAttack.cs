using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using System.Linq;
using OTBG.Audio;

public class PlayerAttack : MonoBehaviour
{
    [FoldoutGroup("References"), SerializeField]
    private Entity entity;
    [FoldoutGroup("References"), SerializeField]
    private PlayerController playerController;

    [FoldoutGroup("Combat"), SerializeField]
    private float attackCooldown;
    private float lastAttackTime;

    [FoldoutGroup("Combat"), SerializeField] private LayerMask enemyLayers;

    [FoldoutGroup("Combat"), SerializeField] private Transform basicAttackPoint;
    [FoldoutGroup("Combat"), SerializeField] private int basicAttackDamage;
    [FoldoutGroup("Combat"), SerializeField] private float basicAttackRange;
    [FoldoutGroup("Combat"), SerializeField] private bool canDealDamage;



    [FoldoutGroup("Combo Management")]
    [SerializeField] private Combo basicCombo;


    // Hold current list of active attacks to determine if we've hit a combo
    private Stack<AttackInput> attackState;
    private string _debugCurrentStateString;
    [SerializeField] private float comboHangTime;

    private Animator animator;

    private void Awake()
    {
        //Always do references in awake :)
        animator = GetComponent<Animator>();
        entity = GetComponent<Entity>();
        playerController = GetComponent<PlayerController>();

    }

    public IEnumerator WaitForHealthController()
    {
        while(entity.healthController == null)
            yield return null;

        entity.healthController.OnDamaged += HealthController_OnDamaged;
    }

    private void OnDisable()
    {
        entity.healthController.OnDamaged -= HealthController_OnDamaged;
    }

    private void HealthController_OnDamaged(DamageData obj)
    {
        ClearAttackState();
    }

    // Start is called before the first frame update
    void Start()
    {
        attackState = new Stack<AttackInput>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAttackState();

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
        if (!CanAttack())
            return;
        
        lastAttackTime = Time.time;
        attackState.Push(new AttackInput(AttackType.Basic, Time.time));
        _debugCurrentStateString += AttackType.Basic.ToString() + ", ";

        HandleAnimationForAttack();

        if (CheckAttackStateAgainstBasicCombo())
        {
            // Execute combo
            _debugCurrentStateString = "Hit Basic Combo!";
            HandleAttack(basicCombo.damage);
            attackState.Clear();
        } else
        {
            HandleAttack(basicAttackDamage);
        }
    }

    private void HandleAnimationForAttack()
    {
        if (attackState.Count == 1)
        {
            animator.SetTrigger("attack");
            return;
        }
        else if (attackState.Count == 2)
        {
            animator.SetTrigger("attack2");
            return;
        }
        else if (attackState.Count == 3)
        {
            //Woah nice job, hit that combo!
            animator.SetTrigger("attack3");
            return;
        }

    }

    private void HandleAttack(int damage)
    {
        AudioManager.Instance.PlaySoundEffect(SFXIDs.PLAYER_ATTACK,true);

        if (!canDealDamage)
            return;

        // Detect enemies in range of attack
        List<Collider2D> hitEnemies = Physics2D.OverlapCircleAll(basicAttackPoint.position, basicAttackRange, enemyLayers).ToList().Take(3).ToList();

        // Apply damge to enemies
        foreach (Collider2D c in hitEnemies)
        {
            IDamageable enemyDamagable = c.GetComponent<IDamageable>();
            if (enemyDamagable.IsDead())
                continue;

            if (enemyDamagable != null)
            {
                // Show effect on enemy head
                EffectsManager.Instance.SpawnHitEffect(c.transform.position + new Vector3(0.0f, 0.5f));
                
                DamageData data = new DamageData();
                data.damageDealer = transform;
                data.target = c.transform;
                data.damageDealt = damage;
                enemyDamagable.TakeDamage(data);

                // Only allow an attack to hit a single enemy
                continue;
            }
        }
    }

    private bool CheckAttackStateAgainstBasicCombo()
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
    private void UpdateAttackState()
    {

        if (attackState.Count == 0) return;

        AttackInput lastInput = attackState.Peek();
        if (Time.time - lastInput.Time >= comboHangTime)
        {
            _debugCurrentStateString = "";
            attackState.Clear();
        }
    }

    public bool CanAttack()
    {
        if (playerController._isStunned) return false;
        return true;
    }

    // Can be called o ntaking damage to null out the combo state
    public void ClearAttackState()
    {
        attackState.Clear();
    }
    
}
