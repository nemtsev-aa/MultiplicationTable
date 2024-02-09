public class EquationBuilderViewConfig : UICompanentConfig {
    public EquationBuilderViewConfig(EquationData data) {
        Data = data;
    }

    public EquationData Data { get; private set; }

    public override void OnValidate() {

    }
}
