public class MultipliersResultView : AccordanceCompanent {
    public int Answer { get; private set; }

    protected override string GetLabelValueText() {
        var resultConfig = (MultipliersResultConfig)Config;
        Answer = resultConfig.Result;

        return $"{Answer}";
    }
}


