using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingWithMouse : MonoBehaviour {
    [SerializeField] private float _minDistance = 0.1f;

    private LineRenderer _line;
    private Vector3 _previousePosition;

    public LineRenderer Line => _line;


    private void Start() {
        Init();
    }

    public void Init() {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 1;
        _previousePosition = transform.position;
    }

    public void StartLine(Vector2 position) {
        _line.positionCount = 1;
        _line.SetPosition(0, position);
    }

    public void UpdateLine() {
        if (Input.GetMouseButton(0)) {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;

            if (Vector3.Distance(currentPosition, _previousePosition) > _minDistance) {
                if (_previousePosition == transform.position) {
                    _line.SetPosition(0, currentPosition);
                }
                else {
                    _line.positionCount++;
                    _line.SetPosition(_line.positionCount - 1, currentPosition);
                }

                _previousePosition = currentPosition;
            }
        }
    }
}
