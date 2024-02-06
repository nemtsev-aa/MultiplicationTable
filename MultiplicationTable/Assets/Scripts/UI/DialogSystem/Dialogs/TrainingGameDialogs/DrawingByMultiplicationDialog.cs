using System;
using Zenject;

public class DrawingByMultiplicationDialog : TrainingGameDialog {
    public override event Action<TrainingGameData> TrainingGameFinished;

    private CellsPanel _cellsPanel;
    private EquationPanel _equationPanel;
    private MultiplierSelectionPanel _multipliersPanel;
    private TimerBar _timerBar;

    private DrawingsConfig _drawings;
    private EquationFactory _equationFactory;
    private TimeCounter _timeCounter;

    [Inject]
    private void Construct(DrawingsConfig drawings, EquationFactory equationFactory, TimeCounter timeCounter) {
        _drawings = drawings;
        _equationFactory = equationFactory;
        TrainingGameType = TrainingGameTypes.Drawing;
        _timeCounter = timeCounter;
    }

    public override void SetTrainingGameData(TrainingGameData data) {
        Data = data;

        Equations = _equationFactory.GetEquations(Data.Multipliers, Data.DifficultyLevelType);
        _cellsPanel.Init(GetRandonDrawingData(), Equations.Count);
        _equationPanel.Init(Equations.Count);
        //_timeCounter.SetTimeValue(3);
    }

    public override void Show(bool value) {
        base.Show(value);

        if (value == true && _timeCounter != null)
            _timeCounter.SetWatchStatus(true);
            //_timeCounter.SetTimerStatus(true);
        else if (value == false && _timeCounter != null)
            _timeCounter.SetWatchStatus(false);
            //_timeCounter.SetTimerStatus(false);
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

        _multipliersPanel = GetPanelByType<MultiplierSelectionPanel>();
        _multipliersPanel.Init(new MultipliersConfig(Multipliers));

        _timerBar = GetPanelByType<TimerBar>();
        _timerBar.Init(_timeCounter);
    }
    
    public override void ResetPanels() {
        base.ResetPanels();

        _timeCounter.Reset();
    }
    
    private DrawingData GetRandonDrawingData() {
        return _drawings.Drawings[UnityEngine.Random.Range(0, _drawings.Drawings.Count)];
    }

    private void OnActiveCellChanged(Cell activeCell) {
        //_currentEquation = _equations[UnityEngine.Random.Range(0, _equations.Count)];
        CurrentEquation = Equations[0];
        CurrentEquation.BaseColor = activeCell.FillStateColor;

        _equationPanel.ShowEquation(CurrentEquation);
    }

    private void OnEmptyCellsCountChanged(int emptyCellsCount) {
        if (emptyCellsCount == 0) {
            TrainingGameFinished?.Invoke(Data);
            ResetPanels();
        }
        else
            OnActiveCellChanged(_cellsPanel.ActiveCell);
    }

    private void OnMultiplierSelected(int multiplier) {
        _equationPanel.SetMultiplier(multiplier);

        EquationVerification(multiplier);
    }

    private void EquationVerification(int multiplier) {
        if (multiplier == CurrentEquation.Multiplier) {
            Equations.Remove(CurrentEquation);
            _equationPanel.ShowEquationVerificationResult(true, Equations.Count);

            _cellsPanel.FillActiveCell();
        }
        else 
        {
            _equationPanel.ShowEquationVerificationResult(false, Equations.Count);
            // Добавить количество ошибок в результат игры -> история игр
        }   
    }

    private void OnMultiplierSelectionPanelShowed(bool status)
        => _multipliersPanel.Show(status);

}
