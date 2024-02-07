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

    private bool _hideAfterSelection;
    
    [Inject]
    private void Construct(UICompanentsFactory companentsFactory) {
        _factory = companentsFactory;
    }

    public void Init(MultipliersConfig config, bool hideAfterSelection = true) {
        _config = config;
        _hideAfterSelection = hideAfterSelection;

        CreateNumberViews();
        Show(false);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        foreach (var iView in _numberViews) {
            iView.ViewSelected -= OnViewSelected;
        }
    }

    private void CreateNumberViews() {
        _numberViews = new List<NumberView>();

        foreach (var iConfig in _config.Multipliers) {
            NumberViewConfig config = new NumberViewConfig(iConfig);

            NumberView newView = _factory.Get<NumberView>(config, _parent);
            newView.Init(config);
            newView.ViewSelected += OnViewSelected;

            _numberViews.Add(newView);
        }
    }

    private void OnViewSelected(int multiplier) {
        Show(!_hideAfterSelection);

        MultiplierSelected?.Invoke(multiplier);
    } 
}
