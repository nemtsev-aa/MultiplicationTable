public class MultipliersCompositionViewConfig : UICompanentConfig {
 
    public MultipliersCompositionViewConfig(EquationData data) {
        Data = data;
    }

    public EquationData Data { get; private set; }

    public override void OnValidate() {

    }
}
