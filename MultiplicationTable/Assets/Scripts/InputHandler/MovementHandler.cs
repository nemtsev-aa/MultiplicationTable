using System;
using UnityEngine;

public class MovementHandler : IDisposable {
    public event Action<MultipliersCompositionView> CompositionViewSelected;
    public event Action<Vector2> Dragged;
    public event Action<MultipliersResultView> ResultViewSelected;

    private IInput _input;
    private bool _isDragging;

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
        RaycastHit2D hit = Physics2D.Raycast(GetWorldPoint(position), Vector2.zero);
        
        if (hit.collider != null && hit.collider.TryGetComponent(out MultipliersCompositionView compositionView)) {
            _isDragging = true;
            CompositionViewSelected?.Invoke(compositionView);
        }
    }

    private void OnDrag(Vector3 position) {
        if (_isDragging) 
            Dragged?.Invoke(GetWorldPoint(position));
    }

    private void ClickUp(Vector3 position) {
       
        _isDragging = false;

        RaycastHit2D hit = Physics2D.Raycast(GetWorldPoint(position), Vector2.zero);
        
        if (hit.collider != null && hit.collider.TryGetComponent(out MultipliersResultView resultView)) {
            _isDragging = false;
            ResultViewSelected?.Invoke(resultView);
        } else {
            ResultViewSelected?.Invoke(null);
        }
    }

    private Vector3 GetWorldPoint(Vector3 position) {
        return Camera.main.ScreenToWorldPoint(position);
    }
}
