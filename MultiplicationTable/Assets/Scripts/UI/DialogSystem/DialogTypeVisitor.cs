using System.Collections.Generic;
using System.Linq;

public class DialogTypeVisitor : IDialogTypeVisitor {
    private readonly IEnumerable<Dialog> _dialogs;

    public DialogTypeVisitor(IEnumerable<Dialog> dialogs) {
        _dialogs = dialogs;
    }

    public Dialog Dialog { get; private set; }

    public void Visit(Dialog dialog) => Visit((dynamic)dialog);

    public void Visit(MainMenuDialog mainMenu) => Dialog = _dialogs.FirstOrDefault(dialog => dialog is MainMenuDialog);

    public void Visit(LearningModeDialog learning) => Dialog = _dialogs.FirstOrDefault(dialog => dialog is LearningModeDialog);

    public void Visit(TrainingModeDialog training) => Dialog = _dialogs.FirstOrDefault(dialog => dialog is TrainingModeDialog);

    public void Visit(SettingsDialog settings) => Dialog = _dialogs.FirstOrDefault(dialog => dialog is SettingsDialog);

    public void Visit(HistoryDialog history) => Dialog = _dialogs.FirstOrDefault(dialog => dialog is HistoryDialog);
}
