using System;
using System.Collections.Generic;

public abstract class TrainingGameDialog : Dialog {
    public virtual event Action<AttemptData> TrainingGameFinished;
    public virtual event Action<float, float> EquationsCountChanged;

    protected List<int> Multipliers = new List<int>() { 2, 3, 4, 5, 6, 7, 8, 9 };
    protected TrainingGameData Data;
    protected List<EquationData> Equations;
    protected EquationData CurrentEquation;

    public TrainingGameTypes TrainingGameType { get; set; }
    public DialogTypes DialogType { get; set; }

    protected List<EquationData> PassedEquation = new List<EquationData>();
    protected int EquationsCount => Equations.Count;
    
    public virtual void SetTrainingGameData(TrainingGameData data) {
        Data = data;
        PassedEquation.Clear();
    }
}
