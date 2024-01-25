using System;
using UnityEngine;
using Zenject;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;

public class DifficultyLevelPanel : UIPanel {
    public event Action<DifficultyLevelData> DifficultyLevelSelected;

    [SerializeField] private RectTransform _selectorsParent;
    [SerializeField] private ToggleGroup _toggleGroup;

    private UICompanentsFactory _factory;
    private LevelsConfig _levelsConfig;
    private List<DifficultyLevelSelector> _selectors = new List<DifficultyLevelSelector>();

    [Inject]
    private void Construct(UICompanentsFactory companentsFactory, LevelsConfig levelsConfig) {
        _factory = companentsFactory;
        _levelsConfig = levelsConfig;
    }

    public void Init() {
        CreateLevelSelector();
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        foreach (var iSelector in _selectors) {
            iSelector.Selected -= OnSelected;
        }
    }

    private void CreateLevelSelector() {
        foreach (var iLevelConfig in _levelsConfig.Configs) {
            DifficultyLevelSelectorConfig comanentConfig = new DifficultyLevelSelectorConfig(iLevelConfig.Name, iLevelConfig.Type);

            DifficultyLevelSelector newSelector = _factory.Get<DifficultyLevelSelector>(comanentConfig, _selectorsParent);
            newSelector.Init(comanentConfig, _toggleGroup);
            newSelector.Selected += OnSelected;

            _selectors.Add(newSelector);
        }
    }

    private void OnSelected(DifficultyLevelSelectorConfig config) {
        var selectedDifficultyLevel = _levelsConfig.Configs.FirstOrDefault(data => data.Type == config.Type);
        
        DifficultyLevelSelected?.Invoke(selectedDifficultyLevel);
    }

    //(EnemyType) UnityEngine.Random.Range(0, Enum.GetValues(typeof(EnemyType)).Length)
}