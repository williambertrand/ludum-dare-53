using OTBG.UI.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonHealthHUD : MonoBehaviour
{
    public EntityHealthController healthController;
    public ValueBar healthBar;

    public void Awake()
    {
        StartCoroutine(WaitToAssign());
    }

    IEnumerator WaitToAssign()
    {
        while (healthController == null)
        {
            healthController = GetComponentInParent<EntityHealthController>();
            yield return null;
        }

        healthController.OnHealthChanged += HealthController_OnHealthChanged;

    }

    private void OnDisable()
    {
        healthController.OnHealthChanged -= HealthController_OnHealthChanged;
    }

    private void HealthController_OnHealthChanged(ValueChange obj)
    {
        healthBar.UpdateValue(obj);
    }
}
