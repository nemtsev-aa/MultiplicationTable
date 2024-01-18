using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyLevelSelector : UICompanent {
    public event Action<DifficultyLevelSelectorConfig> Selected;

    [SerializeField] private TextMeshProUGUI _buttonName;
    [SerializeField] private Button _button;

    private DifficultyLevelSelectorConfig _config;

    public void Init(DifficultyLevelSelectorConfig config) {
        _config = config;

        UpdateCompanent();
    }

    private void OnEnable() {
        _button.onClick.AddListener(SelectorClick);
    }

    private void OnDisable() {
        _button.onClick.RemoveListener(SelectorClick);
    }


    private void SelectorClick() {
        Selected?.Invoke(_config);
    }

    private void UpdateCompanent() {
        _buttonName.text = _config.Name;
    }
}
