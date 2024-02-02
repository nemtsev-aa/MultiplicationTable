using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TrainingGameSelectorsPanel : UIPanel {
    public event Action<TrainingGameTypes> TrainingGameSelected;

    [SerializeField] private RectTransform _parent;
    [SerializeField] private ToggleGroup _toggleGroup;

    private UICompanentsFactory _factory;
    private TrainingGameConfigs _config;
    private List<TrainingGameSelector> _selectors = new List<TrainingGameSelector>();

    [Inject]
    private void Construct(UICompanentsFactory companentsFactory, TrainingGameConfigs config) {
        _factory = companentsFactory;
        _config = config;
    }

    public void Init() {
        CreateGameSelectors();
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        foreach (var iSelector in _selectors) {
            iSelector.TrainingGameSelected -= OnGameSelected;
        }
    }

    private void CreateGameSelectors() {
        foreach (var iConfig in _config.Configs) {
            TrainingGameSelectorConfig config = new TrainingGameSelectorConfig(iConfig.Name, iConfig.Icon, iConfig.Type);

            TrainingGameSelector newSelector = _factory.Get<TrainingGameSelector>(config, _parent);
            newSelector.Int(config, _toggleGroup);
            newSelector.TrainingGameSelected += OnGameSelected;

            _selectors.Add(newSelector);
        }
    }

    private void OnGameSelected(TrainingGameTypes type) {
        TrainingGameSelected?.Invoke(type);
    }
}
