using System;
using UnityEngine;
using Zenject;

public class DesktopInput : IInput, ITickable
{
    public event Action<Vector3> ClickDown;
    public event Action<Vector3> ClickUp;
    public event Action<Vector3> Drag;

    private const int LeftMouseButton = 0;

    private bool _isSwiping;
    private Vector3 _previousPosition;

    public void Tick()
    {
        ProcessClickUp();

        ProcessClickDown();

        ProcessSwipe();
    }

    private void ProcessSwipe()
    {
        if (_isSwiping == false)
            return;

        if(Input.mousePosition != _previousPosition)
            Drag?.Invoke(Input.mousePosition);

        _previousPosition = Input.mousePosition;
    }

    private void ProcessClickDown()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            _isSwiping = true;
            _previousPosition = Input.mousePosition;
            ClickDown?.Invoke(_previousPosition);
        }
    }

    private void ProcessClickUp()
    {
        if (Input.GetMouseButtonUp(LeftMouseButton))
        {
            _isSwiping = false;
            ClickUp?.Invoke(Input.mousePosition);
        }
    }
}
