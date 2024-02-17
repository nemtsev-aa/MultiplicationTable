using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineSpawner {
    private LineFactory _lineFactory;

    private List<Line> _lines;

    public LineSpawner(LineFactory lineFactory) {
        _lineFactory = lineFactory;
        _lines = new List<Line>();
    }

    public void SpawnLine(Vector3 point, out Line line) {
        line = GetLineByStartPoint(point);

        if (line == null) {
            Start(point);
            line = _lines[_lines.Count - 1];
        }
    }

    public Line GetLineByStartPoint(Vector3 startPoint) {
        return _lines.FirstOrDefault(line => line.StartPoint == startPoint);
    }

    public void RemoveLine(Line line) => _lines.Remove(line);

    public void Reset() => _lines.Clear();

    private void Start(Vector3 startPointPosition) {
        Line newLine = _lineFactory.Get();
        newLine.Init(startPointPosition);

        _lines.Add(newLine);
    }
}
