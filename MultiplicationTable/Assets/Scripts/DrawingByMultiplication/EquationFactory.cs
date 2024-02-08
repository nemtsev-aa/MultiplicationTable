using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquationFactory {
    private const int MaxEquationCount = 100;
    private List<int> _multipliers = new List<int>() { 2, 3, 4, 5, 6, 7, 8, 9 };

    private DifficultyLevelsConfig _difficultyConfig;
    private DifficultyLevelTypes _difficultyLevel;
    private List<EquationData> _equations;

    public EquationFactory(DifficultyLevelsConfig difficultyConfig) {
        _difficultyConfig = difficultyConfig;
    }

    public List<int> Multipliers => _multipliers;

    public List<EquationData> GetEquations(List<int> multipliables, DifficultyLevelTypes difficultyLevel) {

        if (multipliables != null && multipliables.Count > 0)
            return CreateEquationDataList(multipliables, difficultyLevel);
        else
            throw new ApplicationException($"MultipliablesList is not set or empty");
    }

    private List<EquationData> CreateEquationDataList(List<int> multipliables, DifficultyLevelTypes difficultyLevel) {
        _difficultyLevel = difficultyLevel;
        _equations = new List<EquationData>();

        var difficultyLevelData = GetDifficultyLevelDataByType(difficultyLevel);
        float equationCount = Mathf.RoundToInt(MaxEquationCount / difficultyLevelData.TimeDuration);

        while(_equations.Count < equationCount) {
            int multipliable = GetRandomNumberFromList(multipliables);
            int multiplier = GetRandomNumberFromList(_multipliers);

            var equation = new EquationData(multipliable, multiplier);

            if (CheckEquationCopyCount(equation, difficultyLevelData.AllowedCopyCount))
                _equations.Add(equation);
            else
                continue;
        }

        return _equations;
    }

    public DifficultyLevelData GetDifficultyLevelDataByType(DifficultyLevelTypes type) {
        return _difficultyConfig.Configs.FirstOrDefault(config => config.Type == type);
    }

    private int GetRandomNumberFromList(List<int> numbers) {
        int randomIndex = UnityEngine.Random.Range(0, numbers.Count);
        return numbers[randomIndex];
    }

    private bool CheckEquationCopyCount(EquationData equation, int allowedCopiesCount) {
        var equationCopyCount = _equations.Where(data 
            => data.Multipliable == equation.Multipliable && 
            data.Multiplier == equation.Multiplier).Count();

        if (equationCopyCount <= allowedCopiesCount)
            return true;
        else
            return false;
    }
}

