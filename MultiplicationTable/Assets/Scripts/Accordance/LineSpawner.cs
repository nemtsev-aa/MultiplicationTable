using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineSpawner : MonoBehaviour {
    [SerializeField] private Pointer _pointer;
    [SerializeField] private float _minDistance = 0.1f;

    private LineFactory _lineFactory;
    private Line _currentLine;
    private List<Line> _lines;

    private Vector3 _previousePosition;

    public LineRenderer Renderer => _currentLine.Renderer;


    private void Start() {
        Init();
    }

    public void Init() {
        _previousePosition = transform.position;
    }

    public void SetCurretLine(Vector3 startPoint) {
        _currentLine = GetLineByStartPoint(startPoint);
    }

    public void StartLine(Vector2 position) {

        _currentLine = _lineFactory.Get(transform);
        _currentLine.Init();

        Renderer.positionCount = 1;
        Renderer.SetPosition(0, position);
    }

    public void UpdateLine() {
        if (Input.GetMouseButton(0)) {
            _pointer.enabled = true;

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
            _pointer.enabled = false;
            _lines.Add(_currentLine);
        }
    }

    private Line GetLineByStartPoint(Vector3 point) {
        return _lines.FirstOrDefault(line => line.StartPoint == point);
    }
}
