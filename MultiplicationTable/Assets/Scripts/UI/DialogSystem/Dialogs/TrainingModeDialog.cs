using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingModeDialog : Dialog {
    public event Action<TrainingGameData> TrainingGameStarted;

    [SerializeField] private Button _startButton;

    private MultiplicationPanel _multiplicationPanel;
    private DifficultyLevelPanel _difficultyLevelPanel;
    private TrainingGameSelectorsPanel _trainingGamePanel;

    private List<int> _currentMultipliers;
    private DifficultyLevelTypes _currentDifficultyLevelType;
    private TrainingGameTypes _currentTrainingGameType;

    public TrainingGameData TrainingGameData { get; private set; }

    public override void InitializationPanels() {
        base.InitializationPanels();

        _multiplicationPanel = GetPanelByType<MultiplicationPanel>();
        _multiplicationPanel.Init();

        _difficultyLevelPanel = GetPanelByType<DifficultyLevelPanel>();
        _difficultyLevelPanel.Init();

        _trainingGamePanel = GetPanelByType<TrainingGameSelectorsPanel>();
        _trainingGamePanel.Init();
    }

    public override void AddListeners() {
        base.AddListeners();

        _startButton.onClick.AddListener(OnStartClick);
        _multiplicationPanel.MultiplierSelected += OnMultiplierSelected;
        _difficultyLevelPanel.DifficultyLevelSelected += OnDifficultyLevelSelected;
        _trainingGamePanel.TrainingGameSelected += OnTrainingGameSelected;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _startButton.onClick.RemoveListener(OnStartClick);
        _multiplicationPanel.MultiplierSelected -= OnMultiplierSelected;
        _difficultyLevelPanel.DifficultyLevelSelected -= OnDifficultyLevelSelected;
        _trainingGamePanel.TrainingGameSelected -= OnTrainingGameSelected;
    }

    private void OnMultiplierSelected(List<int> multipliers) {
        _currentMultipliers = multipliers;

        Debug.Log($"Selection Multiplier: {_currentMultipliers.Count}");
    }

    private void OnDifficultyLevelSelected(DifficultyLevelTypes type) {
        _currentDifficultyLevelType = type;

        Debug.Log($"Selection DifficultyLevel: {_currentDifficultyLevelType}");
    }

    private void OnTrainingGameSelected(TrainingGameTypes trainingGameType) {
        _currentTrainingGameType = trainingGameType;
        Debug.Log($"Selection MiniGame: {_currentTrainingGameType}");
    }

    private void OnStartClick() {
        TrainingGameData = new TrainingGameData(ModeTypes.Training, _currentTrainingGameType,
            _currentMultipliers, _currentDifficultyLevelType);

        TrainingGameStarted?.Invoke(TrainingGameData);
        Debug.Log($"MiniGame Started: {TrainingGameData}");
    }
}
