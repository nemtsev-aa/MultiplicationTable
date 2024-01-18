public class MainMenuDialog : Dialog {

    private MultiplicationPanel _multiplicationPanel;

    public override void Init() {
        base.Init();

    }

    public override void InitializationPanels() {
        base.InitializationPanels();

        _multiplicationPanel = GetPanelByType<MultiplicationPanel>();
        _multiplicationPanel.Init();
    }
}
