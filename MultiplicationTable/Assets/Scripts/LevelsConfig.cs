using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(LevelsConfig), menuName = "Configs/" + nameof(LevelsConfig))]
public class LevelsConfig : ScriptableObject {
    [field: SerializeField] public List<DifficultyLevelData> Configs { get; private set; }

    public DifficultyLevelData CurrentDifficultyLevelData { get; private set; }

    public void SetDifficultyLevelByType(DifficultyLevelsTypes type) {
        CurrentDifficultyLevelData = Configs.FirstOrDefault(data => data.Type == type);
    }
}