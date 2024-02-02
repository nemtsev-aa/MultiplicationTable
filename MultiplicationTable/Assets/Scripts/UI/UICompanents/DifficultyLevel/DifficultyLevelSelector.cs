using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyLevelSelector : UICompanent {
    public event Action<DifficultyLevelTypes> Selected;

    [SerializeField] private TextMeshProUGUI _toggleName;
    [SerializeField] private Toggle _toggle;

    private Color _text—olorSelectedToggle;
    private Color _text—olorDeselectedToggle;

    private DifficultyLevelSelectorConfig _config;

    public void Init(DifficultyLevelSelectorConfig config, ToggleGroup toggleGroup) {
        _config = config;
        _toggle.group = toggleGroup;
        
        _text—olorSelectedToggle = _toggle.colors.normalColor;
        _text—olorDeselectedToggle = _toggle.colors.selectedColor;

        UpdateCompanent();
    }

    private void OnEnable() {
        _toggle.onValueChanged.AddListener(SelectorClick);
    }

    private void OnDisable() {
        _toggle.onValueChanged.RemoveListener(SelectorClick);
    }


    private void SelectorClick(bool status) {
        if (status) {
            _toggleName.color = _text—olorSelectedToggle;
            Selected?.Invoke(_config.Type);
        }
        else
            _toggleName.color = _text—olorDeselectedToggle;
    }

    private void UpdateCompanent() {
        _toggleName.text = _config.Name;
        _toggleName.color = _text—olorDeselectedToggle;
    }
}
