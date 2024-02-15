using System;
using UnityEngine;

public class MovementHandler: IDisposable {
    public event Action<MultipliersCompositionView> CompositionViewSelected;
    public event Action<Vector2> Dragged;
    public event Action<MultipliersResultView> ResultViewSelected;

    private IInput _input;

    private bool _isDragging;
    private Vector3 _endPoint;

    private MultipliersCompositionView _compositionView;
    private MultipliersResultView _resultView;

    public MovementHandler(IInput input) {
        _input = input;

        Debug.Log(input.GetType());

        _input.ClickDown += OnClickDown;
        _input.ClickUp += ClickUp;
        _input.Drag += OnDrag;
    }

    public void Dispose() {
        _input.ClickDown -= OnClickDown;
        _input.ClickUp -= ClickUp;
        _input.Drag -= OnDrag;
    }

    private void OnClickDown(Vector3 position) {
        Debug.Log("ClickDown");

        var point = Camera.main.ScreenToWorldPoint(position);
        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
        if (hit.collider != null) {
            if (hit.collider.TryGetComponent(out MultipliersCompositionView compositionView)) {
                _isDragging = true;

                _compositionView = compositionView;
                _compositionView.Select(true);

                Debug.Log($"MultipliersCompositionView: {compositionView}");
                CompositionViewSelected?.Invoke(_compositionView);
            }
        }
    }

    private void OnDrag(Vector3 position) {
        if (_isDragging) {
            var point = Camera.main.ScreenToWorldPoint(position);
            _endPoint = point;
            Debug.Log($"Dragging MousePosition");
        }
    }

    private void ClickUp(Vector3 position) {
        Debug.Log("ClickUp");
        _isDragging = false;

        var point = Camera.main.ScreenToWorldPoint(position);
        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero);
        if (hit.collider != null && hit.collider.TryGetComponent(out MultipliersResultView resultView)) {
            _isDragging = false;

            _resultView = resultView;
            _resultView.Select(true);

            Debug.Log($"MultipliersResultView: {resultView}");
            ResultViewSelected?.Invoke(resultView);
        }
        else
        {
            if (_compositionView != null)
                _compositionView.Select(false);
        }
    }
}
