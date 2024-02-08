using System;
using UnityEngine;

[Serializable]
public class EquationData {
    public EquationData(int multipliable, int multiplier) {
        Multipliable = multipliable;
        Multiplier = multiplier;
    }

    public EquationData(int multipliable, int multiplier, bool answer) {
        Multipliable = multipliable;
        Multiplier = multiplier;
        Answer = answer;
    }

    public int Multipliable { get; private set; }
    public int Multiplier { get; private set; }
    public Color BaseColor { get; set; }  
    public int Result { get { return Multipliable * Multiplier; } }
    public bool Answer { get; private set; }

}
