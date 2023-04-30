using Sirenix.OdinInspector;
using System;
using UnityEngine;

[RequireComponent(typeof(EntityHealthController))]
public class Entity : MonoBehaviour
{
    public event Action<Entity> OnSpawn;
    public event Action<Entity> OnDeath;
    public static event Action<Entity> OnDamaged;

    public EntityType EntityType;

    [FoldoutGroup("References")]
    public EntityHealthController healthController;

    [SerializeField]
    private bool hasInit;

    private void Awake()
    {
        if (hasInit)
            return;

        Initialise();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    public virtual void Initialise()
    {
        References();
        Initialisation();

        OnSpawn?.Invoke(this);

        hasInit = true;
    }

    public void References()
    {
        healthController = GetComponent<EntityHealthController>();

    }

    public void Initialisation()
    {
        healthController.Initialise();
        Subscribe();
    }

    public void Subscribe()
    {
        healthController.OnHealthChanged += HealthController_OnHealthChanged;
        healthController.OnDamaged += HealthController_OnDamaged;
    }

    private void HealthController_OnDamaged(DamageData obj)
    {
        OnDamaged?.Invoke(this);
    }

    public void Unsubscribe() 
    { 
        healthController.OnHealthChanged -= HealthController_OnHealthChanged;
        healthController.OnDamaged -= HealthController_OnDamaged;

    }

    public virtual void HealthController_OnHealthChanged(ValueChange obj)
    {
        if (obj.value <= 0)
            OnDeath?.Invoke(this);
    }
}
