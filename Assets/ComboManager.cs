using OTBG.Audio;
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
    private ComboTracking currentCombo;
    public ComboTracking CurrentCombo
    {
        get => currentCombo;
        set
        {
            if(currentCombo != value)
            {
                currentCombo = value;
                PlayAudioFileFromCombo(value);
            }
        }
    }
    public int currentComboIndex;

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
        currentComboIndex++;
        AnnounceComboChange();
    }

    [Button]
    public void LoseCombo()
    {
        currentComboIndex = 0;
        AnnounceComboChange();
    }

    public void AnnounceComboChange()
    {
        ComboTracking tracking = GetComboTracking(currentComboIndex);
        CurrentCombo = tracking;
        print(tracking.comboName);
        OnComboChanged?.Invoke(GetComboTracking(currentComboIndex));
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

    public void PlayAudioFileFromCombo(ComboTracking combo)
    {
        if (combo.audioFile == null)
            return;

        AudioManager.Instance.PlaySoundEffect(combo.audioFile, true);
    }
}

[System.Serializable]
public class ComboTracking
{
    public string comboName;
    public int comboReq;
    public Sprite comboSprite;
    public AudioClipSO audioFile;
}



