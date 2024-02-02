using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyLevelSelectorConfig : UICompanentConfig {
    public DifficultyLevelSelectorConfig(string name, DifficultyLevelTypes type) {
        Name = name;
        Type = type;
    }

    public string Name { get; private set; }
    public DifficultyLevelTypes Type { get; private set; }
    
    public override void OnValidate() { }
}
