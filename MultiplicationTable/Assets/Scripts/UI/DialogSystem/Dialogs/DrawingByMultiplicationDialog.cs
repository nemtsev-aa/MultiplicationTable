using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DrawingByMultiplicationDialog : Dialog {
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
        _equationPanel.ShowEquation(_currentEquation);
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
        _currentEquation = _equations[UnityEngine.Random.Range(0, _equations.Count)];
        _currentEquation.BaseColor = activeCell.FillStateColor;

        _equationPanel.ShowEquation(_currentEquation);
    }

    private void OnEmptyCellsCountChanged(int emptyCellsCount) {
        if (emptyCellsCount == 0)
            Debug.Log($"��e ������ ���������!");
        else
            Debug.Log($"��������� {emptyCellsCount}/{_equations.Count}!");
    }

    private void OnMultiplierSelected(int multiplier) {
        _equationPanel.SetMultiplier(multiplier);

        QuationVerification(multiplier);
    }

    private void QuationVerification(int multiplier) {
        if (multiplier == _currentEquation.Multiplier) {
            _cellsPanel.FillActiveCell();

            _equations.Remove(_currentEquation);
        }
    }

    private void OnMultiplierSelectionPanelShowed(bool status)
        => _multipliersPanel.Show(status);


}
