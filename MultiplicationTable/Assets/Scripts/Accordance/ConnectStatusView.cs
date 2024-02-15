using UnityEngine;
using UnityEngine.UI;

public class ConnectStatusView : MonoBehaviour {
    [SerializeField] private Image _background;
    [SerializeField] private Image _filler;

    private MultipliersCompositionView _compositionView;

    private Color _currentFillerColor;
    private Color _defaultFillerColor = Color.white;

    public Vector3 ConnectPoint {
        get {
            _filler.gameObject.transform.GetPositionAndRotation(out Vector3 position, out Quaternion quaternion);
            return position;
        } 
    }

    public void Init(MultipliersCompositionView compositionView) {
        _compositionView = compositionView;
        Reset();
    }


    public void Reset() {
        _background.color = _compositionView.FrameColor;
        _currentFillerColor = _defaultFillerColor;
        _filler.color = _currentFillerColor;
    }

    public void SetVerificationStatus(bool status) {
        _currentFillerColor = _compositionView.FrameColor;

        if (status) {
            _filler.color = _currentFillerColor;
            _background.color = _currentFillerColor;
        }
        else {
            _filler.color = _defaultFillerColor;
            _background.color = _currentFillerColor;
        }

    }
}
