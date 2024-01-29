using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class DrawingByMultiplicationDialog : Dialog {
    private CellsPanel _cellsPanel;
    private EquationPanel _equationPanel;
    private NavigatingByCellsArrayPanel _navigatingPanel;
    private MultiplierSelectionPanel _multipliersPanel;
    private QuestionsConfig _question;

    private EquationData _currentEquation;

    [Inject]
    private void Construct(QuestionsConfig question) {
        _question = question;
    }

    public override void AddListeners() {
        base.AddListeners();

        _cellsPanel.ActiveCellChanged += OnActiveCellChanged;
        _navigatingPanel.DirectionChanged += OnDirectionChanged;
        _multipliersPanel.MultiplierSelected += OnMultiplierSelected;
        _equationPanel.MultiplierSelectionPanelShowed += OnMultiplierSelectionPanelShowed;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _cellsPanel.ActiveCellChanged -= OnActiveCellChanged;
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
        _currentEquation = GetQuestionData(4, 5);
        _equationPanel.ShowEquation(_currentEquation);

        _multipliersPanel = GetPanelByType<MultiplierSelectionPanel>();
        var multipliers = new List<int>() { 2, 3, 4, 5, 6, 7, 8, 9 };
        _multipliersPanel.Init(new MultipliersConfig(multipliers));


        _navigatingPanel = GetPanelByType<NavigatingByCellsArrayPanel>();
        _navigatingPanel.Init();
    }

    private void OnActiveCellChanged(Cell activeCell) {
        int multipliable = (int)activeCell.Position.x;
        int multiplier = (int)activeCell.Position.y;

        _currentEquation = GetQuestionData(multipliable, multiplier);
        _equationPanel.ShowEquation(_currentEquation);
    }

    private void OnDirectionChanged(OffsetDirections direction) {
        _cellsPanel.GetActiveCell(direction);
        Debug.Log($"Direction: {direction}");
    }

    private void OnMultiplierSelected(int multiplier) {
        _equationPanel.SetMultiplier(multiplier);

        QuationVerification(multiplier);
    }

    private void QuationVerification(int multiplier) {
        if (multiplier == _currentEquation.Multiplier) 
            _cellsPanel.FillActiveCell(_currentEquation.BaseColor);

    }

    private void OnMultiplierSelectionPanelShowed(bool status) 
        => _multipliersPanel.Show(status);

    private EquationData GetQuestionData(int multipliable, int multiplier) {
        var multipliables = _question.Equations.Where(data => data.Multipliable == multipliable);
        EquationData data = multipliables.First(data => data.Multiplier == multiplier);

        if (data != null)
            return data;

        return null;
    }
}
