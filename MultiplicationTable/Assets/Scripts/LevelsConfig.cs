using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelsConfig : MonoBehaviour {
    [field: SerializeField] public List<DifficultyLevelData> Configs { get; private set; }

    public DifficultyLevelData CurrentDifficultyLevelData { get; private set; }

    public void SetDifficultyLevelByType(DifficultyLevelsTypes type) {
        CurrentDifficultyLevelData = Configs.FirstOrDefault(data => data.Type == type);
    }
}