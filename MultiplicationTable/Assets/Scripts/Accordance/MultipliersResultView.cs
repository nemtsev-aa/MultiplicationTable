using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultipliersResultView : UICompanent {
    [SerializeField] private TextMeshProUGUI _labelValue;
    [SerializeField] private Image _frameImage;
    [SerializeField] private ConnectStatusView _connectStatus;

    private MultipliersResultConfig _config;
 
    private Color _defaultColor;

    private Color _trueVerificationColor = Color.blue;
    private Color _falseVerificationColor = Color.red;
    private Color _frameColor;

    public Color FrameColor => _frameColor;
    public Vector3 ConnectPointPosition => _connectStatus.ConnectPoint.position;

    public void Init(MultipliersResultConfig config) {
        _config = config;

        FillingCompanents();
    }

    public void SetColor(Color color) {

    }

    private void FillingCompanents() {
        _defaultColor = _frameImage.color;
        _frameColor = _defaultColor;

        _labelValue.text = $"{_config.Result}"; 
    }


}
