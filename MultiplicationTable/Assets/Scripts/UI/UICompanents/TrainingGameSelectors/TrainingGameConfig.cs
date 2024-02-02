using System;
using UnityEngine; 

[Serializable]
public struct TrainingGameConfig {
    [field: SerializeField] public TrainingGameTypes Type { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}
