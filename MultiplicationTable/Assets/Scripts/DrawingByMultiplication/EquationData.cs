using System;
using UnityEngine;

[Serializable]
public class EquationData {
    [field: SerializeField] public int Multipliable { get; private set; }
    [field: SerializeField] public int Result { get; private set; }
    [field: SerializeField] public Color BaseColor { get; private set; }

    public int Multiplier {
        get {
            return (int)(Result / Multipliable);
        }
    }
}
