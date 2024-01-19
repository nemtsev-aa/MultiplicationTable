using System;
using UnityEngine;

[Serializable]
public struct ModeData {
    [field: SerializeField] public ModeTypes Type { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}
