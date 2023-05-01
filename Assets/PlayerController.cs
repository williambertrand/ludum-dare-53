using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using OTBG.UI.Utility;
using OTBG.Audio;
using Mono.CSharp;

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
    public bool _isStunned;
    private float _stunnedAt;

    public bool lowHealthTriggered;

    public override void HealthController_OnHealthChanged(ValueChange obj)
    {
        if(obj.GetPercentage() < 0.4f && !lowHealthTriggered)
        {
            lowHealthTriggered = true;
            AudioManager.Instance.PlaySoundEffect(SFXIDs.PLAYER_LOW_HEALTH, false);
        }

        OnHealthChanged?.Invoke(obj);
        base.HealthController_OnHealthChanged(obj);
    }

    void Awake()
    {
        base.Awake();
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
    }   

    private void OnDisable()
    {
        _entityHealthController.OnDamaged -= Player_OnDamaged;
 
    }

    private void Player_OnDamaged(DamageData d)
    {
        AudioManager.Instance.PlaySoundEffect(SFXIDs.PLAYER_DAMAGED, true);

        _entityHealthController.SetInvulnerable(true);
        _stunnedAt = Time.time;
        _isStunned = true;
        _animator.SetTrigger("hurt");
    }

  

    private void Player_OnDeath()
    {
        
    }
}
