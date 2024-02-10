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
    private CanvasGroup _canvasGroup;
    private EquationItemConfig _config;

    private EquationSlot _slot;
    private Transform _parentAfterDrag;

    private float _scaleFactor;
    private Color _defaultColor;
    

    public int Value => _config.Value;
    
    public EquationSlot Slot {
        get {
            return _slot;
        }
        set {
            if (value.Equals(_slot))
                return;

            _slot = value;
            Debug.Log($"{gameObject.name}: CurrentSlot: {Slot.gameObject.name}");
        }
    }

    public Transform ParentAfterDrag {
        get {
            return _parentAfterDrag;
        }
        set {
            if (value.Equals(transform))
                return;

            ClearItemInBeforeSlot();
            _parentAfterDrag = value;
            SetParentAfterDrag();
        }
    }

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
        
        _parentAfterDrag = transform.parent;
        _rectTransform.SetParent(transform.root);
        _rectTransform.parent.SetAsLastSibling();
        
        _canvasGroup.blocksRaycasts = false;

        SetColor(_defaultColor);
    }

    public void OnDrag(PointerEventData eventData) {
        _rectTransform.anchoredPosition += eventData.delta / _scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        SetParentAfterDrag();
    }

    private void SetParentAfterDrag() {

        transform.SetParent(_parentAfterDrag);
        SetItemInAfterSlot();

        _rectTransform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
        
        SetColor(Color.white);
    }
    
    private void ClearItemInBeforeSlot() {
        if (_parentAfterDrag == null)
            return;

        var slot = GetSlotByTransform(_parentAfterDrag);

        if (slot != null)
            slot.CurrentItem = null;
    }
    
    private void SetItemInAfterSlot() {
        if (transform.parent.TryGetComponent(out EquationSlot slot)) {
            if (Slot != null && Slot.Equals(slot) == true)
                return;

            Slot = slot;
            Slot.CurrentItem = this;
        }
    }
    private EquationSlot GetSlotByTransform(Transform parent) {
        if (parent.TryGetComponent(out EquationSlot slot))
            return slot;
        else
            return null;
    }

    //public void SetSlot(EquationSlot slot) {
    //    if (Slot != null && Slot.Equals(slot))
    //        return;

    //    Slot = slot;

    //    _defaultTransform = Slot.transform;
    //    //transform.SetParent(_defaultTransform);
    //    transform.localPosition = Vector3.zero;

    //    SetColor(Color.white);

    //}

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
