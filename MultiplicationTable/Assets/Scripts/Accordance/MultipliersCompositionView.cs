using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MultipliersCompositionView : UICompanent {
    public event Action<Transform> CompositionSelected;

    [SerializeField] private TextMeshProUGUI _labelValue;
    [SerializeField] private Image _frameImage;
    [SerializeField] private ConnectStatusView _connectStatus;

    private MultipliersCompositionViewConfig _config;
    
    private Color _defaultColor;

    private Color _selectionColor = Color.green;
    private Color _trueVerificationColor = Color.blue;
    private Color _falseVerificationColor = Color.red;
    private Color _frameColor;

    public Color FrameColor => _frameColor;
    public Vector3 ConnectPointPosition => _connectStatus.ConnectPoint;

    public void Init(MultipliersCompositionViewConfig config) {
        _config = config;

        FillingCompanents();
    }

    public void Select(bool status) {
        if (status)
            _frameColor = _selectionColor;
        else
            _frameColor = _defaultColor;

        _frameImage.color = _frameColor;
    }

    private void FillingCompanents() {
        _defaultColor = _frameImage.color;
        _frameColor = _defaultColor;

        _labelValue.text = $"{_config.Data.Multipliable}X{_config.Data.Multiplier}";
    }

}
