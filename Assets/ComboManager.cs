<<<<<<< HEAD
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
=======
using System.Collections;
using System.Collections.Generic;
>>>>>>> 96fd27836c5495c3758b496311161c19ffc4d850
using UnityEngine;

public class ComboManager : MonoBehaviour
{
<<<<<<< HEAD
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

    private void Entity_OnDamaged(Entity obj)
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
=======
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
>>>>>>> 96fd27836c5495c3758b496311161c19ffc4d850
