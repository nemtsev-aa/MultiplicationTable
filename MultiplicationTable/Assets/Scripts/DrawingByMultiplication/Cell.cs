using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;

[Serializable]
public struct CellStateConfig {
    public CellStateConfig(Sprite sprite, Color color, string text) {
        Sprite = sprite;
        Color = color;
        Text = text;
    }

    [field: SerializeField] public Color Color { get; set; }
    [field: SerializeField] public Sprite Sprite { get; set; }
    [field: SerializeField] public string Text { get; set; }
}

public class Cell : UICompanent, IPointerDownHandler {
    public event Action<Cell> Selected;

    [Header("CellStateConfigs")]
    [SerializeField] private CellStateConfig _emptyConfig;
    [SerializeField] private CellStateConfig _activeConfig;

    [Space(10)]
    [SerializeField] private Image _background;
    [SerializeField] private TextMeshProUGUI _textLabel;

    private CellStateConfig _fillConfig;

    private float _duration = 0.3f;
    private Color _startColor;
    private Color _endColor = Color.white;
    private Tween _colorTween;
  
    public Image Background => _background;
    public Color FillStateColor => _fillConfig.Color;
    public Vector2 Position => transform.position;
    public CellStates CurrentState { get; private set; } = CellStates.Empty;
    private CellStateConfig CurrentStateConfig => GetConfigByState(CurrentState);
   
    public void Init(Color color, Vector2 position) {
        _fillConfig = new CellStateConfig(_emptyConfig.Sprite, color, $"");
        transform.position = position;

        CurrentState = CellStates.Fill;
        FillingCompanents();
    }

    public void SwitchState(CellStates state) {
        CurrentState = state;

        SetState();
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (CurrentState is CellStates.Empty) 
            Selected?.Invoke(this);
    }

    private void SetState() {
        if (CurrentState == CellStates.Fill) {
            ShowFillAnimation();
            return;
        }

        FillingCompanents();
    }

    private void FillingCompanents() {
        var config = GetConfigByState(CurrentState);

        _background.color = config.Color;
        _background.sprite = config.Sprite;
        _textLabel.text = config.Text;
    }

    private CellStateConfig GetConfigByState(CellStates state) {
        switch (state) {
            case CellStates.Empty:
                StopChangingColor();
                return _emptyConfig;
               
            case CellStates.Active:
                StopChangingColor();
                StartChangingColor();
                return _activeConfig;

            case CellStates.Fill:
                StopChangingColor();
                return _fillConfig;

            default:
                throw new ArgumentException($"Invalid CellStates: {state}");
        }
    }

    private void ShowFillAnimation() {
        var s = DOTween.Sequence();
        var scaledRunTime = _duration;
        var rotationRunTime = _duration * 2f;

        s.Append(transform.DOScale(transform.localScale.x * 2f, scaledRunTime));
        s.Append(transform.DORotate(Vector3.forward * 360f, rotationRunTime, RotateMode.FastBeyond360));
        s.Append(transform.DOScale(Vector3.one, scaledRunTime)).OnComplete(FillingCompanents);
    }

    private void StartChangingColor() {
        _background.sprite = _activeConfig.Sprite;
        _colorTween = _background.DOColor(_endColor, _duration / 2f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private void StopChangingColor() {
        _colorTween.Kill();
        _colorTween = null;

        _background.color = _startColor;
    }

}
