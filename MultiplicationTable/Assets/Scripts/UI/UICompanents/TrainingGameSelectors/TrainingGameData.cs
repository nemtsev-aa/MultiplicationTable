using System.Collections.Generic;

public class TrainingGameData : MiniGameData {
    public TrainingGameData(ModeTypes modeType, TrainingGameTypes gameType, List<int> multipliers, DifficultyLevelTypes difficultyLevelType) : base(multipliers, difficultyLevelType) {
        ModeType = modeType;
        GameType = gameType;
        Multipliers = multipliers;
        DifficultyLevelType = difficultyLevelType;
    }

    public ModeTypes ModeType { get; private set; }
    public TrainingGameTypes GameType { get; private set; }
    
    public override string ToString() {
        return $"ModeType: {ModeType}, GameType: {GameType}, Multipliers: {Multipliers}, DifficultyLevelType: {DifficultyLevelType}";
    }
}
