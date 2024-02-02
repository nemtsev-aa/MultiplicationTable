public interface ICompanentVisitor {
    void Visit(UICompanentConfig companent);
    void Visit(ModeSelectorConfig modeSelector);
    void Visit(MultiplierButtonConfig multiplierButton);
    void Visit(DifficultyLevelSelectorConfig difficultyLevelSelector);
    void Visit(NumberViewConfig numberViewConfig);
    void Visit(CellConfig cellConfig);
    void Visit(TrainingGameSelectorConfig gameSelectorConfig);
 
}