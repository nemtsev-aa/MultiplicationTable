using System;
using System.Collections.Generic;
using Zenject;

public class DrawingByMultiplicationDialog : TrainingGameDialog {
    public override event Action<AttemptData> TrainingGameFinished;
    public override event Action<float, float> EquationsCountChanged;

    private TimerBar _timerBar;
    private EquationCountBar _equationCountBar;
    private CellsPanel _cellsPanel;
    private EquationPanel _equationPanel;
    private MultiplierSelectionPanel _multipliersPanel;

    private DrawingsConfig _drawings;
    private EquationFactory _equationFactory;
    private TimeCounter _timeCounter;
    private int _maxEquationCount;

    [Inject]
    private void Construct(DrawingsConfig drawings, EquationFactory equationFactory, TimeCounter timeCounter) {
        _drawings = drawings;
        _equationFactory = equationFactory;
        TrainingGameType = TrainingGameTypes.Drawing;
        _timeCounter = timeCounter;
    }

    public override void Show(bool value) {
        base.Show(value);

        if (_timeCounter == null)
            return;

        _timeCounter.SetWatchStatus(value);
    }
    
    public override void SetTrainingGameData(TrainingGameData data) {
        Data = data;

        Equations = _equationFactory.GetEquations(Data.Multipliers, Data.DifficultyLevelType);
        _maxEquationCount = EquationsCount;

        _cellsPanel.Init(GetRandonDrawingData(), _maxEquationCount);
        
        _equationCountBar.Init(this, _maxEquationCount);
        _equationPanel.Init(_multipliersPanel);
    }

    public override void InitializationPanels() { 
        _timerBar = GetPanelByType<TimerBar>();
        _timerBar.Init(_timeCounter);

        _equationCountBar = GetPanelByType<EquationCountBar>();
        _cellsPanel = GetPanelByType<CellsPanel>();
        _equationPanel = GetPanelByType<EquationPanel>();

        _multipliersPanel = GetPanelByType<MultiplierSelectionPanel>();
        var multipliersConfig = new MultipliersConfig(_equationFactory.Multipliers);
        _multipliersPanel.Init(multipliersConfig);
    }

    public override void AddListeners() {
        base.AddListeners();

        _cellsPanel.ActiveCellChanged += OnActiveCellChanged;
        _cellsPanel.EmptyCellsCountChanged += OnEmptyCellsCountChanged;

        _equationPanel.EquationVerificatedChanged += OnEquationVerificatedChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _cellsPanel.ActiveCellChanged -= OnActiveCellChanged;
        _cellsPanel.EmptyCellsCountChanged -= OnEmptyCellsCountChanged;

        _equationPanel.EquationVerificatedChanged -= OnEquationVerificatedChanged;
    }

    public override void ResetPanels() {
        base.ResetPanels();

        _timeCounter.Reset();
    }
    
    public override void PreparingForClosure() {
        bool gameResult = (EquationsCount > 0) ? true : false;
        AttemptData data = new AttemptData(Data, _timeCounter.RemainingTime, gameResult, PassedEquation);
        TrainingGameFinished?.Invoke(data);
    }

    private DrawingData GetRandonDrawingData() {
        return _drawings.Drawings[UnityEngine.Random.Range(0, _drawings.Drawings.Count)];
    }

    private void OnActiveCellChanged(Cell activeCell) {
        CurrentEquation = Equations[UnityEngine.Random.Range(0, Equations.Count)];
        CurrentEquation.BaseColor = activeCell.FillStateColor;

        _equationPanel.ShowEquation(CurrentEquation);
    }

    private void OnEquationVerificatedChanged(bool result) {
        PassedEquation.Add(CurrentEquation, result);

        if (result) {
            Equations.Remove(CurrentEquation);
            _cellsPanel.FillActiveCell();

            EquationsCountChanged?.Invoke(EquationsCount, _maxEquationCount);
        }
    }

    private void OnEmptyCellsCountChanged(int emptyCellsCount) {
        if (emptyCellsCount == 0) {
            PreparingForClosure();
            return;
        }

        OnActiveCellChanged(_cellsPanel.ActiveCell);
    }
}
