using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public struct PlayerStats
{
    public float recoveryTime;
}

public class PlayerController : Entity
{
    public static event Action<ValueChange> OnHealthChanged;
    private EntityHealthController _entityHealthController;

    [FoldoutGroup("Player Stats")] public PlayerStats stats;

    private Animator _animator;
    private bool _isStunned;
    private float _stunnedAt;

    public override void HealthController_OnHealthChanged(ValueChange obj)
    {
        OnHealthChanged?.Invoke(obj);
        base.HealthController_OnHealthChanged(obj);
    }

    void Awake()
    {
        _entityHealthController = GetComponent<EntityHealthController>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _isStunned = false;
    }

    void Update()
    {
        StunnedUpdate();
    }

    private void StunnedUpdate()
    {
        if (!_isStunned) return;
        
        if (Time.time - _stunnedAt > stats.recoveryTime)
        {
            _isStunned = false;
            _entityHealthController.SetInvulnerable(false);
        }
    }
    
    private void OnEnable()
    {
        Entity.OnDamaged += Player_OnDamaged;
    }   

    private void OnDisable()
    {
        Entity.OnDamaged -= Player_OnDamaged;
    }

    private void Player_OnDamaged(Entity e, DamageData d)
    {
        _entityHealthController.SetInvulnerable(true);
        _stunnedAt = Time.time;
        _isStunned = true;
        _animator.SetTrigger("hurt");
    }
}
