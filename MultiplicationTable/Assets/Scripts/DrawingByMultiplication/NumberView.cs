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

        CreateSubscribes();
        FillingCompanents();
    }

    private void CreateSubscribes() {
        _numberButton.onClick.AddListener(NumberButtonClick);
    }

    private void RemoveSubscribes() {
        _numberButton.onClick.RemoveListener(NumberButtonClick);
    }

    private void FillingCompanents() {
        _labelValue.text = $"{_config.Multiplier}";
    }

    private void NumberButtonClick() => ViewSelected?.Invoke(_config.Multiplier);

    public override void Dispose() {
        base.Dispose();

        RemoveSubscribes();
    }
}
