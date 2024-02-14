using System;
using Zenject;

public class AccordanceDialog : TrainingGameDialog {
    private const float DelaySwitchingEquation = 0.5f;

    public override event Action<AttemptData> TrainingGameFinished;
    public override event Action<float, float> EquationsCountChanged;

    private EquationCountBar _equationCountBar;
    private AccordancePanel _accordancePanel;

    private EquationFactory _equationFactory;
    private TimeCounter _timeCounter;
    //private LineSpawner _lineSpawner;

    private int _maxEquationCount;

    [Inject]
    private void Construct(EquationFactory equationFactory, TimeCounter timeCounter) {
        _equationFactory = equationFactory;
        TrainingGameType = TrainingGameTypes.Accordance;
        DialogType = DialogTypes.Accordance;

        _timeCounter = timeCounter;
        //_lineSpawner = lineSpawner;
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
        _accordancePanel.Init(Equations, false);
        //_lineSpawner.Init(_accordancePanel.GetConnectPointTransforms());
    }

    public override void InitializationPanels() {
        _equationCountBar = GetPanelByType<EquationCountBar>();
        _accordancePanel = GetPanelByType<AccordancePanel>();
    }

    public override void PreparingForClosure() {

    }
}
