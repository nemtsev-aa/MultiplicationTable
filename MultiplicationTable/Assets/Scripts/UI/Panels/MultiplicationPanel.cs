using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MultiplicationPanel : UIPanel {
    public event Action<int> MultiplierSelected;

    [SerializeField] private RectTransform _parent;
    [SerializeField] private ToggleGroup _toggleGroup;

    [SerializeField] private int _startValue;
    [SerializeField, Range(1, 10)] private int _buttonCount;

    private UICompanentsFactory _factory;
    private List<MultiplierSelector> _selectors = new List<MultiplierSelector>();

    [Inject]
    private void Construct(UICompanentsFactory companentsFactory) {
        _factory = companentsFactory;
    }

    public void Init() {
        CreateMultiplierSelectors();
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        foreach (var iSelector in _selectors) {
            iSelector.MultiplierSelected -= OnMultiplierSelected;
        }
    }

    private void CreateMultiplierSelectors() {
        for (int i = _startValue; i < _buttonCount; i++) {
            MultiplierButtonConfig config = new MultiplierButtonConfig(i, MultiplierSelectorStatus.UnConplite);

            MultiplierSelector newSelector = _factory.Get<MultiplierSelector>(config, _parent);
            newSelector.Int(config, _toggleGroup);
            newSelector.MultiplierSelected += OnMultiplierSelected;
            _selectors.Add(newSelector);
            
            _startValue++;
        }
    }

    private void OnMultiplierSelected(int level) {
        MultiplierSelected?.Invoke(level);
    }
}
