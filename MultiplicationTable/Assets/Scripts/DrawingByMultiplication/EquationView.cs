using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EquationView : UICompanent {
    public const string MultiplierDefaultText = "?";
    public const string NotEqualChar = "/";
    public const string EqualChar = "=";

    public event Action MultiplierInputStatusChanged;

    [SerializeField] private Button _numberInputButton;
    [SerializeField] private TextMeshProUGUI _multipliableText;
    [SerializeField] private TextMeshProUGUI _multiplierText;
    [SerializeField] private TextMeshProUGUI _equationText;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private Image _backgroundColor;

    private float _duration = 0.3f;
    private EquationData _data;

    public int Multiplier => _data.Multiplier;

    public void Init(EquationPanel equationPanel, EquationViewConfig config) {
        _data = config.Data;

        AddListeners();
        FillingCompanents();
    }
    
    public void ShowMultiplier(string value) {
        _multiplierText.text = value;
    }

    public void ShowEquationVerificationResult(bool result) {
        if (result) {
            _equationText.text = $"{EqualChar}";
            _equationText.color = Color.green;
        }
        else {
            _equationText.text = $"{NotEqualChar}";
            _equationText.color = Color.red;
        }

        ShowResultAnimation();
    }

    public void Reset() {
        _data = null;
        RemoveListeners();
    }

    private void AddListeners() {
        _numberInputButton.onClick.AddListener(MultiplierInputButtonClick);
    }

    private void RemoveListeners() {
        _numberInputButton.onClick.RemoveListener(MultiplierInputButtonClick);
    }

    private void FillingCompanents() {
        _multipliableText.text = $"{_data.Multipliable}";
        _multiplierText.text = $"{MultiplierDefaultText}";
        _equationText.text = $"{EqualChar}";
        _resultText.text = $"{_data.Result}";

        if (_data.BaseColor != Color.clear)
            _backgroundColor.color = _data.BaseColor;
    }

    private void MultiplierInputButtonClick()
        => MultiplierInputStatusChanged?.Invoke();

    private void ShowResultAnimation() {
        var s = DOTween.Sequence();

        s.Append(_equationText.transform.DOScale(Vector3.one * 1.3f, _duration));
        s.Append(_equationText.transform.DOScale(Vector3.one, _duration));
        s.Append(_equationText.DOColor(Color.white, _duration));

    }

    public override void Dispose() {
        base.Dispose();

        RemoveListeners();
    }
}
