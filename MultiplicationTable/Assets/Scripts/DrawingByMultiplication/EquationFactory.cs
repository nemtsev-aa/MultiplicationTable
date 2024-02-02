using System;
using System.Collections.Generic;
using System.Linq;

public class EquationFactory {
    private const int MaxEquationCount = 100;
    private List<int> _multipliers = new List<int>() { 2, 3, 4, 5, 6, 7, 8, 9 };

    private DifficultyLevelsConfig _difficultyConfig;

    public EquationFactory(DifficultyLevelsConfig difficultyConfig) {
        _difficultyConfig = difficultyConfig;
    }

    public List<EquationData> GetEquations(List<int> multipliables, DifficultyLevelTypes difficultyLevel) {

        if (multipliables != null && multipliables.Count > 0)
            return CreateEquationDataList(multipliables, difficultyLevel);
        else
            throw new ApplicationException($"MultipliablesList is not set or empty");
    }

    private List<EquationData> CreateEquationDataList(List<int> multipliables, DifficultyLevelTypes difficultyLevel) {
        var equations = new List<EquationData>();

        var difficultyLevelData = GetDifficultyLevelDataByType(difficultyLevel);
        float equationCount = MaxEquationCount / difficultyLevelData.TimeDuration;

        for (int i = 0; i < equationCount; i++) {
            int multipliable = GetRandomNumberFromList(multipliables);
            int multiplier = GetRandomNumberFromList(_multipliers);

            var equation = new EquationData(multipliable, multiplier);
            equations.Add(equation);
        }

        return equations;
    }

    private DifficultyLevelData GetDifficultyLevelDataByType(DifficultyLevelTypes type) {
        return _difficultyConfig.Configs.FirstOrDefault(config => config.Type == type);
    }

    private int GetRandomNumberFromList(List<int> numbers) {
        int randomIndex = UnityEngine.Random.Range(0, numbers.Count);
        return numbers[randomIndex];
    }

}
