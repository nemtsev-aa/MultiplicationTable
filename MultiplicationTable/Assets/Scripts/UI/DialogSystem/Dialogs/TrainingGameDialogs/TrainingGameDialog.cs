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

    protected Dictionary<EquationData, bool> PassedEquation = new Dictionary<EquationData, bool>();
    protected int EquationsCount => Equations.Count;
    
    public abstract void SetTrainingGameData(TrainingGameData data);
   
}
