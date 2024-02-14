using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultipliersCompositionView : UICompanent {
    [SerializeField] private TextMeshProUGUI _labelValue;
    [SerializeField] private Image _frameImage;
    [SerializeField] private ConnectStatusView _connectStatus;

    private MultipliersCompositionViewConfig _config;
    private LineSpawner _drawing;

    private Color _defaultColor;
    private Color _trueVerificationColor = Color.blue;
    private Color _falseVerificationColor = Color.red;
    private Color _frameColor;

    public Color FrameColor => _frameColor;

    public void Init(MultipliersCompositionViewConfig config) {
        _config = config;
        

        FillingCompanents(); 
    }

    private void FillingCompanents() {
        _defaultColor = _frameImage.color;
        _frameColor = _defaultColor;
    }

    private void OnMouseDown() {
        _drawing.StartLine(_connectStatus.ConnectPoint.position);
    }

    private void OnMouseDrag() {
        _drawing.UpdateLine();
    }

    private void OnMouseUp() {
        
    }

}
