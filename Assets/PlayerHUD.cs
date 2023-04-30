using OTBG.UI.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public ValueBar healthBar;

    private void Awake()
    {
        PlayerController.OnHealthChanged += PlayerController_OnHealthChanged;
    }

    private void OnDisable()
    {
        PlayerController.OnHealthChanged -= PlayerController_OnHealthChanged;
    }

    private void PlayerController_OnHealthChanged(ValueChange obj)
    {
        healthBar.UpdateValue(obj);
    }
}
