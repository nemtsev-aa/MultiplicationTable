using System;
using UnityEngine;
using UnityEngine.UI;

public class NavigatingByCellsArrayPanel : UIPanel {
    public event Action<OffsetDirections> DirectionChanged;

    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _upButton;
    [SerializeField] private Button _downButton;
    [SerializeField] private Button _rightButton;

    public void Init() {
        AddListeners();
    }

    public override void AddListeners() {
        base.AddListeners();

        _leftButton.onClick.AddListener(() => DirectionButtonClick(OffsetDirections.Left));
        _upButton.onClick.AddListener(() => DirectionButtonClick(OffsetDirections.Up));
        _downButton.onClick.AddListener(() => DirectionButtonClick(OffsetDirections.Down));
        _rightButton.onClick.AddListener(() => DirectionButtonClick(OffsetDirections.Right));
    }

    public override void RemoveListeners() {
        base.RemoveListeners();

        _leftButton.onClick.RemoveListener(() => DirectionButtonClick(OffsetDirections.Left));
        _upButton.onClick.RemoveListener(() => DirectionButtonClick(OffsetDirections.Up));
        _downButton.onClick.RemoveListener(() => DirectionButtonClick(OffsetDirections.Down));
        _rightButton.onClick.RemoveListener(() => DirectionButtonClick(OffsetDirections.Right));
    }

    private void DirectionButtonClick(OffsetDirections offsetDirection) {
        DirectionChanged?.Invoke(offsetDirection);
    }

}
