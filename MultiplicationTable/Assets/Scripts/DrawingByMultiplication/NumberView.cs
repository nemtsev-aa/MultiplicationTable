using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberView : UICompanent {
    public event Action<int> ViewSelected;

    [SerializeField] private Button _numberButton;
    [SerializeField] private TextMeshProUGUI _labelValue;

    private NumberViewConfig _config;
    
    public void Init(NumberViewConfig config) {
        _config = config;

        AddListeners();
        FillingCompanents();
    }

    private void AddListeners() {
        _numberButton.onClick.AddListener(SetMultiplier);
    }

    private void RemoveListeners() {
        _numberButton.onClick.RemoveListener(SetMultiplier);
    }

    private void SetMultiplier() =>
        ViewSelected?.Invoke(_config.Multiplier);

    private void FillingCompanents() =>
        _labelValue.text = $"{_config.Multiplier}";
    
    public override void Dispose() {
        base.Dispose();

        RemoveListeners();
    }
}
