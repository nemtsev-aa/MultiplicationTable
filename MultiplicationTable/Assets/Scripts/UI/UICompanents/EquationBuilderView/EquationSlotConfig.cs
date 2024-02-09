public class EquationSlotConfig : UICompanentConfig {
    public EquationSlotConfig() {
    }

    public EquationSlotConfig(EquationItem item) {
        Item = item;
    }

    public EquationItem Item { get; private set; }

    public override void OnValidate() {

    }
}
