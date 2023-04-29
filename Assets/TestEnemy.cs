using UnityEngine;

public class TestEnemy : Entity, IDamageable
{
    public void Death()
    {
        throw new System.NotImplementedException();
    }

    public override void Initialise()
    {
        base.Initialise();
    }

    public bool IsAlly(EntityType type)
    {
        throw new System.NotImplementedException();
    }

    public bool IsDead()
    {
        throw new System.NotImplementedException();
    }

    public bool IsInvunerable()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(DamageData damageData)
    {
        Debug.Log("took damage");
    }
}
