using UnityEngine;

public class ModeSelectorConfig : UICompanentConfig {
    public ModeSelectorConfig(string name, Sprite icon, ModeTypes type) {
        Name = name;
        Icon = icon;
        Type = type;
    }

    public string Name { get; private set; }
    public Sprite Icon { get; private set; }
    public ModeTypes Type { get; private set; }

    public override void OnValidate() {

    }
}
