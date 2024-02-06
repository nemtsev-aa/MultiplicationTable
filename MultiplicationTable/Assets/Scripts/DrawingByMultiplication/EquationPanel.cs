using System;
using UnityEngine;

public class EquationPanel : UIPanel {
    public event Action<bool> MultiplierSelectionPanelShowed;
    public event Action<bool> EquationVerificatedChanged;
    public event Action<float, float> EquationsCountChanged;

    [SerializeField] private EquationView _equationView;
    [SerializeField] private EquationCountBar _equationCountBar;
    
    private EquationData _data;
    private bool _isSelect = false;
    private int _maxEquationCount;

    public void Init(int maxEquationCount) {
        AddListeners();

        _maxEquationCount = maxEquationCount;
        _equationCountBar.Init(this, _maxEquationCount);
    }

    public override void AddListeners() {
        base.AddListeners();

        _equationView.NumberInputStatusChanged += OnNumberInputStatusChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _equationView.NumberInputStatusChanged -= OnNumberInputStatusChanged;
    }

    public override void Reset() {
        base.Reset();

        _data = null;
        _isSelect = false;

        _equationView.Reset();
        _equationCountBar.Reset(); 
    }

    public void ShowEquation(EquationData data) {
        if (data != null) {
            _data = data;

            var config = new EquationViewConfig(_data);
            _equationView.Init(this, config);
        }
    }

    public void SetMultiplier(int multiplier) {
        _isSelect = false;
        _equationView.ShowMultiplier($"{multiplier}");
    }

    public void ShowEquationVerificationResult(bool result, float currentEquationCount) {
        if (result) 
            EquationsCountChanged?.Invoke(currentEquationCount, _maxEquationCount);

        EquationVerificatedChanged?.Invoke(result);
    }

    private void OnNumberInputStatusChanged() {
        if (_isSelect)
            _isSelect = false;
        else
            _isSelect = true;

        MultiplierSelectionPanelShowed?.Invoke(_isSelect);
    }
}
