public interface ICompanentVisitor {
    void Visit(UICompanentConfig companent);
    void Visit(ModeSelectorConfig modeSelector);
    void Visit(MultiplierButtonConfig multiplierButton);
    void Visit(DifficultyLevelSelectorConfig difficultyLevelSelector);
}