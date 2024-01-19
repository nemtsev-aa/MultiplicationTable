using UnityEngine;

public class MainMenuDialog : Dialog {
    private ModePanel _modePanel;
    private MultiplicationPanel _multiplicationPanel;
    private DifficultyLevelPanel _difficultyLevelPanel;
    
    private ModeTypes _currentModeType;
    private int _currentMultiplier;
    private DifficultyLevelData _currentLevelData;

    public ModeTypes ModeType => _currentModeType;
    public int Multiplier => _currentMultiplier;
    public DifficultyLevelData CurrentLevelData => _currentLevelData;
    
    public override void Init() {
        base.Init();
    }
    
    public override void InitializationPanels() {
        base.InitializationPanels();

        _modePanel = GetPanelByType<ModePanel>();
        _modePanel.Init();

        _multiplicationPanel = GetPanelByType<MultiplicationPanel>();
        _multiplicationPanel.Init();

        _difficultyLevelPanel = GetPanelByType<DifficultyLevelPanel>();
        _difficultyLevelPanel.Init();
    }

    public override void AddListeners() {
        base.AddListeners();

        _modePanel.ModeSelected += OnModeSelected;
        _multiplicationPanel.MultiplierSelected += OnMultiplierSelected;
        _difficultyLevelPanel.DifficultyLevelSelected += OnDifficultyLevelSelected;
    }


    public override void RemoveListeners() {
        base.RemoveListeners();

        _multiplicationPanel.MultiplierSelected -= OnMultiplierSelected;
    }

    private void OnModeSelected(ModeTypes type) {
        _currentModeType = type;

        Debug.Log($"Selection ModeType: {_currentModeType}");
    }

    private void OnMultiplierSelected(int multiplier) {
        _currentMultiplier = multiplier;
        
        Debug.Log($"Selection Multiplier: {_currentMultiplier}");
    }

    private void OnDifficultyLevelSelected(DifficultyLevelData data) {
        _currentLevelData = data;
        
        Debug.Log($"Selection DifficultyLevel: {_currentLevelData.Name}");
    }
}
