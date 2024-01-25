using System;
using UnityEngine;

public class EquationPanel : UIPanel {
    public event Action<bool> MultiplierSelectionPanelShowed;

    [SerializeField] private EquationView _equationView;

    private EquationData _data;

    public void Init() {
        AddListeners();
    }

    public override void Show(bool value) {
        base.Show(value);

        _equationView.Show(value);
    }

    public override void AddListeners() {
        base.AddListeners();

        _equationView.NumberInputStatusChanged += OnNumberInputStatusChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _equationView.NumberInputStatusChanged -= OnNumberInputStatusChanged;
    }

    public void ShowEquation(EquationData data) {
        _data = data;

        var config = new EquationViewConfig(_data);
        _equationView.Init(config);
    }

    public void SetMultiplier(int multiplier) {
        _equationView.ShowMultiplier($"{multiplier}"); 
    }

    private void OnNumberInputStatusChanged(bool status) {
        MultiplierSelectionPanelShowed?.Invoke(status);
    }
}
