using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class LineSpawner : MonoBehaviour {
    private List<Transform> _startPoints;
    private Transform _linesParent;
    private Pointer _pointer;

    private LineFactory _lineFactory;
    private Line _currentLine;
    private List<Line> _lines;

    public LineRenderer Renderer => _currentLine.Renderer;

    [Inject]
    public void Construct(LineFactory lineFactory) {
        _lineFactory = lineFactory;
    }

    public void Init(List<Transform> startPoints) {
        _startPoints = startPoints;

        _linesParent = transform.GetChild(0);
        _pointer = GetComponentInChildren<Pointer>();

        StartSpawn();
    }

    private void StartSpawn() {
        if (_startPoints.Count == 0)
            throw new ArgumentNullException($"LineSpawner: StartPoint list is empty!");

        _lines = new List<Line>();

        foreach (var iPoint in _startPoints) {
            Line newLine = _lineFactory.Get(_linesParent);
            newLine.Init(iPoint);
            
            newLine.LineCreated += OnLineCreated;
            newLine.LineUpdated += OnLineUpdated;

            _lines.Add(newLine);
        }
    }

    public void StartLine(Vector2 position) {
        _currentLine = GetLineByStartPoint(position);
    }

    private void OnLineUpdated() {
        _pointer.enabled = true;
    }

    private void OnLineCreated() {
        
    }

    private Line GetLineByStartPoint(Vector3 point) {
        return _lines.FirstOrDefault(line => line.StartPoint == point);
    }
}
