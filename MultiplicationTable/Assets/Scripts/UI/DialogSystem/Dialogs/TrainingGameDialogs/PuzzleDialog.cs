using System;
using UnityEngine.UI;
using Zenject;

public class PuzzleDialog : TrainingGameDialog {
    private const float DelaySwitchingEquation = 0.5f;

    public override event Action<AttemptData> TrainingGameFinished;
    public override event Action<float, float> EquationsCountChanged;

    //private TimerBar _timerBar;
    private EquationCountBar _equationCountBar;
    private EquationBuildersPanel _equationBuildersPanel;
    private EquationItemsPanel _equationItemsPanel;

    private EquationFactory _equationFactory;
    private TimeCounter _timeCounter;
    private int _maxEquationCount;
    
    [Inject]
    private void Construct(EquationFactory equationFactory, TimeCounter timeCounter) {
        _equationFactory = equationFactory;
        TrainingGameType = TrainingGameTypes.Puzzles;
        DialogType = DialogTypes.Puzzle;

        _timeCounter = timeCounter;
    }

    public override void Show(bool value) {
        base.Show(value);

        if (_timeCounter == null)
            return;

        _timeCounter.SetWatchStatus(value);
    }

    public override void SetTrainingGameData(TrainingGameData data) {
        base.SetTrainingGameData(data);

        Equations = _equationFactory.GetEquations(Data.Multipliers, Data.DifficultyLevelType);
        _maxEquationCount = Equations.Count;

        _equationCountBar.Init(this, _maxEquationCount);
        _equationItemsPanel.Init(Equations);
        _equationBuildersPanel.Init(Equations, false);
    }

    public override void InitializationPanels() {
        _equationCountBar = GetPanelByType<EquationCountBar>();
        _equationBuildersPanel = GetPanelByType<EquationBuildersPanel>();
        _equationItemsPanel = GetPanelByType<EquationItemsPanel>();
    }

    public override void AddListeners() {
        base.AddListeners();

        _equationBuildersPanel.EquationVerificatedChanged += OnEquationVerificatedChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _equationBuildersPanel.EquationVerificatedChanged -= OnEquationVerificatedChanged;
    }

    public override void ResetPanels() {
        base.ResetPanels();

        _timeCounter.Reset();
        PassedEquation.Clear();
    }

    public override void PreparingForClosure() {
        bool gameResult = (EquationsCount > 0) ? true : false;

        AttemptData data = new AttemptData(Data,
            _timeCounter.RemainingTime,
            gameResult,
            PassedEquation);

        TrainingGameFinished?.Invoke(data);
    }

    private void OnEquationVerificatedChanged(EquationData data, bool result) {
        SetVerificationResult(data, result);

        if (result) {
            Equations.Remove(data);
            EquationsCountChanged?.Invoke(Equations.Count, _maxEquationCount);
        }

        if (EquationsCount == 0)
            Invoke(nameof(PreparingForClosure), DelaySwitchingEquation);
    }

    private void SetVerificationResult(EquationData data, bool result) {
        var equation = new EquationData(
            data.Multipliable,
            data.Multiplier,
            result);

        PassedEquation.Add(equation);
    }
}
