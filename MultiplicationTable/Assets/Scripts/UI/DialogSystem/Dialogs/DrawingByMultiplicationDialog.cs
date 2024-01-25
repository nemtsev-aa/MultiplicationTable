using System;

public class DrawingByMultiplicationDialog : Dialog {
    private CellsPanel _cellsPanel;
    private EquationPanel _equationPanel;
    private NavigatingByCellsArrayPanel _navigatingPanel;
    private MultiplierSelectionPanel _multipliersPanel;


    public override void Init() {
        base.Init();

    }

    public override void AddListeners() {
        base.AddListeners();

        //_equationPanel.
        _navigatingPanel.DirectionChanged += OnDirectionChanged;
    }


    public override void RemoveListeners() {
        base.RemoveListeners();

    }

    public override void InitializationPanels() {
        base.InitializationPanels();

        _cellsPanel = GetPanelByType<CellsPanel>();
        _cellsPanel.Init();

        _equationPanel = GetPanelByType<EquationPanel>();
        _equationPanel.Init();
        _multipliersPanel = _equationPanel.MultiplierSelectionPanel;

        _navigatingPanel = GetPanelByType<NavigatingByCellsArrayPanel>();
        _navigatingPanel.Init();
    }

    private void OnDirectionChanged(OffsetDirections direction) {
        
    }
}
