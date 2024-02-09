using UnityEngine;

public class EquationItemConfig : UICompanentConfig {
    public EquationItemConfig(int value, Canvas mainCanvas, RectTransform rectTransform) {
        Value = value;
        MainCanvas = mainCanvas;
        RectTransform = rectTransform;
    }

    public int Value { get; private set; }
    public Canvas MainCanvas { get; private set; }
    public RectTransform RectTransform { get; private set; }
    
    public override void OnValidate() {

    }
}
