public class MultiplierButtonConfig : UICompanentConfig {
    public MultiplierButtonConfig(int index, MultiplierSelectorStatus status) {
        Index = index;
        Status = status;
    }

    public int Index { get; private set; }
    public MultiplierSelectorStatus Status { get; private set; }
    
    public override void OnValidate() {
        
    }
}
