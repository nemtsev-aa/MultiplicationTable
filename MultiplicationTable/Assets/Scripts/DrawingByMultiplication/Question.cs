using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Configs/Question")]
public class Question : ScriptableObject {
    [field: SerializeField] public List<EquationData> Equations { get; private set; }
}
