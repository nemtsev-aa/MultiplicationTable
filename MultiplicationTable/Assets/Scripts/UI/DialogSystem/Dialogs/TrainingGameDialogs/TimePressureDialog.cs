using System;
using Zenject;

public class TimePressureDialog : TrainingGameDialog {
    public override event Action<AttemptData> TrainingGameFinished;
    public override event Action<float, float> EquationsCountChanged;

    private TimerBar _timerBar;
    private EquationCountBar _equationCountBar;
    private EquationPanel _equationPanel;
    private MultiplierSelectionPanel _multipliersPanel;

    private EquationFactory _equationFactory;
    private TimeCounter _timeCounter;
    private int _maxEquationCount;

    [Inject]
    private void Construct(EquationFactory equationFactory, TimeCounter timeCounter) {
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
        _maxEquationCount = Equations.Count;

        _equationCountBar.Init(this, _maxEquationCount);
        _equationPanel.Init(_multipliersPanel);

    }

    public override void InitializationPanels() {
        _timerBar = GetPanelByType<TimerBar>();
        _timerBar.Init(_timeCounter);

        _equationCountBar = GetPanelByType<EquationCountBar>();
        _equationPanel = GetPanelByType<EquationPanel>();

        _multipliersPanel = GetPanelByType<MultiplierSelectionPanel>();
        var multipliersConfig = new MultipliersConfig(_equationFactory.Multipliers);
        _multipliersPanel.Init(multipliersConfig);
    }

    public override void AddListeners() {
        base.AddListeners();

        _equationPanel.EquationVerificatedChanged += OnEquationVerificatedChanged;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

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

    private void OnEquationVerificatedChanged(bool result) {
        PassedEquation.Add(CurrentEquation, result);

        if (result) {
            Equations.Remove(CurrentEquation);
            EquationsCountChanged?.Invoke(Equations.Count, _maxEquationCount);
        }
    }
}
