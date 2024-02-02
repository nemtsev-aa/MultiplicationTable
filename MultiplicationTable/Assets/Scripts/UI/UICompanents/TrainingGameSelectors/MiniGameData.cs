using System;
using System.Collections.Generic;

[Serializable]
public class MiniGameData {
    public MiniGameData(List<int> multipliers, DifficultyLevelTypes difficultyLevelType) {
        Multipliers = multipliers;
        DifficultyLevelType = difficultyLevelType;
    }

    public List<int> Multipliers { get; set; }
    public DifficultyLevelTypes DifficultyLevelType { get; set; }
}
