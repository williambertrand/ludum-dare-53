using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Basic
}

struct AttackInput
{
    AttackType type;
    float time;

    public AttackInput(AttackType type, float time)
    {
        this.type = type;
        this.time = time;
    }

    public float Time
    {
        get { return time; }
        set { time = value; }
    }

    public AttackType Type
    {
        get { return type; }
        set { type = value; }
    }
}

[System.Serializable]
public class Attack
{
    public AttackType type;
    public string anim;
    public float duration;
}

[System.Serializable]
public class Combo
{
    public List<AttackType> attackTypes;
}

public class PlayerAttack : MonoBehaviour
{
    

    [SerializeField]
    private float attackCooldown;
    private float lastAttackTime;

    [SerializeField] private Combo basicCombo;


    // Hold current list of active attacks to determine if we've hit a combo
    private Stack<AttackInput> attackState;

    [SerializeField] private float comboHangTime;

    // Start is called before the first frame update
    void Start()
    {
        attackState = new Stack<AttackInput>();
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    private bool checkAttackStateAgainstCombos()
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
        return true;
    }


    // Clear attack state if the combo hang time has been surpassed
    private void updateAttackState()
    {

        if (attackState.Count == 0) return;

        AttackInput lastInput = attackState.Peek();
        if (Time.time - lastInput.Time >= comboHangTime)
        {
            attackState.Clear();
        }
    }
}
