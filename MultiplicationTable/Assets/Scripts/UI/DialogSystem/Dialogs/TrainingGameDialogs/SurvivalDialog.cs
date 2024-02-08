using System;
using Zenject;

public class SurvivalDialog : TrainingGameDialog {
    private const float DelaySwitchingEquation = 0.5f;

    public override event Action<AttemptData> TrainingGameFinished;
    public override event Action<float, float> EquationsCountChanged;

    private TimerBar _timerBar;
    private EquationCountBar _equationCountBar;
    private EquationPanel _equationPanel;
    private MultiplierSelectionPanel _multipliersPanel;

    private EquationFactory _equationFactory;
    private TimeCounter _timeCounter;
    private int _maxEquationCount;
    private float _timeDuration;

    [Inject]
    private void Construct(EquationFactory equationFactory, TimeCounter timeCounter) {
        _equationFactory = equationFactory;
        TrainingGameType = TrainingGameTypes.Survival;
        DialogType = DialogTypes.Survival;

        _timeCounter = timeCounter;
    }

    public override void Show(bool value) {
        base.Show(value);

        if (_timeCounter == null)
            return;

        _timeCounter.SetTimerStatus(true);
    }

    public override void SetTrainingGameData(TrainingGameData data) {
        base.SetTrainingGameData(data);

        Equations = _equationFactory.GetEquations(Data.Multipliers, Data.DifficultyLevelType);
        _maxEquationCount = Equations.Count;

        _equationCountBar.Init(this, _maxEquationCount);
        _equationPanel.Init(_multipliersPanel);

        GetRandomEquation();

        _multipliersPanel.Show(true);

        _timeDuration = GetTimeDurationByDifficultyLevelType(Data.DifficultyLevelType);
        _timeCounter.SetTimeValue(_timeDuration);
    }

    public override void InitializationPanels() {
        _timerBar = GetPanelByType<TimerBar>();
        _timerBar.Init(_timeCounter);

        _equationCountBar = GetPanelByType<EquationCountBar>();
        _equationPanel = GetPanelByType<EquationPanel>();

        _multipliersPanel = GetPanelByType<MultiplierSelectionPanel>();
        var multipliersConfig = new MultipliersConfig(_equationFactory.Multipliers);
        _multipliersPanel.Init(multipliersConfig, false);
    }

    public override void AddListeners() {
        base.AddListeners();

        _timeCounter.TimeIsOver += OnTimeIsOver;
        _equationPanel.EquationVerificatedChanged += OnEquationVerificatedChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _timeCounter.TimeIsOver -= OnTimeIsOver;
        _equationPanel.EquationVerificatedChanged -= OnEquationVerificatedChanged;
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

    private float GetTimeDurationByDifficultyLevelType(DifficultyLevelTypes difficultyLevelType) {
        DifficultyLevelData data = _equationFactory.GetDifficultyLevelDataByType(difficultyLevelType);
        return data.TimeDuration;
    }

    private void OnTimeIsOver() => PreparingForClosure();

    private void OnEquationVerificatedChanged(bool result) {
        SetVerificationResult(result);

        if (result) {
            Equations.Remove(CurrentEquation);
            EquationsCountChanged?.Invoke(Equations.Count, _maxEquationCount);
        }

        if (EquationsCount > 0)
            Invoke(nameof(GetRandomEquation), DelaySwitchingEquation);
        else
            Invoke(nameof(PreparingForClosure), DelaySwitchingEquation);
    }

    private void SetVerificationResult(bool result) {
        var equation = new EquationData(
            CurrentEquation.Multipliable,
            CurrentEquation.Multiplier,
            result);

        PassedEquation.Add(equation);
    }

    private void GetRandomEquation() {
        CurrentEquation = Equations[UnityEngine.Random.Range(0, Equations.Count)];
        _equationPanel.ShowEquation(CurrentEquation);
        _timeCounter.SetTimeValue(_timeDuration);
    }
}
