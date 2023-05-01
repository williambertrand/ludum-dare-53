using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct EnemyStats
{
    public float moveSpeed;
    public float maxHealth;
    public float damage;
    public float attackRange;
    public float tryAttackRange; // Range to try an attack
    public float attackSpeed;
    public float attackDuration;
    public float stunRecoveryTime;
}
