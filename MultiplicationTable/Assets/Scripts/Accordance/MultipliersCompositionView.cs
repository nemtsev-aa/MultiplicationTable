using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultipliersCompositionView : UICompanent, IConnected {
    public event Action<Transform> CompositionSelected;
    public event Action FrameColorChanged;

    [SerializeField] private TextMeshProUGUI _labelValue;
    [SerializeField] private Image _frameImage;
    [SerializeField] private ConnectStatusView _connectStatus;

    private MultipliersCompositionViewConfig _config;
    private Color _frameColor;

    [field: SerializeField] public ConnectedViewConfig ConnectedViewConfig { get; set; }

    public Color FrameColor {
        get { return _frameColor; }
    }

    public Vector3 ConnectPointPosition => _connectStatus.ConnectPoint;

    public void Init(MultipliersCompositionViewConfig config) {
        _config = config;

        _connectStatus.Init(this);
        FillingCompanents();
    }

    public void Select(bool status) {
        if (status)
            _frameColor = ConnectedViewConfig.SelectionColor;
        else
            _frameColor = ConnectedViewConfig.DefaultColor;

        _frameImage.color = _frameColor;
        FrameColorChanged?.Invoke();
    }

    public void FillingCompanents() {
        Select(false);

        _labelValue.text = $"{_config.Data.Multipliable}X{_config.Data.Multiplier}";
    }
}
