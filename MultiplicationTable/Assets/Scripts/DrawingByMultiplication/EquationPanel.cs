using System;
using UnityEngine;

public class EquationPanel : UIPanel {
    public event Action<bool> MultiplierSelectionPanelShowed;

    [SerializeField] private EquationView _equationView;

    private EquationData _data;
    private bool _isSelect = false;

    public void Init() {
        AddListeners();
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
    }

    public void ShowEquation(EquationData data) {
        if (data != null) {
            _data = data;

            var config = new EquationViewConfig(_data);
            _equationView.Init(config);
        }
    }

    public void SetMultiplier(int multiplier) {
        _isSelect = false;
        _equationView.ShowMultiplier($"{multiplier}");
    }

    public void ShowQuationVerificationResult(bool result) {
        _equationView.ShowQuationVerificationResult(result);
    }

    private void OnNumberInputStatusChanged() {
        if (_isSelect)
            _isSelect = false;
        else
            _isSelect = true;

        MultiplierSelectionPanelShowed?.Invoke(_isSelect);
    }
}
