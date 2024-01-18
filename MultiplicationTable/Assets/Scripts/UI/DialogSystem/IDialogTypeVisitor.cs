public interface IDialogTypeVisitor {
    void Visit(Dialog dialog);
    void Visit(MainMenuDialog mainMenu);
    void Visit(LearningModeDialog learning);
    void Visit(TrainingModeDialog training);
    void Visit(SettingsDialog settings);
    void Visit(HistoryDialog history);
}
