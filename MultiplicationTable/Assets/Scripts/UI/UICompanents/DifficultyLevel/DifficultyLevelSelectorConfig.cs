using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyLevelSelectorConfig : UICompanentConfig {
    public DifficultyLevelSelectorConfig(string name) {
        Name = name;
    }

    public string Name { get; private set; }
    
    public override void OnValidate() {

    }
}
