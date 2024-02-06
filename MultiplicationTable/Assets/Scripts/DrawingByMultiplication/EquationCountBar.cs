using TMPro;
using UnityEngine;

public class EquationCountBar : Bar {
    [SerializeField] private TextMeshProUGUI _countValue;
    private DrawingByMultiplicationDialog _drawingByMultiplicationDialog;
    
    private int _maxValue;
    private int _currentValue = 0;

    public void Init(DrawingByMultiplicationDialog drawingByMultiplicationDialog, int equationCount) {
        _maxValue = equationCount;
        _drawingByMultiplicationDialog = drawingByMultiplicationDialog;
        
        OnValueChanged(_currentValue, _maxValue);

        AddListeners();
    }

    public override void AddListeners() {
        base.AddListeners();

        _drawingByMultiplicationDialog.EquationsCountChanged += OnValueChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _drawingByMultiplicationDialog.EquationsCountChanged -= OnValueChanged;
    }

    public override void Reset() {
        base.Reset();
        
        _maxValue = _currentValue = 0;
    }

    protected override void OnValueChanged(float currentValue, float maxValue) {
        if (currentValue == _currentValue)
            _currentValue = 0;
        else if (currentValue == 0)
            _currentValue = _maxValue;
        else
            _currentValue = _maxValue - (int)currentValue;

        _countValue.text = $"{_currentValue}/{_maxValue}";
        Filler.fillAmount = (float)_currentValue / _maxValue;
    }
}
