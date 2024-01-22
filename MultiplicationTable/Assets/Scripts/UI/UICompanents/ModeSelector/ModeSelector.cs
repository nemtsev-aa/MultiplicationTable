using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelector : UICompanent {
    public event Action<ModeTypes> ModeSelected;

    [SerializeField] private Toggle _toggle;
    [SerializeField] private TextMeshProUGUI _modeName;
    [SerializeField] private Image _modeIcon;

    private Color _text—olorSelectedToggle;
    private Color _text—olorDeselectedToggle;
    private ModeSelectorConfig _config;
    private bool _isSelect = false;
    
    public void Int(ModeSelectorConfig config, ToggleGroup group) {
        _config = config;
        _toggle.group = group;

        CreateSubscribes();
        SetColors();
        FillingInComponents();
    }

    private void CreateSubscribes() {
        _toggle.onValueChanged.AddListener(ToggleClick);
    }
    
    public void RemoveSubscribes() {
        _toggle.onValueChanged.RemoveListener(ToggleClick);
    }

    private void SetColors() {
        _text—olorSelectedToggle = _toggle.colors.normalColor;
        _text—olorDeselectedToggle = _toggle.colors.selectedColor;
    }

    private void FillingInComponents() {
        _modeName.text = $"{_config.Name}";
        _modeIcon.sprite = _config.Icon;
    }

    private void ToggleClick(bool status) {
        if (status == true && _isSelect == true) {
            ModeSelected?.Invoke(_config.Type);
        } else if (status == true) {
            _modeName.color = _text—olorSelectedToggle;
            _modeIcon.color = _text—olorSelectedToggle;
            _isSelect = true;  
        } else if (status == false) {
            _modeName.color = _text—olorDeselectedToggle;
            _modeIcon.color = _text—olorDeselectedToggle;
            _isSelect = false;
        }    
    }

    public override void Dispose() {
        base.Dispose();

        RemoveSubscribes();
    }
}
