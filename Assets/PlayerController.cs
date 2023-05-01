using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using OTBG.UI.Utility;

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
    public ValueBar healthValueBar;

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
        _entityHealthController.OnDamaged += Player_OnDamaged;
        _entityHealthController.OnHealthChanged += Player_OnHealthChanged;
    }   

    private void OnDisable()
    {
        _entityHealthController.OnDamaged -= Player_OnDamaged;
        _entityHealthController.OnHealthChanged -= Player_OnHealthChanged;
    }

    private void Player_OnDamaged(DamageData d)
    {
        _entityHealthController.SetInvulnerable(true);
        _stunnedAt = Time.time;
        _isStunned = true;
        _animator.SetTrigger("hurt");
    }

    private void Player_OnHealthChanged(ValueChange valueChange)
    {
        healthValueBar.UpdateValue(valueChange);
    }

    private void Player_OnDeath()
    {
        
    }
}
