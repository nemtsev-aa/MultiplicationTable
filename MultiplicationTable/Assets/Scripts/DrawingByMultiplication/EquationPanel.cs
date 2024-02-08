using System;
using UnityEngine;

public class EquationPanel : UIPanel {
    public event Action<bool> EquationVerificatedChanged;

    [SerializeField] private EquationView _equationView;
    private MultiplierSelectionPanel _multipliersPanel;

    private EquationData _data;
    private int _maxEquationCount;

    public void Init(MultiplierSelectionPanel multipliersPanel) {
        //if (_multipliersPanel != null)
        //    return;

        _multipliersPanel = multipliersPanel;
        AddListeners();
    }

    public override void AddListeners() {
        base.AddListeners();

        _multipliersPanel.MultiplierSelected += OnMultiplierSelected;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _multipliersPanel.MultiplierSelected -= OnMultiplierSelected;
    }

    public override void Reset() {
        base.Reset();

        _data = null;
        _equationView.Reset();

        RemoveListeners();
    }

    public void ShowEquation(EquationData data) {
        if (data != null) {
            _data = data;

            var config = new EquationViewConfig(_data);
            _equationView.Init(this, config);
        }
    }
    
    private void OnMultiplierSelected(int multiplier) {
        _equationView.ShowMultiplier($"{multiplier}");

        EquationVerification(multiplier);
    }

    private void EquationVerification(int multiplier) {
        bool result = (multiplier == _data.Multiplier) ? true : false;
        
        _equationView.ShowEquationVerificationResult(result);
        EquationVerificatedChanged?.Invoke(result);
    }
}
