using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DrawingByMultiplicationDialog : Dialog {
    public event Action AllEmptyCellsFilled;

    private CellsPanel _cellsPanel;
    private EquationPanel _equationPanel;
    private MultiplierSelectionPanel _multipliersPanel;

    private DrawingsConfig _drawings;
    private EquationFactory _equationFactory;

    private List<EquationData> _equations;
    private EquationData _currentEquation;
    private TrainingGameData _data;


    [Inject]
    private void Construct(DrawingsConfig drawings, EquationFactory equationFactory) {
        _drawings = drawings;
        _equationFactory = equationFactory;
    }

    public void SetTrainingGameData(TrainingGameData data) {
        _data = data;

        _equations = _equationFactory.GetEquations(_data.Multipliers, _data.DifficultyLevelType);
        _cellsPanel.Init(GetRandonDrawingData(), _equations.Count);
    }

    public override void AddListeners() {
        base.AddListeners();

        _cellsPanel.ActiveCellChanged += OnActiveCellChanged;
        _cellsPanel.EmptyCellsCountChanged += OnEmptyCellsCountChanged;
        _multipliersPanel.MultiplierSelected += OnMultiplierSelected;
        _equationPanel.MultiplierSelectionPanelShowed += OnMultiplierSelectionPanelShowed;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _cellsPanel.ActiveCellChanged -= OnActiveCellChanged;
        _cellsPanel.EmptyCellsCountChanged -= OnEmptyCellsCountChanged;
        _multipliersPanel.MultiplierSelected -= OnMultiplierSelected;
        _equationPanel.MultiplierSelectionPanelShowed -= OnMultiplierSelectionPanelShowed;
    }

    public override void InitializationPanels() {
        base.InitializationPanels();

        _cellsPanel = GetPanelByType<CellsPanel>();

        _equationPanel = GetPanelByType<EquationPanel>();
        _equationPanel.Init();

        _multipliersPanel = GetPanelByType<MultiplierSelectionPanel>();
        var multipliers = new List<int>() { 2, 3, 4, 5, 6, 7, 8, 9 };
        _multipliersPanel.Init(new MultipliersConfig(multipliers));
    }

    private DrawingData GetRandonDrawingData() {
        return _drawings.Drawings[UnityEngine.Random.Range(0, _drawings.Drawings.Count)];
    }

    private void OnActiveCellChanged(Cell activeCell) {
        //_currentEquation = _equations[UnityEngine.Random.Range(0, _equations.Count)];
        _currentEquation = _equations[0];
        _currentEquation.BaseColor = activeCell.FillStateColor;

        _equationPanel.ShowEquation(_currentEquation);
    }

    private void OnEmptyCellsCountChanged(int emptyCellsCount) {
        if (emptyCellsCount == 0) {
            AllEmptyCellsFilled?.Invoke();
            Debug.Log($"Всe ячейки заполнены!");
        }
        else {
            OnActiveCellChanged(_cellsPanel.ActiveCell);
            Debug.Log($"Заполнено {emptyCellsCount}/{_equations.Count}!");
        }
    }

    private void OnMultiplierSelected(int multiplier) {
        _equationPanel.SetMultiplier(multiplier);

        QuationVerification(multiplier);
    }

    private void QuationVerification(int multiplier) {
        if (multiplier == _currentEquation.Multiplier) {
            _equations.Remove(_currentEquation);
            _cellsPanel.FillActiveCell(); 
        }
    }

    private void OnMultiplierSelectionPanelShowed(bool status)
        => _multipliersPanel.Show(status);

}
