public class NumberViewConfig : UICompanentConfig {
    public NumberViewConfig(int multiplier) {
        Multiplier = multiplier;
    }

    public int Multiplier { get; private set; }

    public override void OnValidate() {

    }
}
