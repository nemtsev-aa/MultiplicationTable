using System;
using UnityEngine;
using Zenject;

public class Line : MonoBehaviour {
    public event Action LineCreated;
    public event Action LineUpdated;

    private Material _material;
    private Vector3 _previousePosition;
    
    [field: SerializeField] public LineRenderer Renderer { get; private set; }

    public Vector3 StartPoint => Renderer.GetPosition(0);
    public Vector3 EndPoint => Renderer.GetPosition(1);

    public void Init(Vector3 startPointPosition) {
        _material = new Material(Renderer.material);
        Renderer.material = _material;
        Renderer.positionCount = 2;

        StartLine(startPointPosition);
    }

    public void StartLine(Vector2 position) {
        Renderer.SetPosition(0, position);

        _previousePosition = position;
    }

    public void UpdateLine(Vector2 position) {
        Renderer.SetPosition(1, position);
        _previousePosition = position;
    }

    public void EndLine(Vector2 position) {
        //var position = new Vector2(transform.position.x, transform.position.y);
        Renderer.SetPosition(1, position);

        //_previousePosition = position;
    }

    public void SetColor(Color color) => _material.color = color;
    
    public class Factory : PlaceholderFactory<Line> {
    }
}
