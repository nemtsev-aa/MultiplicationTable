using System;
using System.Collections.Generic;

public abstract class TrainingGameDialog : Dialog {
    public virtual event Action<TrainingGameData> TrainingGameFinished;

    protected List<int> Multipliers = new List<int>() { 2, 3, 4, 5, 6, 7, 8, 9 };
    protected TrainingGameData Data;
    protected List<EquationData> Equations;
    protected EquationData CurrentEquation;

    public TrainingGameTypes TrainingGameType { get; set; }

    public abstract void SetTrainingGameData(TrainingGameData data);

}
