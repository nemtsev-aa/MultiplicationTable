public class MultipliersResultConfig : UICompanentConfig {
    public MultipliersResultConfig(int result) {
        Result = result;
    }

    public int Result { get; private set; }

    public override void OnValidate() {
        
    }
}
