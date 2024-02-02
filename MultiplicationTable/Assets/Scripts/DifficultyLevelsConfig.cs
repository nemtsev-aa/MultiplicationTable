using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(DifficultyLevelsConfig), menuName = "Configs/" + nameof(DifficultyLevelsConfig))]
public class DifficultyLevelsConfig : ScriptableObject {
    [field: SerializeField] public List<DifficultyLevelData> Configs { get; private set; }
}