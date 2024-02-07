using System.Collections.Generic;
using System;

[Serializable]
public class AttemptData {
    public AttemptData(TrainingGameData gameData, float time, bool result, List<EquationData> equations) {
        GameData = gameData;
        Time = time;
        Result = result;
        Equations = equations;
    }

    public TrainingGameData GameData { get; }
    public float Time { get; }
    public bool Result { get; }
    List<EquationData> Equations { get; }
}
