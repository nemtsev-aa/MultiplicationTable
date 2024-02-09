using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquationItem : UICompanent, IDragHandler, IBeginDragHandler, IEndDragHandler {
    public event Action<EquationSlot> ParentChanged;
    public event Action<EquationItem> ParentCleared;

    [SerializeField] private Image _frameImage;
    [SerializeField] private TextMeshProUGUI _labelValue;

    private RectTransform _rectTransform;
    private Transform _defaultTransform;
    private CanvasGroup _canvasGroup;

    private EquationItemConfig _config;
    private float _scaleFactor;

    private Color _defaultColor;
    
    private EquationSlot _slot;


    public int Value => _config.Value;
    public EquationSlot Slot { get; set; }

    public void Init(EquationItemConfig config) {
        _config = config;

        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();

        if (_config.MainCanvas != null)
            _scaleFactor = _config.MainCanvas.scaleFactor;

        _defaultColor = _frameImage.color;

        //AddListeners();
        FillingCompanents();
    }

    #region DragMetods
    public void OnBeginDrag(PointerEventData eventData) {
        if (_config.MainCanvas == false)
            return;

        _slot = Slot;

        Slot = null;

        _rectTransform.SetParent(_config.RectTransform);
        _rectTransform.parent.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;

        SetColor(_defaultColor);
    }

    public void OnDrag(PointerEventData eventData) {
        _rectTransform.anchoredPosition += eventData.delta / _scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (Slot == null) 
            Slot = _slot;

        _rectTransform.SetParent(Slot.transform);
        _rectTransform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
    }

    public void SetSlot(EquationSlot slot) {
        if (Slot != null && Slot.Equals(slot))
            return;

        ParentChanged?.Invoke(_slot);

        Slot = slot;

        _defaultTransform = Slot.transform;
        transform.SetParent(_defaultTransform);
        transform.localPosition = Vector3.zero;

        SetColor(Color.white);
        
    }

    #endregion

    private void FillingCompanents() =>
        _labelValue.text = $"{_config.Value}";

    private void SetColor(Color color) {
        _labelValue.color = color;
        _frameImage.color = color;
    }

    public override void Dispose() {
        base.Dispose();

        //RemoveListeners();
    }

}
