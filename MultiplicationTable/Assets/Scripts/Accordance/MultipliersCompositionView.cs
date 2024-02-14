using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultipliersCompositionView : UICompanent {
    [SerializeField] private TextMeshProUGUI _labelValue;
    [SerializeField] private Image _frameImage;
    [SerializeField] private ConnectStatusView _connectStatus;

    private MultipliersCompositionViewConfig _config;
    private LineSpawner _lineSpawner;

    private Color _defaultColor;

    private Color _trueVerificationColor = Color.blue;
    private Color _falseVerificationColor = Color.red;
    private Color _frameColor;

    public Color FrameColor => _frameColor;
    public Transform ConnectPointTransform => _connectStatus.ConnectPoint;

    public void Init(MultipliersCompositionViewConfig config) {
        _config = config;

        FillingCompanents();
    }

    private void FillingCompanents() {
        _defaultColor = _frameImage.color;
        _frameColor = _defaultColor;

        _labelValue.text = $"{_config.Data.Multipliable}X{_config.Data.Multiplier}";
    }

    private void OnMouseDown() {
        _lineSpawner.StartLine(_connectStatus.ConnectPoint.position);
    }
}
