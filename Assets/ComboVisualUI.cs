using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboVisualUI : MonoBehaviour
{
    public Image comboVisual;
    public ComboTracking comboTracking;
    public void OnEnable()
    {
        ComboManager.OnComboChanged += ComboManager_OnComboChanged;
    }

    private void Start()
    {
        HideCombo();
    }

    private void ComboManager_OnComboChanged(ComboTracking combo)
    {
        if (combo.comboReq == 0)
        {
            HideCombo();
            return;
        }

        ShowCombo(combo);
    }

    public void HideCombo()
    {
        comboVisual.gameObject.SetActive(false);
    }

    public void ShowCombo(ComboTracking combo)
    {
        comboVisual.sprite = combo.comboSprite;

        comboVisual.gameObject.SetActive(true);


        if(comboTracking != combo)
            comboVisual.transform.DOPunchScale(transform.localScale * 1.05f, 0.2f);

        comboTracking = combo;
    }

    private void OnDisable()
    {
        ComboManager.OnComboChanged -= ComboManager_OnComboChanged; 
    }
}
