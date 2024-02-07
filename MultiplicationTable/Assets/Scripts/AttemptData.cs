using System.Collections.Generic;
using System;

[Serializable]
public class AttemptData {
    public AttemptData(TrainingGameData gameData, float time, bool result, Dictionary<EquationData, bool> equations) {
        GameData = gameData;
        Time = time;
        Result = result;
        Equations = equations;
    }

    public TrainingGameData GameData { get; }
    public float Time { get; }
    public bool Result { get; }
    Dictionary<EquationData, bool> Equations { get; }
}
