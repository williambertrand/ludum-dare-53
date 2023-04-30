using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    public static event Action<ValueChange> OnHealthChanged;

    public override void HealthController_OnHealthChanged(ValueChange obj)
    {
        OnHealthChanged?.Invoke(obj);
        base.HealthController_OnHealthChanged(obj);
    }
}
