public class MultipliersCompositionView : AccordanceCompanent {
    public int Multipliable { get; private set; }
    public int Multiplier { get; private set; }
    
    protected override string GetLabelValueText() {
        var compositionConfig = (MultipliersCompositionViewConfig)Config;
        Multipliable = compositionConfig.Data.Multipliable;
        Multiplier = compositionConfig.Data.Multiplier;

        return $"{Multipliable}X{Multiplier}";
    }
}

