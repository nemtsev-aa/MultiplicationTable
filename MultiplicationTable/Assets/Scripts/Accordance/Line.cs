using System;
using UnityEngine;

public class Line : MonoBehaviour {
    private Material _material;

    [field: SerializeField] public LineRenderer Renderer { get; private set; }

    public Vector3 StartPoint => Renderer.GetPosition(0);
    public Vector3 EndPoint => Renderer.GetPosition(1);

    public void Init(Vector3 startPointPosition) {
        _material = new Material(Renderer.material);
        Renderer.material = _material;
        Renderer.positionCount = 2;

        StartLine(startPointPosition);
        EndLine(startPointPosition);
    }

    public void StartLine(Vector2 position) {
        Renderer.SetPosition(0, position);
    }

    public void UpdateLine(Vector2 position) {
        Renderer.SetPosition(1, position);
    }

    public void EndLine(Vector2 position) {
        Renderer.SetPosition(1, position);
    }

    public void SetColor(Color color) => _material.color = color;

}
