using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AccordanceDialog : TrainingGameDialog {
    private const float DelaySwitchingEquation = 0.5f;

    public override event Action<AttemptData> TrainingGameFinished;
    public override event Action<float, float> EquationsCountChanged;

    private EquationCountBar _equationCountBar;


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

    }

    public override void InitializationPanels() {
        _equationCountBar = GetPanelByType<EquationCountBar>();

    }
    
    public override void PreparingForClosure() {
        
    }
}
