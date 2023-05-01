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
    public float _health;
    [FoldoutGroup("Stats"), SerializeField]
    private float _maxHealth;

    private bool _isInvulnerable;
    public bool canTakeDamage;

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

        if (!canTakeDamage)
            damageData.damageDealt = 0;

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
        if (entity == null)
        {
            return false;
        }
        return entity.EntityType == type;
    }

    public bool IsDead()
    {
        return _health <= 0;
    }
}
