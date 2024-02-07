using System;
using UnityEngine;

[Serializable]
public class EquationData {
    public EquationData(int multipliable, int multiplier) {
        Multipliable = multipliable;
        Multiplier = multiplier;
    }
    
    [field: SerializeField] public int Multipliable { get; private set; }
    [field: SerializeField] public int Multiplier { get; private set; }
    [field: SerializeField] public Color BaseColor { get; set; }  
    public int Result { get { return Multipliable * Multiplier; } }
    public bool Answer { get; set; }
}
