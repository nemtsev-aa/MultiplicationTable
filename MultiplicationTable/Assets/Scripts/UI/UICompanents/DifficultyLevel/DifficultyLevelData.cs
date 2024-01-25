using System;
using UnityEngine;

[Serializable]
public struct DifficultyLevelData {
    [field: SerializeField] public DifficultyLevelsTypes Type { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public float TimeDuration { get; private set; }
}