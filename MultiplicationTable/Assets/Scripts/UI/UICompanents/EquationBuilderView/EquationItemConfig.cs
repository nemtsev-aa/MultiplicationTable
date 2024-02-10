using UnityEngine;

public class EquationItemConfig : UICompanentConfig {
    public EquationItemConfig(int value, Canvas mainCanvas) {
        Value = value;
        MainCanvas = mainCanvas;
    }

    public int Value { get; private set; }
    public Canvas MainCanvas { get; private set; }

    public override void OnValidate() {

    }
}
