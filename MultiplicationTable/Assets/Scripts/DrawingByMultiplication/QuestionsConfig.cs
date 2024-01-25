using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(QuestionsConfig), menuName = "Configs/" + nameof(QuestionsConfig))]
public class QuestionsConfig : ScriptableObject {
    [field: SerializeField] public List<EquationData> Equations { get; private set; }
}
