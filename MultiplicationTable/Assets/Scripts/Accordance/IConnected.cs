using System;
using UnityEngine;

public interface IConnected {
    public event Action<Transform> CompositionSelected;
    public event Action FrameColorChanged;

    public ConnectedViewConfig ConnectedViewConfig { get; set; }
    public Color FrameColor { get; }

    public void Select(bool status);
    public void FillingCompanents();
}
