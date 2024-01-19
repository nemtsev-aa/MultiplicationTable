using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ModsConfig), menuName = "Configs/" + nameof(ModsConfig))]
public class ModsConfig : ScriptableObject {
    [field: SerializeField] public List<ModeData> Configs { get; private set; }

    public ModeData CurrentModeData { get; private set; }

    public void SetModeDataByType(ModeTypes type) {
        CurrentModeData = Configs.FirstOrDefault(data => data.Type == type);
    }
}
