public class EquationViewConfig : UICompanentConfig {
    public EquationViewConfig(EquationData data) {
        Data = data;
    }

    public EquationData Data { get; private set; }

    public override void OnValidate() {
        
    }
}
