using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OTBG.UI.Utility
{
    public class ValueBar : MonoBehaviour
    {
        [FoldoutGroup("References"), SerializeField] private Image _fillImage;
        [FoldoutGroup("References"),ShowIf(@"_doesShowText"), SerializeField] private TMP_Text _valueText;
        
        [FoldoutGroup("Values"),SerializeField] private bool _doesShowText;
        [FoldoutGroup("Values"),SerializeField] private bool _doesShowPercentage;

        private void OnValidate()
        {
            if(_valueText != null)
                _valueText.gameObject.SetActive(_doesShowText);
        }

        [Button]
        public void UpdateValue(ValueChange valueChange)
        {
            ShowValueText(valueChange);
            UpdateVisual(valueChange);
        }
        
        public void ShowValueText(ValueChange valueChange)
        {
            if (_valueText == null) return;
            if (!_doesShowPercentage)
                _valueText.text = $"{valueChange.value} / {valueChange.maxValue}";
            else
                _valueText.text = ((int)(valueChange.GetPercentage() * 100)).ToString();
        }
        
        public void ShowCustomText(string text)
        {
            if(_valueText == null) return;
            _valueText.text = text;
            _valueText.gameObject.SetActive(true);
        }

        private void UpdateVisual(ValueChange valueChange)
        {
            _fillImage.fillAmount = valueChange.GetPercentage();
        }
    }
}
