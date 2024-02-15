using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class LineSpawner {
    private Transform _linesParent;
    private LineFactory _lineFactory;

    private List<Line> _lines;

    public LineSpawner(LineFactory lineFactory) {
        _lineFactory = lineFactory;
        _lines = new List<Line>();
    }

    public IEnumerable<Line> Lines {
        get { return _lines; }
    }

    private void StartSpawn(Vector3 startPointPosition) {
        Line newLine = _lineFactory.Get();
        newLine.Init(startPointPosition);

        _lines.Add(newLine);
    }

    public void GetLineByStartPoint(Vector3 point, out Line line) {
        line = _lines.FirstOrDefault(line => line.StartPoint == point);

        if (line == null) {
            StartSpawn(point);
            line = _lines[_lines.Count];
        }
    }
}
