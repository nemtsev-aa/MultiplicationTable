using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class AccordancePanel : UIPanel {
    [SerializeField] private RectTransform _compositionParent;
    [SerializeField] private RectTransform _resultParent;

    private UICompanentsFactory _factory;
    private MovementHandler _movementHandler;
    private LineSpawner _lineSpawner;
    private List<MultipliersCompositionView> _compositionViews;
    private List<MultipliersResultView> _resultViews;

    private List<EquationData> _equations;
    private bool _hideAfterSelection;
    private Line _currentLine;

    private MultipliersCompositionView _compositionView;
    private MultipliersResultView _resultView;

    [Inject]
    private void Construct(UICompanentsFactory companentsFactory, MovementHandler movementHandler, LineSpawner lineSpawner) {
        _factory = companentsFactory;
        _movementHandler = movementHandler;
        _lineSpawner = lineSpawner;
    }

    public void Init(List<EquationData> equations, bool hideAfterSelection = true) {
        _equations = equations;
        _hideAfterSelection = hideAfterSelection;

        CreateCompositionViews();
        CreateResultViews();

        AddListeners();

        Show(!hideAfterSelection);
    }

    public override void AddListeners() {
        base.AddListeners();

        _movementHandler.CompositionViewSelected += OnCompositionViewSelected;
        _movementHandler.Dragged += OnDragged;
        _movementHandler.ResultViewSelected += OnResultViewSelected;
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        //foreach (var iView in _compositionViews) {
        //    iView.CompositionSelected -= OnCompositionSelected;
        //}

        //foreach (var iView in _resultViews) {
        //    //iView.
        //}
    }

    public override void Reset() {
        base.Reset();

        foreach (var iView in _compositionViews) {
            Destroy(iView.gameObject);
        }
        _compositionViews.Clear();

        foreach (var iView in _resultViews) {
            Destroy(iView.gameObject);
        }
        _resultViews.Clear();

        _equations = null;
    }
    
    private void CreateCompositionViews() {
        _compositionViews = new List<MultipliersCompositionView>();

        foreach (var iData in _equations) {
            MultipliersCompositionViewConfig config = new MultipliersCompositionViewConfig(iData);

            MultipliersCompositionView newView = _factory.Get<MultipliersCompositionView>(config, _compositionParent);
            newView.Init(config);
            
            _compositionViews.Add(newView);
        }
    }

    private void CreateResultViews() {
        var shuffledEquation = Shuffle(_equations);
        _resultViews = new List<MultipliersResultView>();

        foreach (var iData in shuffledEquation) {
            MultipliersResultConfig config = new MultipliersResultConfig(iData.Result);

            MultipliersResultView newView = _factory.Get<MultipliersResultView>(config, _resultParent);
            newView.Init(config);

            _resultViews.Add(newView);
        }
    }
    
    private void OnCompositionViewSelected(MultipliersCompositionView view) {
        _compositionView = view;
        _compositionView.Select(true);

        _lineSpawner.SpawnLine(view.ConnectPointPosition, out Line line);
        _currentLine = line;
        _currentLine.SetColor(_compositionView.FrameColor);
    }

    private void OnDragged(Vector2 position) {
        _currentLine.UpdateLine(position);
    }

    private void OnResultViewSelected(MultipliersResultView view) {
        if (view == null) {
            _compositionView.Select(false);
            _compositionView = null;

            Line line = _lineSpawner.GetLineByStartPoint(_currentLine.StartPoint);
            _lineSpawner.RemoveLine(line);
            Destroy(line.gameObject);

            return;
        }

        _resultView = view;
        view.Select(true);
    }

    private List<EquationData> Shuffle(List<EquationData> list) {
        EquationData[] array = list.ToArray();

        for (int i = array.Length - 1; i > 0; i--) {
            int rnd = UnityEngine.Random.Range(0, i);
            EquationData temp = array[i];

            array[i] = array[rnd];
            array[rnd] = temp;
        }

        return array.ToList();
    }
}
