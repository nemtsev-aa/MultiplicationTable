using TMPro;
using UnityEngine;

public class EquationCountBar : Bar {
    [SerializeField] private TextMeshProUGUI _countValue;
    private EquationPanel _equationPanel;

    private int _maxValue;
    private int _currentValue = 0;

    public void Init(EquationPanel equationPanel, int count) {
        _equationPanel = equationPanel;
        _maxValue = count;

        OnValueChanged(_currentValue);

        AddListeners();
    }

    public override void AddListeners() {
        base.AddListeners();

        _equationPanel.EquationsCountChanged += OnValueChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _equationPanel.EquationsCountChanged -= OnValueChanged;
    }

    public override void Reset() {
        base.Reset();
        _maxValue = _currentValue = 0;
        _equationPanel = null;
    }

    protected override void OnValueChanged(float valueInParts) {
        if (valueInParts == _currentValue)
            _currentValue = 0;
        else if (valueInParts == 0)
            _currentValue = _maxValue;
        else
            _currentValue = _maxValue - (int)valueInParts;

        _countValue.text = $"{_currentValue}/{_maxValue}";
        Filler.fillAmount = (float)_currentValue / _maxValue;
    }
}
