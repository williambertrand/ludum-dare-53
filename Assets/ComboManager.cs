using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComboManager : MonoBehaviour
{

    public static event Action<ComboTracking> OnComboChanged;

    public List<ComboTracking> tracking = new List<ComboTracking>();

    public int currentCombo;

    public void OnEnable()
    {
        Entity.OnDamaged += Entity_OnDamaged;
    }

    private void OnDisable()
    {
        Entity.OnDamaged -= Entity_OnDamaged;
    }

    [Button]
    public void AddToCombo()
    {
        currentCombo++;
        AnnounceComboChange();
    }

    [Button]
    public void LoseCombo()
    {
        currentCombo = 0;
        AnnounceComboChange();
    }

    public void AnnounceComboChange()
    {
        ComboTracking tracking = GetComboTracking(currentCombo);
        print(tracking.comboName);
        OnComboChanged?.Invoke(GetComboTracking(currentCombo));
    }

    public ComboTracking GetComboTracking(int currentCombo)
    {
        return tracking.Last(c => c.comboReq <= currentCombo);
    }

    private void Entity_OnDamaged(Entity obj, DamageData d)
    {
        if (obj.EntityType == EntityType.Player)
            LoseCombo();

        if (obj.EntityType == EntityType.Enemy)
            AddToCombo();
    }
}

[System.Serializable]
public class ComboTracking
{
    public string comboName;
    public int comboReq;
    public Sprite comboSprite;
}



