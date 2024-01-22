using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningModeDialog : Dialog {
    private MultiplicationPanel _multiplicationPanel;

    private int _currentMultiplier;
    public int Multiplier => _currentMultiplier;
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

    private void OnMultiplierSelected(int multiplier) {
        _currentMultiplier = multiplier;

        Debug.Log($"Selection Multiplier: {_currentMultiplier}");
    }
}
