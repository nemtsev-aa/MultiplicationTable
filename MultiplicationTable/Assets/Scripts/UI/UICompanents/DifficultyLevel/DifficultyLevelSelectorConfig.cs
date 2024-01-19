using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyLevelSelectorConfig : UICompanentConfig {
    public DifficultyLevelSelectorConfig(string name, DifficultyLevelsTypes type) {
        Name = name;
        Type = type;
    }

    public string Name { get; private set; }
    public DifficultyLevelsTypes Type { get; private set; }
    
    public override void OnValidate() { }
}
