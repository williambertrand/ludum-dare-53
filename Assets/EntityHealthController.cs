using Sirenix.OdinInspector;
using System;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class EntityHealthController : MonoBehaviour, IDamageable
{
    public event Action<ValueChange> OnHealthChanged;
    public event Action<DamageData> OnDamaged;

    [FoldoutGroup("References")]
    public Entity entity;

    [FoldoutGroup("Stats"), SerializeField]
    private float _health;
    [FoldoutGroup("Stats"), SerializeField]
    private float _maxHealth;

    private bool _isInvulnerable;

    //Call this from Spawner
    public void Initialise()
    {
        entity = GetComponent<Entity>();
        SetHealth(new ValueChange(_maxHealth, _maxHealth));
    }

    /// <summary>
    /// When you want to deal damage to an enemy. Do this.
    /// </summary>
    [Button]
    public void TakeDamage(DamageData damageData)
    {
        if (IsDead() || IsInvunerable())
            return;

        SetHealth(new ValueChange(_health -= damageData.damageDealt, _maxHealth));
        OnDamaged?.Invoke(damageData);
    }

    /// <summary>
    /// Call this to set the health to any value. It announces the OnHealthChanged event when called.
    /// </summary>
    /// <param name="valueChange"></param>
    public void SetHealth(ValueChange valueChange)
    {
        _health = Mathf.Clamp(valueChange.value,0,_maxHealth);
        OnHealthChanged?.Invoke(new ValueChange(_health, _maxHealth));

        if (_health <= 0)
            Death();
    }

    public void Death()
    {
        //TODO Add a second before despawning them for flickering in and out of existence.
        print("Entity died, destroying in 1 second");
        Destroy(this.gameObject, 1);
    }

    public bool IsInvunerable()
    {
        return _isInvulnerable;
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        this._isInvulnerable = isInvulnerable;
    }
    

    public bool IsAlly(EntityType type)
    {
        return entity.EntityType == type;
    }

    public bool IsDead()
    {
        return _health <= 0;
    }

}
