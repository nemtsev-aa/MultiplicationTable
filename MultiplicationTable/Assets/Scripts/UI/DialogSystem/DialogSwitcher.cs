
public class DialogSwitcher {
    private UIManager _uIManager;
    private Dialog _activeDialog;

    public DialogSwitcher(UIManager uIManager) {
        _uIManager = uIManager;
    }

    public void ShowDialog(DialogTypes type) {
        if (_activeDialog != null)
            _activeDialog.Show(false);
        
        _activeDialog = _uIManager.GetDialogByType(type);
        _activeDialog.Show(true);
    }

}
