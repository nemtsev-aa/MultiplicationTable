using UnityEngine;
using Zenject;

public class LineFactory {
    private DiContainer _container;
    private Line _linePrefab;

    public LineFactory(DiContainer container, Line linePrefab) {
        _container = container;
        _linePrefab = linePrefab;
    }

    public Line Get(Transform parent) {
        var newLine = _container.InstantiatePrefabForComponent<Line>(_linePrefab, parent);
        return newLine;
    }
}

