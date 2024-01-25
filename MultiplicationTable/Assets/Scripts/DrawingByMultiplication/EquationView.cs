using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquationView : UICompanent {
    public event Action<bool> NumberInputStatusChanged;

    [SerializeField] private Button _numberInputButton;
    [SerializeField] private TextMeshProUGUI _multipliableText;
    [SerializeField] private TextMeshProUGUI _multiplierText;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private Image _backgroundColor;

    private bool _isSelect = false;
    private EquationData _data;

    public int Multiplier => _data.Multiplier;

    public void Init(EquationViewConfig config) {
        _data = config.Data;

        CreateSubscribers();
        FillingCompanents();
    }

    public override void Show(bool value) {
        base.Show(value);

        if (value == false)
            _isSelect = false;
    }
    
    public void ShowMultiplier(string value) {
        _multiplierText.text = value;
    }

    private void CreateSubscribers() {
        _numberInputButton.onClick.AddListener(NumberInputButtonClick);
    }

    private void RemoveSubscribers() {
        _numberInputButton.onClick.RemoveListener(NumberInputButtonClick);
    }

    private void FillingCompanents() {
        _multipliableText.text = $"{_data.Multipliable}";
        _resultText.text = $"{_data.Result}";
        _backgroundColor.color = _data.BaseColor;
    }

    private void NumberInputButtonClick() {
        if (_isSelect)
            _isSelect = false;
        else
            _isSelect = true;

        NumberInputStatusChanged?.Invoke(_isSelect);
    }

    public override void Dispose() {
        base.Dispose();

        RemoveSubscribers();
    }
}
