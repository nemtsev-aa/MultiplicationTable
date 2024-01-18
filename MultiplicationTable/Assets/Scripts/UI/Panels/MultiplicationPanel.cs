using System;
using UnityEngine;
using Zenject;

public class MultiplicationPanel : UIPanel {
    [SerializeField] private RectTransform _parent;
    [SerializeField] private int _startValue;
    [SerializeField, Range(1,8)] private int _buttonCount;

    private UICompanentsFactory _factory;
    
    [Inject]
    private void Construct(UICompanentsFactory companentsFactory) {
        _factory = companentsFactory;
    }

    public void Init() {
        CreateButtons();
    }

    private void CreateButtons() {
        for (int i = 0; i < _buttonCount; i++) {
            MultiplierButtonConfig config = new MultiplierButtonConfig(i, MultiplierSelectorStatus.UnConplite);

            MultiplierSelector newButton = _factory.Get<MultiplierSelector>(config, _parent);
            newButton.Int(config);
            newButton.transform.SetParent(transform);
            newButton.MultiplierSelected += OnMultiplierSelected;

            _startValue++;
        }
    }

    private void OnMultiplierSelected(int level) {
        Debug.Log($"{level}");
    }
}
