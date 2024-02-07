using TMPro;
using UnityEngine;

public class EquationCountBar : Bar {
    [SerializeField] private TextMeshProUGUI _countValue;
    private TrainingGameDialog _trainingGameDialog;
    
    private int _maxValue;
    private int _currentValue = 0;

    public void Init(TrainingGameDialog trainingGameDialog, int equationCount) {
        _maxValue = equationCount;
        _trainingGameDialog = trainingGameDialog;
        
        OnValueChanged(_currentValue, _maxValue);

        AddListeners();
    }

    public override void AddListeners() {
        base.AddListeners();

        _trainingGameDialog.EquationsCountChanged += OnValueChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _trainingGameDialog.EquationsCountChanged -= OnValueChanged;
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
