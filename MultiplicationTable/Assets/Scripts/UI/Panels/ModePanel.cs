using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ModePanel : UIPanel {
    public event Action<ModeTypes> ModeSelected;

    [SerializeField] private RectTransform _parent;
    [SerializeField] private ToggleGroup _toggleGroup;

    private UICompanentsFactory _factory;
    private ModsConfig _config;
    private List<ModeSelector> _selectors = new List<ModeSelector>();

    [Inject]
    private void Construct(UICompanentsFactory companentsFactory, ModsConfig config) {
        _factory = companentsFactory;
        _config = config;
    }

    public void Init() {
        CreateModeSelectors();
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        foreach (var iSelector in _selectors) {
            iSelector.ModeSelected -= OnModeSelected;
        }
    }

    private void CreateModeSelectors() {
        foreach (var iConfig in _config.Configs) {
            ModeSelectorConfig config = new ModeSelectorConfig(iConfig.Name, iConfig.Icon, iConfig.Type);

            ModeSelector newSelector = _factory.Get<ModeSelector>(config, _parent);
            newSelector.Int(config, _toggleGroup);
            newSelector.ModeSelected += OnModeSelected;

            _selectors.Add(newSelector);
        }
    }

    private void OnModeSelected(ModeTypes type) {
        ModeSelected?.Invoke(type);
    }
}
