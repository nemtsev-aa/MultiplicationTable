using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EquationBuildersPanel : UIPanel {
    public event Action<EquationData, bool> EquationVerificatedChanged;

    [SerializeField] private RectTransform _parent;

    private UICompanentsFactory _factory;
    private List<EquationBuilderView> _builderViews;

    private List<EquationData> _equations;
    private int _solvedEquationsCount;

    private bool _hideAfterSelection;

    [Inject]
    private void Construct(UICompanentsFactory companentsFactory) {
        _factory = companentsFactory;
    }

    public void Init(List<EquationData> equations, bool hideAfterSelection = true) {
        _equations = equations;
        _hideAfterSelection = hideAfterSelection;

        CreateEquationBuilders();
        Show(!hideAfterSelection);
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        foreach (var iView in _builderViews) {
            iView.EquationVerificatedChanged -= OnEquationVerificatedChanged;
        }
    }

    public override void Reset() {
        base.Reset();

        foreach (var iView in _builderViews) {
            Destroy(iView.gameObject);
        }

        _builderViews.Clear();
    }

    private void CreateEquationBuilders() {
        _builderViews = new List<EquationBuilderView>();

        foreach (var iEquation in _equations) {
            EquationBuilderViewConfig config = new EquationBuilderViewConfig(iEquation);
            EquationSlotConfig slotConfig = new EquationSlotConfig();

            EquationBuilderView newView = _factory.Get<EquationBuilderView>(config, _parent);
            EquationSlot multipliableSlot = _factory.Get<EquationSlot>(slotConfig, _parent);
            multipliableSlot.Init(slotConfig);
            EquationSlot multiplierSlot = _factory.Get<EquationSlot>(slotConfig, _parent);
            multiplierSlot.Init(slotConfig);

            newView.Init(config, multipliableSlot, multiplierSlot);
            newView.EquationVerificatedChanged += OnEquationVerificatedChanged;

            _builderViews.Add(newView);
        }
    }

    private void OnEquationVerificatedChanged(EquationData data, bool result) {
        EquationVerificatedChanged?.Invoke(data, result);
    }
}
