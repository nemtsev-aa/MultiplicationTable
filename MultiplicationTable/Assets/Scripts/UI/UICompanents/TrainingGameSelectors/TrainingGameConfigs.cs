using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(TrainingGameConfigs), menuName = "Configs/" + nameof(TrainingGameConfigs))]
public class TrainingGameConfigs : ScriptableObject {
    [field: SerializeField] public List<TrainingGameConfig> Configs { get; private set; }

    public TrainingGameConfig CurrentGameConfig { get; private set; }

}
