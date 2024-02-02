using UnityEngine;

public class TrainingGameSelectorConfig : UICompanentConfig {
    public TrainingGameSelectorConfig(string name, Sprite icon, TrainingGameTypes type) {
        Name = name;
        Icon = icon;
        Type = type;
    }

    public string Name { get; private set; }
    public Sprite Icon { get; private set; }
    public TrainingGameTypes Type { get; private set; }

    public override void OnValidate() {

    }
}
