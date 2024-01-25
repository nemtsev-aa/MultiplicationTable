using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DrawingByMultiplicationDialog : Dialog {
    private CellsPanel _cellsPanel;
    private EquationPanel _equationPanel;
    private NavigatingByCellsArrayPanel _navigatingPanel;
    private MultiplierSelectionPanel _multipliersPanel;
    private QuestionsConfig _question;

    [Inject]
    private void Construct(QuestionsConfig question) {
        _question = question;
    }

    public override void AddListeners() {
        base.AddListeners();

        _navigatingPanel.DirectionChanged += OnDirectionChanged;
        _multipliersPanel.MultiplierSelected += OnMultiplierSelected;
        _equationPanel.MultiplierSelectionPanelShowed += OnMultiplierSelectionPanelShowed;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _navigatingPanel.DirectionChanged -= OnDirectionChanged;
        _multipliersPanel.MultiplierSelected -= OnMultiplierSelected;
        _equationPanel.MultiplierSelectionPanelShowed -= OnMultiplierSelectionPanelShowed;
    }

    public override void InitializationPanels() {
        base.InitializationPanels();

        _cellsPanel = GetPanelByType<CellsPanel>();
        _cellsPanel.Init();

        _equationPanel = GetPanelByType<EquationPanel>();
        _equationPanel.Init();
        _equationPanel.ShowEquation(GetQuestionData());

        _multipliersPanel = GetPanelByType<MultiplierSelectionPanel>();
        var multipliers = new List<int>() { 2, 3, 4, 5, 6, 7, 8, 9 };
        _multipliersPanel.Init(new MultipliersConfig(multipliers));


        _navigatingPanel = GetPanelByType<NavigatingByCellsArrayPanel>();
        _navigatingPanel.Init();
    }

    private void OnDirectionChanged(OffsetDirections direction) {
        _cellsPanel.MoveActiveCell(direction);

        Debug.Log($"Direction: {direction}");
    }

    private void OnMultiplierSelected(int multiplier) {
        _multipliersPanel.Show(false);
        _equationPanel.SetMultiplier(multiplier);

        Debug.Log($"Multiplier: {multiplier}");
    }

    private void OnMultiplierSelectionPanelShowed(bool status) {
        _multipliersPanel.Show(status);
    }

    private EquationData GetQuestionData() {
        return _question.Equations[0];
    }
}
