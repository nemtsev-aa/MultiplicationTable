using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquationBuilderView : UICompanent {
    public const string MultiplierDefaultText = "?";
    public const string NotEqualChar = "/";
    public const string EqualChar = "=";

    public event Action MultiplierInputStatusChanged;
    public event Action<EquationData, bool> EquationVerificatedChanged;

    [SerializeField] private RectTransform _multipliableSlotParent;
    [SerializeField] private RectTransform _multiplierSlotParent;

    [SerializeField] private TextMeshProUGUI _equationText;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private Image _backgroundColor;

    private EquationSlot _multipliableSlot;
    private EquationSlot _multiplierSlot;
    private float _duration = 0.3f;
    private EquationData _data;
    
    private int Multipliable {
        get {
            if (_multipliableSlot.CurrentItem != null)
                return _multipliableSlot.CurrentItem.Value;
            else
                return 0;
        }
    }

    private int Multiplier {
        get {
            if (_multiplierSlot.CurrentItem != null)
                return _multiplierSlot.CurrentItem.Value;
            else
                return 0;
        }
    }

    public void Init(EquationBuilderViewConfig config, EquationSlot multipliableSlot, EquationSlot multiplierSlot) {
        _data = config.Data;

        SetParentByEquationSlot(multipliableSlot, multiplierSlot);
        AddListeners();
        FillingCompanents();
    }

    
    public void Reset() {
        _data = null;
        //RemoveListeners();
    }

    private void SetParentByEquationSlot(EquationSlot multipliableSlot, EquationSlot multiplierSlot) {
        _multipliableSlot = multipliableSlot;
        _multipliableSlot.transform.SetParent(_multipliableSlotParent);
        _multipliableSlot.transform.localPosition = Vector3.zero;

        _multiplierSlot = multiplierSlot;
        _multiplierSlot.transform.SetParent(_multiplierSlotParent);
        _multiplierSlot.transform.localPosition = Vector3.zero;
    }

    private void AddListeners() {
        _multipliableSlot.ItemValueChanged += OnItemValueChanged;
        _multiplierSlot.ItemValueChanged += OnItemValueChanged;
    }

    private void RemoveListeners() {
        _multipliableSlot.ItemValueChanged -= OnItemValueChanged;
        _multiplierSlot.ItemValueChanged -= OnItemValueChanged;
    }

    private void OnItemValueChanged() {
        EquationVerification();
    }

    private void FillingCompanents() {
        //_multipliableText.text = $"{_data.Multipliable}";
        //_multiplierText.text = $"{MultiplierDefaultText}";

        _equationText.text = $"{EqualChar}";
        _resultText.text = $"{_data.Result}";

        if (_data.BaseColor != Color.clear)
            _backgroundColor.color = _data.BaseColor;
    }

    private void EquationVerification() {
        if (Multipliable == 0 || Multiplier == 0)
            return;

        int composition = Multipliable * Multiplier;
        bool result = (composition == _data.Result) ? true : false;

        ShowEquationVerificationResult(result);
        EquationVerificatedChanged?.Invoke(_data, result);
    }

    private void ShowEquationVerificationResult(bool result) {
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
