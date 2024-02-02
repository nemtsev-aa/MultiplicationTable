using System.Collections.Generic;
using UnityEngine;

public class LearningModeDialog : Dialog {
    private MultiplicationPanel _multiplicationPanel;

    private List<int> _currentMultipliers;
    public List<int> Multipliers => _currentMultipliers;
    
    public override void InitializationPanels() {
        base.InitializationPanels();

        _multiplicationPanel = GetPanelByType<MultiplicationPanel>();
        _multiplicationPanel.Init();
    }

    public override void AddListeners() {
        base.AddListeners();

        _multiplicationPanel.MultiplierSelected += OnMultiplierSelected;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _multiplicationPanel.MultiplierSelected -= OnMultiplierSelected;
    }

    private void OnMultiplierSelected(List<int> multipliers) {
        _currentMultipliers = multipliers;

        Debug.Log($"Selection Multiplier: {_currentMultipliers}");
    }
}
