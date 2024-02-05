using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrainingGameSelector : UICompanent {
    public event Action<TrainingGameTypes> TrainingGameSelected;

    [SerializeField] private Toggle _toggle;
    [SerializeField] private TextMeshProUGUI _gameName;
    [SerializeField] private Image _gameIcon;

    private Color _text—olorSelectedToggle;
    private Color _text—olorDeselectedToggle;
    private TrainingGameSelectorConfig _config;
    
    public void Int(TrainingGameSelectorConfig config, ToggleGroup group) {
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
        _gameName.text = $"{_config.Name}";
        _gameIcon.sprite = _config.Icon;
    }

    private void ToggleClick(bool status) {
         if (status == true) {
            _gameName.color = _text—olorSelectedToggle;
            _gameIcon.color = _text—olorSelectedToggle;
        }
        else if (status == false) {
            _gameName.color = _text—olorDeselectedToggle;
            _gameIcon.color = _text—olorDeselectedToggle; 
        }

        TrainingGameSelected?.Invoke(_config.Type);
    }

    public override void Dispose() {
        base.Dispose();

        RemoveSubscribes();
    }
}
