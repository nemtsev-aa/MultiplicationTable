using System;
using UnityEngine;

public class Line : MonoBehaviour {
    public event Action LineCreated;
    public event Action LineUpdated;

    private Material _material;
    private Vector3 _previousePosition;
    private float _minDistance = 0.1f;

    [field: SerializeField] public LineRenderer Renderer { get; private set; }
    public Vector3 StartPoint => Renderer.GetPosition(0);

    public void Init(Transform transform) {
        _material = new Material(Renderer.material);
        Renderer.material = _material;

        Renderer.positionCount = 1;
        var position = new Vector2(transform.position.x, transform.position.y);
        Renderer.SetPosition(0, position);

        _previousePosition = position;
    }

    public void UpdateLine() {
        if (Input.GetMouseButton(0)) {
            LineUpdated?.Invoke();

            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;

            if (Vector3.Distance(currentPosition, _previousePosition) > _minDistance) {
                if (_previousePosition == transform.position) {
                    Renderer.SetPosition(0, currentPosition);
                }
                else {
                    Renderer.positionCount++;
                    Renderer.SetPosition(Renderer.positionCount - 1, currentPosition);
                }

                _previousePosition = currentPosition;
            }
        }
        else {
            LineCreated?.Invoke();
        }
    }

    public void SetColor(Color color) => _material.color = color;

}
