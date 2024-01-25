using System.Collections.Generic;

public class MultipliersConfig {
    
    public MultipliersConfig(List<int> multipliers) {
        Multipliers = multipliers;
    }

    public List<int> Multipliers { get; private set; }
}
