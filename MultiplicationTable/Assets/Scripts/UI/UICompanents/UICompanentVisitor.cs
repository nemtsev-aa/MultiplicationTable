using System.Collections.Generic;
using System.Linq;

public class UICompanentVisitor : ICompanentVisitor {
    private List<UICompanent> _companents = new List<UICompanent>();

    public UICompanentVisitor(List<UICompanent> companents) {
        _companents = companents;
    }

    public UICompanent Companent { get; private set; }

    public void Visit(UICompanentConfig config) => Visit((dynamic)config);
    
    public void Visit(ModeSelectorConfig modeSelector)
        => Companent = _companents.FirstOrDefault(companent => companent is ModeSelector);

    public void Visit(MultiplierButtonConfig multiplierButton) 
        => Companent = _companents.FirstOrDefault(companent => companent is MultiplierSelector);

    public void Visit(DifficultyLevelSelectorConfig difficultyLevelSelector) 
        => Companent = _companents.FirstOrDefault(companent => companent is DifficultyLevelSelector);
    
    public void Visit(NumberViewConfig numberViewConfig)
        => Companent = _companents.FirstOrDefault(companent => companent is NumberView);

    public void Visit(CellConfig cellConfig) 
        => Companent = _companents.FirstOrDefault(companent => companent is Cell);

    public void Visit(TrainingGameSelectorConfig gameSelectorConfig)
        => Companent = _companents.FirstOrDefault(companent => companent is TrainingGameSelector);
    
    public void Visit(EquationBuilderViewConfig equationBuilderViewConfig)
        => Companent = _companents.FirstOrDefault(companent => companent is EquationBuilderView);
    
    public void Visit(EquationItemConfig equationItemConfig)
        => Companent = _companents.FirstOrDefault(companent => companent is EquationItem);

    public void Visit(EquationSlotConfig equationSlotConfig)
        => Companent = _companents.FirstOrDefault(companent => companent is EquationSlot);

    public void Visit(MultipliersCompositionViewConfig compositionViewConfig)
        => Companent = _companents.FirstOrDefault(companent => companent is MultipliersCompositionView);

    public void Visit(MultipliersResultConfig resultViewConfig)
        => Companent = _companents.FirstOrDefault(companent => companent is MultipliersResultView);
}