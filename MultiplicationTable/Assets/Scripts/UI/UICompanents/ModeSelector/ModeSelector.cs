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

    public void Int(ModeSelectorConfig config, ToggleGroup group) {
        _config = config;
        _toggle.group = group;

        SetColors();
        FillingInComponents();
    }

    private void OnEnable() {
        _toggle.onValueChanged.AddListener(ToggleClick);
    }

    private void OnDisable() {
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
        if (status) {
            _modeName.color = _text—olorSelectedToggle;
            _modeIcon.color = _text—olorSelectedToggle;

            ModeSelected?.Invoke(_config.Type);
        }
        else {
            _modeName.color = _text—olorDeselectedToggle;
            _modeIcon.color = _text—olorDeselectedToggle;
        }
            
    }
}
