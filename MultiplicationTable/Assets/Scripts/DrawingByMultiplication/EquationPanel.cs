using UnityEngine;

public class EquationPanel : UIPanel {
    [SerializeField] private EquationView _equationView;
    [field: SerializeField] public MultiplierSelectionPanel MultiplierSelectionPanel { get; private set; }

    public void Init() {
        MultiplierSelectionPanel.Show(false);

    }
}
