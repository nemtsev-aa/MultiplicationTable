using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MultiplierSelectionPanel : UIPanel {
    public event Action<int> MultiplierSelected;

    [SerializeField] private RectTransform _parent;

    private UICompanentsFactory _factory;
    private MultipliersConfig _config;
    private List<NumberView> _numberViews;

    [Inject]
    private void Construct(UICompanentsFactory companentsFactory) {
        _factory = companentsFactory;
    }

    public void Init(MultipliersConfig config) {
        CreateNumberViews();
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        foreach (var iView in _numberViews) {
            iView.ViewSelected -= OnViewSelected;
        }
    }

    private void CreateNumberViews() {
        foreach (var iConfig in _config.Multipliers) {
            NumberViewConfig config = new NumberViewConfig(iConfig);

            NumberView newView = _factory.Get<NumberView>(config, _parent);
            newView.ViewSelected += OnViewSelected;

            _numberViews.Add(newView);
        }
    }

    private void OnViewSelected(int multiplier) => MultiplierSelected?.Invoke(multiplier);

}